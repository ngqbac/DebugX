using System;

namespace DebugX.Extensions
{
    public static class EnumExtension
    {
        public static int GetIndex(this Enum value) => Array.IndexOf(Enum.GetValues(value.GetType()), value);

        public static int GetFlagValue(this Enum value) => 1 << Convert.ToInt32(value);
    }
}