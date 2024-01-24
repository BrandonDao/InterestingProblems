namespace LargestXOR
{
    public static class ExtensionMethods
    {
        public static long BitAt(this uint value, int bitIndex) => (value & (1 << bitIndex)) >> bitIndex;
    }
}
