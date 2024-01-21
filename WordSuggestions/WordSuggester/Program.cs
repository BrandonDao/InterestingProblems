using WordSuggestions;

namespace WordSuggester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WordSuggester suggestor = new(File.ReadAllLines(
                @"C:\Users\brand\Documents\Github\InterestingProblems\WordSuggestions\SharedLibrary\DictionaryWords.txt"));

            string input = "";
            Console.WriteLine($"Input:");

            while (true)
            {
                var keyInfo = Console.ReadKey();

                if ((keyInfo.KeyChar >= 'a' && keyInfo.KeyChar <= 'z') || (keyInfo.KeyChar >= 'A' && keyInfo.KeyChar <= 'Z'))
                {
                    input += keyInfo.KeyChar;
                }
                if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
                    {
                        input = "";
                    }
                    else
                    {
                        input = input[..(input.Length - 1)];
                    }
                }


                Console.Clear();
                Console.WriteLine($"Input:      {input}");
                
                if (input.Length == 0) continue;

                List<TermInfo>? suggestedTermInfos = suggestor.GetSuggestions(input, maxEditDistance: 2, maxSuggestionCount: 25);

                if (suggestedTermInfos == null) continue;

                Console.Write("Suggestion: ");
                foreach (var suggestedTermInfo in suggestedTermInfos)
                {
                    Console.Write($"{suggestedTermInfo.TargetTerm} ({suggestedTermInfo.LevenshteinDistance}), ");
                }
            }
        }
    }
}