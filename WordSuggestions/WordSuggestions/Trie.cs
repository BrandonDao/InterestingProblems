using System.Diagnostics.CodeAnalysis;

namespace SymSpell
{
    public class Trie()
    {
        public interface INode
        {
            public Dictionary<char, INode> Children { get; }
        }

        public class PrefixNode(List<string> suggestedWords) : INode
        {
            public Dictionary<char, INode> Children { get; set; } = [];
            public List<string> SuggestedWords { get; set; } = suggestedWords;
        }
        public class WordNode(List<string> suggestedWords) : INode
        {
            public Dictionary<char, INode> Children { get; set; } = [];
            public List<string> SuggestedWords { get; set; } = suggestedWords;
        }



        private readonly INode head = new PrefixNode(null);

        public void Insert(string word, List<string> suggestedWords)
        {
            INode current = head;
            INode? child;
            int wordIdx = 0;

            for(; wordIdx < word.Length - 1; wordIdx++)
            {
                if (!current.Children.TryGetValue(word[wordIdx], out child))
                {
                    child = new PrefixNode(null);
                    current.Children.Add(word[wordIdx], child);
                }

                current = child;
            }

            if (current.Children.TryGetValue(word[^1], out child))
            {
                PrefixNode prefixChild = ((PrefixNode)child);

                if(prefixChild.SuggestedWords != null)
                {
                    prefixChild.SuggestedWords.AddRange(suggestedWords);
                }
                else
                {
                    prefixChild.SuggestedWords = suggestedWords;
                }
            }
            else
            {
                current.Children.TryAdd(word[^1], new PrefixNode(suggestedWords));
            }
        }

        public bool TryGetValue(string word, [MaybeNullWhen(false)] out List<string> value)
        {
            INode current = head;

            for (int wordIdx = 0; wordIdx < word.Length; wordIdx++)
            {
                if (!current.Children.TryGetValue(word[wordIdx], out INode? child))
                {
                    value = null;
                    return false;
                }

                current = child;
            }

            value = ((PrefixNode)current).SuggestedWords;
            return value != null;
        }
    }
}