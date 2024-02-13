namespace Waterpipes
{
    internal class Program
    {
        public static int FindVolume(int[] pipes)
        {
            int leftIdx = 0, rightIdx = pipes.Length - 1, leftMaxIdx = 0, rightMaxIdx = 0, volume = 0;

            bool isCurrentlyLeft = pipes[leftIdx] <= pipes[rightIdx];

            while (rightIdx - leftIdx > 1)
            {
                if (isCurrentlyLeft)
                {
                    leftIdx++;

                    if (pipes[leftIdx] >= pipes[leftMaxIdx])
                    {
                        leftMaxIdx = leftIdx;
                        if (pipes[leftMaxIdx] > pipes[rightMaxIdx])
                        {
                            isCurrentlyLeft = !isCurrentlyLeft;
                            continue;
                        }
                    }

                    volume += pipes[leftMaxIdx] - pipes[leftIdx];
                }
                else
                {
                    rightIdx--;

                    if (pipes[rightIdx] >= pipes[rightMaxIdx])
                    {
                        rightMaxIdx = rightIdx;
                        if (pipes[rightMaxIdx] > pipes[leftMaxIdx])
                        {
                            isCurrentlyLeft = !isCurrentlyLeft;
                            continue;
                        }
                    }

                    volume += pipes[rightMaxIdx] - pipes[rightIdx];
                }
            }
            return volume;
        }

        private static void Main(string[] args)
        {
            const int n = 10;
            const int maxHeight = 8;
            var pipes = new int[n];

            var random = new Random(3956);

            for (int i = 0; i < n; i++)
            {
                pipes[i] = random.Next(0, maxHeight + 1);
            }

            for (int i = 0; i < pipes.Length; i++)
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