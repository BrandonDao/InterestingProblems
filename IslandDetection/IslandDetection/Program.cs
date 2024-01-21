using System.Data.SqlTypes;

namespace IslandDetection
{
    internal class Program
    {
        /*
        private static int FindIslandCount(int[,] map)
        {
            HashSet<(int, int)> visitedSet = [];

            int counter = 0;

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (visitedSet.Contains((r, c))) continue;

                    visitedSet.Add((r, c));

                    if (map[r, c] == 1)
                    {
                        Explore(r, c, map, visitedSet);
                        counter++;
                    }
                }
            }

            return counter;
        }

        private static readonly (int, int)[] lookupOffsets = [(0, 1), (0, -1), (1, 0), (-1, 0)];

        private static void Explore(int r, int c, int[,] map, HashSet<(int, int)> visitedSet)
        {
            foreach (var lookupOffset in lookupOffsets)
            {
                int rLookup = r + lookupOffset.Item1;
                int cLookup = c + lookupOffset.Item2;

                if (visitedSet.Contains((rLookup, cLookup))) continue;
                if (rLookup < 0 || rLookup >= map.GetLength(0) || cLookup < 0 || cLookup >= map.GetLength(1)) continue;

                visitedSet.Add((rLookup, cLookup));

                if (map[rLookup, cLookup] == 1)
                {
                    Explore(rLookup, cLookup, map, visitedSet);
                }
            }
        }
        */

        private static (int, int)[] FindLargestIsland(int[,] map)
        {
            HashSet<(int, int)> visitedSet = [];
            HashSet<(int, int)> currentIslandSet = [];
            (int, int)[] largestIslandSet = [];

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (visitedSet.Contains((r, c))) continue;

                    visitedSet.Add((r, c));

                    if (map[r, c] == 1)
                    {
                        currentIslandSet.Add((r, c));
                        Explore(r, c, map, visitedSet, currentIslandSet);

                        if(currentIslandSet.Count > largestIslandSet.Length)
                        {
                            largestIslandSet = new (int, int)[currentIslandSet.Count];
                            currentIslandSet.CopyTo(largestIslandSet);
                        }
                        currentIslandSet.Clear();
                    }
                }
            }

            return largestIslandSet;
        }

        private static readonly (int, int)[] lookupOffsets = [(0, 1), (0, -1), (1, 0), (-1, 0)];

        private static void Explore(int r, int c, int[,] map, HashSet<(int, int)> visitedSet, HashSet<(int, int)> currentIslandSet)
        {
            foreach (var lookupOffset in lookupOffsets)
            {
                int rLookup = r + lookupOffset.Item1;
                int cLookup = c + lookupOffset.Item2;

                if (visitedSet.Contains((rLookup, cLookup))) continue;
                if (rLookup < 0 || rLookup >= map.GetLength(0) || cLookup < 0 || cLookup >= map.GetLength(1)) continue;

                visitedSet.Add((rLookup, cLookup));

                if (map[rLookup, cLookup] == 1)
                {
                    currentIslandSet.Add((rLookup, cLookup));
                    Explore(rLookup, cLookup, map, visitedSet, currentIslandSet);
                }
            }
        }


        private static void Main(string[] args)
        {
            var test1 = new int[,]
            {
                { 0, 1, 0 },
                { 1, 0, 1 },
                { 0, 1, 1 }
            };
            var test2 = new int[,]
            {
                { 0, 0, 1, 0 },
                { 0, 1, 1, 0 },
                { 1, 0, 0, 0 },
                { 0, 1, 1, 1 },
            };
            var test3 = new int[,]
            {
                { 1, 1 },
                { 0, 1 },
            };
            var test4 = new int[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 },
            };
            var test5 = new int[,]
            {
                { 1, 0, 0, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 1, 0, 0, 1 },
            };
            var test3Result = FindLargestIsland(test3);

            var test1Result = FindLargestIsland(test1);
            
            var test2Result = FindLargestIsland(test2);


            var test4Result = FindLargestIsland(test4);

            var test5Result = FindLargestIsland(test4);

            ;
        }
    }
}