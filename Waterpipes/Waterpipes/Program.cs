namespace Waterpipes
{
    internal class Program
    {
        [Flags]
        private enum CellType : byte
        {
            Pipe = 1,
            Visited = 2
        }


        public static int FindVolume(int[] pipes)
        {
            int currIdx = 0;
            int volume = 0;

            while (pipes[currIdx] <= pipes[currIdx + 1])
            {
                if (currIdx + 2 >= pipes.Length) return 0;

                currIdx++;
            }

            while (currIdx < pipes.Length - 1)
            {
                int max = 0;
                int maxIdx = 0;
                int sum = 0;

                for (int i = currIdx + 1; i < pipes.Length; i++)
                {
                    if (pipes[i] >= max)
                    {
                        maxIdx = i;
                        max = pipes[maxIdx];

                        if (pipes[maxIdx] >= pipes[currIdx] || maxIdx == pipes.Length - 1) break;
                    }

                    sum += pipes[i];
                }

                if (maxIdx != currIdx + 1)
                {
                    volume += (maxIdx - currIdx - 1) * Math.Min(pipes[currIdx], pipes[maxIdx]) - sum;
                }

                currIdx = maxIdx;
            }

            return volume;
        }

        private static void Main(string[] args)
        {
            const int n = 4;
            const int maxHeight = 5;
            var pipes = new int[n];

            //var random = new Random(5834256);

            //for (int i = 0; i < n; i++)
            //{
            //    pipes[i] = random.Next(0, maxHeight + 1);
            //}

            pipes = [4, 2, 3, 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < pipes[i]; j++)
                {
                    Console.Write("[]");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Volume: {FindVolume(pipes)}");
        }
    }
}
