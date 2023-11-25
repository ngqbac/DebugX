using System;
using DebugX.Attribute;
using DebugX.Internal;
using MyBox;
using UnityEngine;

namespace DebugX
{
    public class LogUtilityController : MonoBehaviour
    {
        public static LogUtilityController Instance;

        public bool isOn;
        
        [ConditionalField(nameof(isOn))] public ColorCollection color = new();

        private void Awake()
        {
            name = "LogUtility";
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(this);
                }
            }
            LogUtilityAttribute.TypeColor = color.Value;
            LogUtilityAttribute.IsOn = isOn;
            LogUtilityAttribute.Initialized = true;
        }

        #region ValidateEditor

        private void OnValidate()
        {
            if (InternalUtility.CompareColor32(color.Value[0], new Color32()))
            {
                color.Value[0] = InternalUtility.GetColor("008000");
            }
            
            if (InternalUtility.CompareColor32(color.Value[1], new Color32()))
            {
                color.Value[1] = InternalUtility.GetColor("87CEEB");
            }
            
            if (InternalUtility.CompareColor32(color.Value[2], new Color32()))
            {
                color.Value[2] = InternalUtility.GetColor("FFA500");
            }
            
            if (InternalUtility.CompareColor32(color.Value[3], new Color32()))
            {
                color.Value[3] = InternalUtility.GetColor("FF69B4");
            }
            
            if (InternalUtility.CompareColor32(color.Value[4], new Color32()))
            {
                color.Value[4] = InternalUtility.GetColor("FF0000");
            }
        }

        #endregion
    }

    [Serializable]
    public class ColorCollection : CollectionWrapper<Color32>
    {
        public ColorCollection()
        {
            Value = new Color32[5];
        }
    }
}