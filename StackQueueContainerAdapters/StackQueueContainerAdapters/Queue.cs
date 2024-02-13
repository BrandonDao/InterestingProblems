namespace StackQueueContainerAdapters
{
    internal partial class Program
    {
        public class Queue<T>(IContainer<T> container)
        {
            public int Count => container.Count;

            private readonly IContainer<T> container = container;

            public void Enqueue(T item) => container.AddLast(item);

            public T Dequeue()
            {
                T item = container.First;
                container.RemoveFirst();
                return item;
            }

            public T Peek() => container.First;

            public void Clear() => container.Clear();
        }
    }
}