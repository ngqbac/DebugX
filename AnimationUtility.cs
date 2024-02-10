using System;
using DebugX.Attributes;
using DebugX.Internal;
using DG.Tweening;
using UnityEngine;

namespace DebugX.Runtime
{
    public static class AnimationUtility
    {
        public static void Move(GameObject gameObject, Vector3 target, Action completeHandle = null)
        {
            
        }

        public static void Move(GameObject gameObject, Vector3 target, float animTime, Action completeHandle = null)
        {
            
        }

        /// <summary>
        /// Scale up gameObject
        /// </summary>
        /// <param name="gameObject">GameObject need scale up</param>
        /// <param name="completeHandle">Action when complete animation</param>
        /// <param name="values">
        ///     A Vector for scale by all dimension.
        ///     A list float for scale by order x, y, z. Pass null for unused dimension
        /// </param>

        public static void ScaleUp(GameObject gameObject, Action completeHandle = null, params object[] values)
        {
            var paramsValue = AnimationInternal.GetParamsValue(values);
            
            if (paramsValue.IsType<Vector3>())
            {
                
            }

            if (paramsValue.IsType<Vector2>())
            {
                
            }

            if (paramsValue.IsType<float>())
            {
                
            }
        }
    }
}