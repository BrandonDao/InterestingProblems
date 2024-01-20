using LRUCache;
using SymSpell;
using System.Text;

namespace WordSuggestions
{
    public static class SymSpellSuggester
    {
        private static readonly Trie termToSuggestedTerm = new();
        private static readonly LRUCache<string, List<TermInfo>> cache = new(capacity: 100);

        public static void AddDictionaryWord(StringBuilder strBuilder, string word)
        {
            //termToSuggestedTerm.TryAdd(word, [word]);

            //for (int i = 0; i < word.Length; i++)
            //{
            //    strBuilder.Append(word);
            //    strBuilder.Remove(i, 1);

            //    string suggestion = strBuilder.ToString();

            //    if (termToSuggestedTerm.TryGetValue(suggestion, out List<string>? suggestedTerms))
            //    {
            //        suggestedTerms.Add(word);
            //    }
            //    else
            //    {
            //        termToSuggestedTerm.Add(suggestion, [word]);
            //    }

            //    strBuilder.Clear();
            //}

            termToSuggestedTerm.Insert(word, [word]);

            for (int i = 0; i < word.Length; i++)
            {
                if (word.Length <= 1) return;

                strBuilder.Append(word);
                strBuilder.Remove(i, 1);

                string suggestion = strBuilder.ToString();

                if (termToSuggestedTerm.TryGetValue(suggestion, out List<string>? suggestedTerms))
                {
                    suggestedTerms.Add(word);
                }
                else
                {
                    termToSuggestedTerm.Insert(suggestion, [word]);
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
                string variant = input[..i] + input[(i + 1)..];
                outputTerms.Add(variant);
            }

            foreach (var relatedTerm in outputTerms)
            {
                if(FindSuggestedWordsHelper(relatedTerm, input, out List<TermInfo>? suggestions))
                {
                    cache.Put(input, suggestions!);
                    return suggestions!;
                }
            }

            return [];
        }


        private static bool FindSuggestedWordsHelper(string relatedTerm, string input, out List<TermInfo>? sortedSuggestions)
        {
            if (termToSuggestedTerm.TryGetValue(relatedTerm, out List<string>? suggestedTerms))
            {
                sortedSuggestions = [];
                foreach (string suggestion in suggestedTerms)
                {
                    TermInfo info = new(input, suggestion);
                    sortedSuggestions.Add(info);
                }
                sortedSuggestions.Sort(Comparer<TermInfo>.Create((x, y) => x.LevenshteinDistance.CompareTo(y.LevenshteinDistance)));

                return true;
            }
            sortedSuggestions = null;
            return false;
        }
    }
}