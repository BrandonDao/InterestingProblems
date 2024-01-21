using LRUCache;
using System.Text;
using WordSuggestions;

namespace WordSuggester
{
    public class WordSuggester
    {
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public HashSet<string> WordDictionary;

        private readonly LRUCache<string, List<TermInfo>> cache;

        public WordSuggester(string[] words)
        {
            WordDictionary = [];
            cache = new LRUCache<string, List<TermInfo>>(capacity: 100);


            foreach (var word in words)
            {
                WordDictionary.Add(word);
            }
        }

        public List<TermInfo> GetSuggestions(string input, int maxEditDistance, int maxSuggestionCount)
        {
            if (cache.TryGetValue(input, out List<TermInfo> suggestions)) return suggestions;
            if (WordDictionary.Contains(input)) return [new TermInfo(input, input)];

            suggestions = [];

            foreach(var word in WordDictionary)
            {
                if (Math.Abs(input.Length - word.Length) >= maxEditDistance) continue;

                if(WagnerFischerDistance(input, word) < maxEditDistance)
                {
                    suggestions.Add(new TermInfo(input, word));
                }

                if (suggestions.Count >= maxSuggestionCount) break;
            }

            suggestions.Sort(Comparer<TermInfo>.Create((x, y) => x.LevenshteinDistance.CompareTo(y.LevenshteinDistance)));

            cache.Put(input, suggestions);
            return suggestions;
        }


        private static int WagnerFischerDistance(string input, string target)
        {
            var matrix = new int[input.Length + 1, target.Length + 1];

            matrix[0, 0] = 0;

            for(int c = 1; c < target.Length; c++)
            {
                matrix[0, c] = c;
            }
            for(int r = 0; r < input.Length; r++)
            {
                matrix[r, 0] = r;
            }

            for(int r = 1; r < input.Length; r++)
            {
                for(int c = 1; c < target.Length; c++)
                {
                    matrix[r, c] = Math.Min(matrix[r - 1, c], Math.Min(matrix[r - 1, c - 1], matrix[r, c - 1]));

                    if (input[r - 1] != target[c - 1])
                    {
                        matrix[r, c]++;
                    }
                }
            }

            return matrix[input.Length - 1, target.Length - 1];
        }
    }
}