namespace LargestXOR
{
    internal class Program
    {
        private static uint LargestXORBad(uint[] inputs)
        {
            uint max = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int x = i; x < inputs.Length; x++)
                {
                    if ((inputs[i] ^ inputs[x]) > max)
                    {
                        max = inputs[i] ^ inputs[x];
                    }
                }
            }
            return max;
        }

        private class Trie()
        {
            public class Node(uint value)
            {
                public uint Value { get; set; } = value;
                public Node?[] Children { get; set; } = new Node?[2];
            }

            public Node Head { get; set; } = new(uint.MaxValue);

            public void Insert(uint value)
            {
                Node current = Head;

                for (int i = 0; i < 32; i++)
                {
                    byte bit = (byte)((value & (1 << i)) >> i);

                    current!.Children ??= new Node?[2];
                    current.Children[bit] ??= new Node(bit);

                    current = current.Children[bit]!;
                }
            }

        }

        //private static uint LargestXORBetter(uint[] inputs)
        //{
        //    Trie trie = new();

        //    for(int i = 0; i < inputs.Length; i++)
        //    {
        //        trie.Insert(inputs[i]);
        //    }

        //    uint leftValue = 0;
        //    uint rightValue = 0;
        //    Trie.Node left = trie.Head;
        //    Trie.Node right = trie.Head;

        //    for(int i = 0; i < 32; i++)
        //    {
        //        if(left.Children.Length == right.Children.Length)
        //        {
        //            if(left)
        //        }
        //    }
        //}

        private static void Main()
        {
            Random random = new(12345);
            var inputs = new uint[] { 2, 5, 7, 9, 14, 11 };

            Trie trie = new();

            //for(int i = 0; i < inputs.Length; i++)
            //{
            //    inputs[i] = (uint)random.Next(0, int.MaxValue);
            //    trie.Insert(inputs[i]);
            //}

            var output = LargestXORBad(inputs);

            ;
        }
    }
}