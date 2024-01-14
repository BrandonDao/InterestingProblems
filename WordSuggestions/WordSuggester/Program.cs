namespace WordSuggester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordSuggester suggestor = new(File.ReadAllLines(
                @"C:\Users\brand\Documents\Github\InterestingProblems\WordSuggestions\SharedLibrary\DictionaryWords.txt"));

            var suggestions = suggestor.GetSuggestions("ariplane", 1);

            ;
        }
    }
}