namespace MergeKSortedLists
{
    internal class Program
    {
        private class LinkedListNode<T>(T value, LinkedListNode<T>? next)
        {
            public T Value { get; set; } = value;
            public LinkedListNode<T>? Next { get; set; } = next;

            public override string ToString() => $"{Value} -> {(Next == null ? "null" : Next)}";
        }

        private static LinkedListNode<int> MergeKSortedLists(List<LinkedListNode<int>> inputs)
        {
            PriorityQueue<LinkedListNode<int>, int> heap = new();

            LinkedListNode<int> outputHeadPtr = new(0xcafe, next: null);
            LinkedListNode<int> outputTail = outputHeadPtr;

            for (int i = 0; i < inputs.Count; i++)
            {
                heap.Enqueue(inputs[i], inputs[i].Value);
            }

            while (heap.Count > 0)
            {
                outputTail.Next = heap.Dequeue();
                outputTail = outputTail.Next;

                if (outputTail.Next != null)
                {
                    heap.Enqueue(outputTail.Next, outputTail.Next.Value);
                }
            }
            return outputHeadPtr.Next!;
        }

        private static void Main(string[] args)
        {
            var test1 = new List<LinkedListNode<int>>()
            {
                new(-1, new(3, new(5, null))),
                new(2, new(4, new(5, new(5, null)))),
                new(-0, new(6, null)),
            };

            var test1Output = MergeKSortedLists(test1);


            ;
        }
    }
}