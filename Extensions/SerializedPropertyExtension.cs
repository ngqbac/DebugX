#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace DebugX.Extensions
{
    public static class SerializedPropertyExtension
    {
        public static object GetParent(this SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("[", StringComparison.Ordinal));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[", StringComparison.Ordinal))
                        .Replace("[", "").Replace("]", ""));
                    obj = GetValueAt(obj, elementName, index);
                }
                else
                {
                    obj = GetFieldValue(obj, element);
                }
            }

            return obj;


            object GetValueAt(object source, string name, int index)
            {
                var enumerable = GetFieldValue(source, name) as IEnumerable;
                if (enumerable == null) return null;

                var enm = enumerable.GetEnumerator();
                while (index-- >= 0)
                    enm.MoveNext();
                return enm.Current;
            }

            object GetFieldValue(object source, string name)
            {
                if (source == null)
                    return null;

                foreach (var type in GetHierarchyTypes(source.GetType()))
                {
                    var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (f != null)
                        return f.GetValue(source);

                    var p = type.GetProperty(name,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (p != null)
                        return p.GetValue(source, null);
                }

                return null;


                IEnumerable<Type> GetHierarchyTypes(Type sourceType)
                {
                    yield return sourceType;
                    while (sourceType.BaseType != null)
                    {
                        yield return sourceType.BaseType;
                        sourceType = sourceType.BaseType;
                    }
                }
            }
        }

        public static string AsStringValue(this SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    return property.stringValue;

                case SerializedPropertyType.Character:
                case SerializedPropertyType.Integer:
                    if (property.type == "char") return Convert.ToChar(property.intValue).ToString();
                    return property.intValue.ToString();

                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue != null ? property.objectReferenceValue.ToString() : "null";

                case SerializedPropertyType.Boolean:
                    return property.boolValue.ToString();

                case SerializedPropertyType.Enum:
                    return property.GetValue().ToString();

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Get raw object value out of the SerializedProperty
        /// </summary>
        public static object GetValue(this SerializedProperty property)
        {
            if (property == null) return null;

            object obj = property.serializedObject.targetObject;
            var elements = property.GetFixedPropertyPath().Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("[", StringComparison.Ordinal));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[", StringComparison.Ordinal))
                        .Replace("[", "").Replace("]", ""));
                    obj = GetValueByArrayFieldName(obj, elementName, index);
                }
                else obj = GetValueByFieldName(obj, element);
            }

            return obj;


            object GetValueByArrayFieldName(object source, string name, int index)
            {
                if (!(GetValueByFieldName(source, name) is IEnumerable enumerable)) return null;
                var enumerator = enumerable.GetEnumerator();

                for (var i = 0; i <= index; i++)
                    if (!enumerator.MoveNext())
                        return null;
                return enumerator.Current;
            }

            // Search "source" object for a field with "name" and get it's value
            object GetValueByFieldName(object source, string name)
            {
                if (source == null) return null;
                var type = source.GetType();

                while (type != null)
                {
                    var fieldInfo = type.GetField(name,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (fieldInfo != null) return fieldInfo.GetValue(source);

                    var propertyInfo = type.GetProperty(name,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (propertyInfo != null) return propertyInfo.GetValue(source, null);

                    type = type.BaseType;
                }

                return null;
            }
        }

        private static string GetFixedPropertyPath(this SerializedProperty property) =>
            property.propertyPath.Replace(".Array.data[", "[");

        public static int GetUniquePropertyId(this SerializedProperty property)
            => property.serializedObject.targetObject.GetType().GetHashCode()
               + property.propertyPath.GetHashCode();
    }
}
#endif