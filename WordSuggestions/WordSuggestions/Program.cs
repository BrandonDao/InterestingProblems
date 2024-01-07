namespace WordSuggestions
{
    internal class Program
    {
        private static HashSet<string> GenerateRelated(string input, int editDistance)
        {
            var outputs = new HashSet<string>();

            for (int i = 0; i < input.Length; i++)
            {
                outputs.Add(input[..i] + input[(i + 1)..]);
            }

            return outputs;
        }

        private static void AddWord(ref Dictionary<string, HashSet<string>> map, string word)
        {
            var relatedOutputs = GenerateRelated(word, 2);
            map.Add(word, [word]);

            foreach (var relatedOutput in relatedOutputs)
            {
                if (map.TryGetValue(relatedOutput, out HashSet<string>? list))
                {
                    list.Add(word);
                    continue;
                }

                map.Add(relatedOutput, [word]);
            }
        }

        private static void Main(string[] args)
        {
            Dictionary<string, HashSet<string>> inputToSuggestion = [];

            AddWord(ref inputToSuggestion, "airport");
            AddWord(ref inputToSuggestion, "apple");
            AddWord(ref inputToSuggestion, "fair");

            string input = "";

            while (true)
            {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[..(input.Length - 1)];
                }
                else
                {
                    input += keyInfo.KeyChar;
                }

                Console.Clear();
                Console.WriteLine(input);

                HashSet<string> relatedInputs = GenerateRelated(input, editDistance: 2);
                relatedInputs.Add(input);

                foreach (var relatedInput in relatedInputs)
                {
                    if (inputToSuggestion.TryGetValue(relatedInput, out HashSet<string>? list))
                    {
                        foreach(var word in list)
                        {
                            Console.Write($"{word}, ");
                        }
                        break;
                    }
                }

            }
        }
    }
}