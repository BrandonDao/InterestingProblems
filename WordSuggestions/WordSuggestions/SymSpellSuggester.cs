using LRUCache;
using System.Text;

namespace WordSuggestions
{
    public static class SymSpellSuggester
    {
        private static readonly Dictionary<string, HashSet<string>> termToSuggestedTerm = [];
        private static readonly Dictionary<string, TermInfo> termInfoByTerm = [];
        private static readonly LRUCache<string, List<TermInfo>> cache = new(capacity: 100);

        public static void AddDictionaryWord(StringBuilder strBuilder, string word)
        {
            termToSuggestedTerm.TryAdd(word, [word]);

            for (int i = 0; i < word.Length; i++)
            {
                strBuilder
                    .Append(word[..i])
                    .Append(word[(i + 1)..]);

                if (termToSuggestedTerm.TryGetValue(strBuilder.ToString(), out HashSet<string>? suggestedTerms))
                {
                    suggestedTerms.Add(word);
                }
                else
                {
                    termToSuggestedTerm.Add(strBuilder.ToString(), [word]);
                }

                strBuilder.Clear();
            }
        }

        public static List<TermInfo>? FindSuggestedWords(string input)
        {
            if (input.Length == 0) return null;

            if(cache.TryGetValue(input, out List<TermInfo>? cachedSuggestions)) return cachedSuggestions;

            HashSet<string> outputTerms = new(capacity: input.Length) { input };

            for (int i = 0; i < input.Length; i++)
            {
                outputTerms.Add(input[..i] + input[(i + 1)..]);
            }

            foreach (var relatedTerm in outputTerms)
            {
                if(FindSuggestedWordsHelper(relatedTerm, out List<TermInfo>? suggestions))
                {
                    cache.Put(input, suggestions!);
                    return suggestions!;
                }
            }

            return [];
        }


        private static bool FindSuggestedWordsHelper(string input, out List<TermInfo>? sortedSuggestions)
        {
            if (termToSuggestedTerm.TryGetValue(input, out HashSet<string>? suggestedTerms))
            {
                sortedSuggestions = [];
                foreach (string suggestion in suggestedTerms)
                {
                    if (termInfoByTerm.TryGetValue(suggestion, out TermInfo termInfo))
                    {
                        sortedSuggestions.Add(termInfo);
                    }
                    else
                    {
                        sortedSuggestions.Add(new TermInfo(input, suggestion));
                    }
                }
                sortedSuggestions.Sort(Comparer<TermInfo>.Create((x, y) => x.LevenshteinDistance.CompareTo(y.LevenshteinDistance)));

                return true;
            }
            sortedSuggestions = null;
            return false;
        }
    }
}