namespace @bool
{
    internal class Program
    {
        private static unsafe void Main()
        {
            HashSet<bool> cry = [];

            byte byte1 = 5;
            byte byte2 = 0;
            byte byte3 = 42;

            cry.Add(*(bool*)&byte1);
            cry.Add(*(bool*)&byte2);
            cry.Add(*(bool*)&byte3);

            ;

            int count = cry.Count;  // 3 !!!
            foreach (bool @bool in cry)
            {
                Console.WriteLine(@bool);    // True, False, True
            }
        }
    }
}