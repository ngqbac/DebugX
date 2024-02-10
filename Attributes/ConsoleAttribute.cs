using DebugX.Console;

namespace DebugX.Attributes
{
    public static class ConsoleAttribute
    {
        public static bool Initialized;
        private static bool _isOn;

        public static bool IsOn
        {
            get => _isOn;
            set
            {
                if (value == _isOn) return;
                _isOn = value;
                if (Initialized)
                {
                    ConsoleController.Instance.isOn = value;
                    if (value == false)
                    {
                        ConsoleController.Instance.TurnOff();
                    }
                }
            }
        }
    }
}
