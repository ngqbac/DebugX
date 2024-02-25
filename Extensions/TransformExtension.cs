using System.Linq;
using UnityEngine;

namespace DebugX.Extensions
{
    public static class TransformExtension
    {
        public static void SetActiveChildren(this Transform parent, bool active = true) => parent.Cast<Transform>()
            .ToList().ForEach(child => child.gameObject.SetActive(active));
    }
}
