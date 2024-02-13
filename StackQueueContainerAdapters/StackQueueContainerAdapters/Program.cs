namespace StackQueueContainerAdapters
{
    internal partial class Program
    {
        public class ListWrapper<T>(List<T> list) : IContainer<T>
        {
            private readonly List<T> list = list;

            public int Count => list.Count;
            public T First { get => list[0]; }
            public T Last { get => list[^1]; }

            public void AddFirst(T item) => list.Insert(0, item);
            public void AddLast(T item) => list.Add(item);
            public void RemoveFirst() => list.RemoveAt(0);
            public void RemoveLast() => list.RemoveAt(list.Count - 1);
            public void Clear() => list.Clear();
        }

        private static void Main(string[] args)
        {
            ListWrapper<int> list = new([1, 2, 3, 4]);

            Queue<int> q = new(list);

            q.Enqueue(5);
            var a = q.Peek();
            var b = q.Dequeue();
            var c = q.Dequeue();


        }
    }
}