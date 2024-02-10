using UnityEngine;
using System;

namespace DebugX.Attributes
{
    public class ValueWrapper
    {
        private readonly object _value;
        private readonly Type _valueType;

        private ValueWrapper(object value, Type valueType, int index = 0)
        {
            _value = value;
            _valueType = valueType;
            GetIndex = index;
        }

        public static ValueWrapper Create(Vector3 value, int index = 0) => new(value, typeof(Vector3), index);
        public static ValueWrapper Create(Vector2 value, int index = 0) => new(value, typeof(Vector2), index);
        public static ValueWrapper Create(int value, int index = 0) => new(value, typeof(int), index);
        public static ValueWrapper Create(float value, int index = 0) => new(value, typeof(float), index);
        public static ValueWrapper Create(string value, int index = 0) => new(value, typeof(string), index);
        public static ValueWrapper Create(bool value, int index = 0) => new(value, typeof(bool), index);
        public static ValueWrapper Create(object[] value, int index = 0) => new(value, typeof(object[]), index);
        public static ValueWrapper Create() => new(default, typeof(object), -1);

        public bool IsType<T>() => _valueType == typeof(T);

        public T GetValue<T>() => _valueType == typeof(T)
            ? (T)_value
            : throw new InvalidOperationException($"The value is not of type {typeof(T)}.");

        public int GetIndex { get; }
    }
}