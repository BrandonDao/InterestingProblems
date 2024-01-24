namespace LargestXOR
{
    internal partial class Program
    {
        /// <summary>Wrapper for a ^ b = output</summary>
        public readonly struct XORInfo(uint a, uint b)
        {
            public readonly uint A = a;
            public readonly uint B = b;
            public readonly uint Output = a ^ b;

            public override string ToString() => $"{A} ^ {B} = {Output}";
        }
    }
}