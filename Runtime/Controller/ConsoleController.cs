using System;
using DebugX.Attributes;
using MyBox;
using UnityEngine;

namespace DebugX.Console
{
    [RequireComponent(typeof(ConsoleExtension))]
    public class ConsoleController : MonoBehaviour
    {
        public static ConsoleController Instance;

        public bool isOn;

        [ConditionalField(nameof(isOn))] public KeyCode toggleKey = KeyCode.BackQuote;
        [ConditionalField(nameof(isOn))] public bool shakeToOpen;

        [ConditionalField(nameof(shakeToOpen))]
        public float shakeAcceleration = 3f;
        
        private void Awake()
        {
            name = "Console";
            Instance = this;
            DontDestroyOnLoad(this);
            ConsoleAttribute.IsOn = isOn;
            ConsoleAttribute.Initialized = true;
        }

        private void Update()
        {
            if (!isOn) return;
            if (Input.GetKeyDown(toggleKey) || (shakeToOpen && Input.acceleration.sqrMagnitude > shakeAcceleration * 2f))
            {
                TurnOn();
            }
        }

        private void TurnOn()
        {
            GetComponent<ConsoleExtension>().TurnOn();
        }

        public void TurnOff()
        {
            GetComponent<ConsoleExtension>().TurnOff();
        }
    }
}
