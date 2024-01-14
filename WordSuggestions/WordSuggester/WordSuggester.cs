using System.Text;

namespace WordSuggester
{
    public class WordSuggester
    {
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public HashSet<string> WordDictionary;

        public WordSuggester(string[] words)
        {
            WordDictionary = [];
            
            foreach(var word in words)
            {
                WordDictionary.Add(word);
            }
        }

        public List<string> GetSuggestions(string input, int maxEditDistance)
        {
            List<string> suggestions = new(capacity: (Alphabet.Length << 2) * input.Length + Alphabet.Length - 1);
            StringBuilder strBuilder = new();

            for(int i = 0; i < input.Length; i++)
            {
                string suggestion = strBuilder.Append(input).Remove(i, 1).ToString();

                if (WordDictionary.Contains(suggestion)) suggestions.Add(suggestion);

                strBuilder.Clear();
            }

            for(int i = 0; i < input.Length - 1; i++)
            {
                strBuilder.Append(input);

                string temp = strBuilder[i].ToString();
                strBuilder[i] = strBuilder[i + 1];
                strBuilder[i + 1] = temp[0];

                string suggestion = strBuilder.ToString();

                if (WordDictionary.Contains(suggestion)) suggestions.Add(suggestion);

                strBuilder.Clear();
            }

            for(int i = 0; i < input.Length; i++)
            {
                for (int alphabetIdx = 0; alphabetIdx < Alphabet.Length; alphabetIdx++)
                {
                    strBuilder.Append(input)[i] = Alphabet[alphabetIdx];
                    string suggestion = strBuilder.ToString();

                    if (WordDictionary.Contains(suggestion)) suggestions.Add(suggestion);

                    strBuilder.Clear();
                }
            }

            for(int i = 0; i < input.Length + 1; i++)
            {
                for(int alphabetIdx = 0; alphabetIdx < Alphabet.Length; alphabetIdx++)
                {
                    strBuilder.Append(input[])
                }
            }

            return suggestions;
        }
    }
}