namespace LargestXOR
{
    public class SpecializedTrie()
    {
        private const int BitCount = 32;

        public class Node(byte value)
        {
            public byte Value { get; set; } = value;
            public Node?[] Children { get; set; } = new Node?[2];
        }

        public Node Head { get; set; } = new(123);

        public void Insert(uint value)
        {
            Node current = Head;

            for (int i = BitCount - 1; i >= 0; i--)
            {
                byte bit = (byte)value.BitAt(i);

                current!.Children ??= new Node?[2];
                current.Children[bit] ??= new Node(bit);

                current = current.Children[bit]!;
            }
        }

        /// <returns>True if found exact, False otherwise</returns>
        public bool TryFindClosest(uint target, out uint closest)
        {
            Node current = Head;
            closest = 0;

            for (int bitIdx = BitCount - 1; bitIdx >= 0; bitIdx--)
            {
                byte targetBit = (byte)target.BitAt(bitIdx);

                Node? child = current.Children[targetBit];

                if (child is null)
                {
                    child = current.Children[^(targetBit + 1)];

                    if (child is null)
                    {
                        closest <<= bitIdx;
                        return false;
                    }
                }

                closest |= (uint)child.Value << bitIdx;
                current = child;
            }

            return closest == target;
        }
    }
}