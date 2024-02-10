using System.Collections.Generic;
using UnityEngine;

namespace DebugX.Attributes
{
    public static class AnimationUtilityAttribute
    {
        public static float BaseAnimationTime = 0.3f;
        public static float BaseDelayTime = 0.05f;

        public enum PivotType
        {
            UpperLeft,
            UpperCenter,
            UpperRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            LowerLeft,
            LowerCenter,
            LowerRight
        }

        private static readonly Dictionary<PivotType, Vector2> PivotVector = new()
        {
            { PivotType.UpperLeft, new Vector2(0f, 1f) },
            { PivotType.UpperCenter, new Vector2(0.5f, 1f) },
            { PivotType.UpperRight, new Vector2(1f, 1f) },
            { PivotType.MiddleLeft, new Vector2(0f, 0.5f) },
            { PivotType.MiddleCenter, new Vector2(0.5f, 0.5f) },
            { PivotType.MiddleRight, new Vector2(1f, 0.5f) },
            { PivotType.LowerLeft, new Vector2(0f, 0f) },
            { PivotType.LowerCenter, new Vector2(0.5f, 0f) },
            { PivotType.LowerRight, new Vector2(1f, 0f) }
        };

        public static Vector2 GetVector(this PivotType pivot)
        {
            return PivotVector.TryGetValue(pivot, out var vector) ? vector : Vector2.zero;
        }
    }
}