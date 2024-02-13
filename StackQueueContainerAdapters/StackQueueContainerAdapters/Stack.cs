namespace StackQueueContainerAdapters
{
    internal partial class Program
    {
        public class Stack<T>(IContainer<T> container)
        {
            public int Count => container.Count;

            private readonly IContainer<T> container = container;

            public void Push(T item) => container.AddLast(item);

            public T Pop()
            {
                T item = container.Last;
                container.RemoveLast();
                return item;
            }

            public T Peek() => container.Last;

            public void Clear() => container.Clear();
        }
    }
}