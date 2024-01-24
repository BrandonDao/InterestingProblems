using System.Diagnostics;

namespace LargestXOR
{
    internal partial class Program
    {
        private static XORInfo LargestXORBad(uint[] inputs)
        {
            XORInfo maxInfo = new(inputs[0], inputs[0]);

            for (int i = 1; i < inputs.Length; i++)
            {
                for (int x = i; x < inputs.Length; x++)
                {
                    XORInfo currentInfo = new(inputs[i], inputs[x]);
                    if (currentInfo.Output > maxInfo.Output)
                    {
                        maxInfo = currentInfo;
                    }
                }
            }
            return maxInfo;
        }

        private static XORInfo LargestXORGood(uint[] inputs)
        {
            SpecializedTrie trie = new();
            var NOTinputs = new uint[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                NOTinputs[i] = ~inputs[i];
                trie.Insert(inputs[i]);
            }

            XORInfo maxInfo = new(inputs[0], inputs[0]);
            for (int i = 1; i < NOTinputs.Length; i++)
            {
                bool hasFoundExact = trie.TryFindClosest(NOTinputs[i], out uint closest);

                XORInfo currentInfo = new(inputs[i], closest);

                if (hasFoundExact) return currentInfo;

                if(currentInfo.Output > maxInfo.Output)
                {
                    maxInfo = currentInfo;
                }
            }
            return maxInfo;
        }

        private static void Main()
        {
            Random random = new();
            var inputs = new uint[50_000];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = (uint)random.Next(0, int.MaxValue);
            }

            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine(LargestXORGood(inputs) + $" ({stopwatch.ElapsedMilliseconds} ms)");

            stopwatch.Restart();

            Console.WriteLine(LargestXORBad(inputs) + $" ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}