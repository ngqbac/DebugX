using System;
using DebugX.Attributes;
using DebugX.Extensions;
using UnityEngine;

namespace DebugX.Internal
{
    public static class AnimationInternal
    {
        public static void SetPivot(GameObject gameObject, AnimationUtilityAttribute.PivotType pivotTarget)
        {
            var basePivot = gameObject.GetComponent<RectTransform>().pivot;
            if (basePivot != pivotTarget.GetVector())
            {
                var baseSize = gameObject.GetComponent<RectTransform>().rect.size;
                var basePos = gameObject.GetComponent<RectTransform>().localPosition;
                var deltaPivot = pivotTarget.GetVector().Subtract(basePivot);
                gameObject.GetComponent<RectTransform>().pivot = pivotTarget.GetVector();
                gameObject.GetComponent<RectTransform>().localPosition =
                    basePos.Add(new Vector3(deltaPivot.x * baseSize.x, deltaPivot.y * baseSize.y));
            }
        }

        public static ValueWrapper GetParamsValue(params object[] values)
        {
            if (Array.Exists(values, v => v is Vector3))
            {
                return ValueWrapper.Create((Vector3)Array.Find(values, v => v is Vector3));
            }
            else
            {
                var nonNull = Array.FindAll(values, v => v != null);

                switch (nonNull.Length)
                {
                    case 1:
                    {
                        int index = Array.FindIndex(values, v => v == nonNull[0]);
                        Debug.Log(nonNull[0] + " at " + index);
                        break;
                    }

                    case 2:
                    {
                        return ValueWrapper.Create(new Vector2((float)nonNull[0], (float)nonNull[1]));
                    }

                    case 3:
                    {
                        return ValueWrapper.Create(new Vector3((float)nonNull[0], (float)nonNull[1],
                            (float)nonNull[3]));
                    }
                }
            }
            
            return ValueWrapper.Create();
        }
    }
}