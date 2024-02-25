namespace DebugX.Extensions
{
    public static class BoolExtension
    {
        public static int ToInt(this bool value, int trueValue, int falseValue) => value ? trueValue : falseValue;
    }
}