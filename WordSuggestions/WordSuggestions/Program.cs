namespace WordSuggestions
{
    internal partial class Program
    {
        private static void Main()
        {
            string[] dictionaryWords = File.ReadAllLines(
                @"C:\Users\brand\Documents\Github\InterestingProblems\WordSuggestions\WordSuggestions\DictionaryWords.txt");

            foreach (var word in dictionaryWords)
            {
                WordSuggester.AddDictionaryWord(word);
            }

            string input = "";

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
                Console.WriteLine(input);

                List<TermInfo>? suggestedTermInfos = WordSuggester.FindSuggestedWords(input);
               
                if (suggestedTermInfos == null) continue;

                foreach (var suggestedTermInfo in suggestedTermInfos)
                {
                    Console.Write($"{suggestedTermInfo.TargetTerm} ({suggestedTermInfo.LevenshteinDistance}), ");
                }
            }
        }
    }
}