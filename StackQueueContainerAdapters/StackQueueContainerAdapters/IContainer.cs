namespace StackQueueContainerAdapters
{
    internal partial class Program
    {
        public interface IContainer<T>
        {
            public int Count { get; }
            public T First { get; }
            public T Last { get; }
            public void AddFirst(T item);
            public void AddLast(T item);
            public void RemoveFirst();
            public void RemoveLast();
            public void Clear();
        }
    }
}