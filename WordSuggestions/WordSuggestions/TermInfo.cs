namespace WordSuggestions
{
    public readonly struct TermInfo(string term, string targetTerm)
    {
        public readonly string Term = term;
        public readonly string TargetTerm = targetTerm;
        public readonly int LevenshteinDistance = FindLevenshteinDistance(term, targetTerm);

        public static int FindLevenshteinDistance(string a, string b)
        {
            if (a.Length == 0) return b.Length;
            if (b.Length == 0) return a.Length;

            if (a[0] == b[0]) return FindLevenshteinDistance(a[1..], b[1..]);

            return 1 + Math.Min(
                FindLevenshteinDistance(a[1..], b),
                Math.Min(
                    FindLevenshteinDistance(a, b[1..]),
                    FindLevenshteinDistance(a[1..], b[1..])));
        }
    }
}