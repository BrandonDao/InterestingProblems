namespace LRUCache
{
    public class KeyValuePair<TKey, TValue>(TKey key, TValue value)
    {
        public TKey Key { get; set; } = key;
        public TValue Value { get; set; } = value;
    }
    
    public class LRUCache<TKey, TValue>(int capacity) : ICache<TKey, TValue>
    {
        public int Capacity { get; private set; } = capacity;
        public int Count => list.Count;

        private readonly LinkedList<KeyValuePair<TKey, TValue>> list = [];
        private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> dictionary = [];

        public void Put(TKey key, TValue value)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> node;

            if (dictionary.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>>? kvp))
            {
                node = kvp;
                list.Remove(node.Value);
            }
            else
            {
                if (Count + 1 > Capacity)
                {
                    dictionary.Remove(list.Last.Value.Key);
                    list.RemoveLast();
                }

                node = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
                dictionary.Add(key, node);
            }

            node.Value.Value = value;

            list.AddFirst(node);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (!dictionary.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>>? kvp)) return false;

            LinkedListNode<KeyValuePair<TKey, TValue>> node = kvp;

            value = node.Value.Value;
            list.Remove(node);
            list.AddFirst(node);

            return true;
        }
    }
}