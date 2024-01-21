namespace _5thElementFromLast
{
    internal class Program
    {
        private class LinkedListNode<T>(T value, LinkedListNode<T>? next)
        {
            public T Value { get; set; } = value;
            public LinkedListNode<T> Next { get; set; } = next;

            public override string ToString() => $"{Value} -> {(Next == null ? "null" : Next)}";
        }

        private static LinkedListNode<T> Find5thFromLast<T>(LinkedListNode<T> head)
        {
            static void Helper(LinkedListNode<T> current, ref LinkedListNode<T>? output, ref int distanceFromLast)
            {
                if (current.Next == null)
                {
                    distanceFromLast = 1;
                    return;
                }

                Helper(current.Next, ref output, ref distanceFromLast);
                distanceFromLast++;

                if (distanceFromLast == 5)
                {
                    output = current;
                }
            }

            int distance = -1;
            LinkedListNode<T>? output = null;

            Helper(head, ref output, ref distance);
            return output;
        }



        private static LinkedListNode<T> Find5thFromLastBetter<T>(LinkedListNode<T> head)
        {
            var current = head;
            var lookahead = current.Next.Next.Next.Next.Next;

            while (lookahead != null)
            {
                current = current.Next;
                lookahead = lookahead.Next;
            }
            return current;
        }

        private static void Main()
        {
            var output = Find5thFromLastBetter<int>(new(0, new(1, new(2, new(3, new(4, null))))));
            var output1 = Find5thFromLastBetter<int>(new(0, new(1, new(2, new(3, new(4, new(5, new(6, null))))))));

            ;
        }
    }
}