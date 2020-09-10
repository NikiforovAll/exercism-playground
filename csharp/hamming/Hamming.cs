
using System;
using System.Collections.Generic;
using System.Linq;

public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand)
    {
        return firstStrand.Length == secondStrand.Length
            ? Solve(firstStrand, secondStrand)
            : throw new ArgumentException(nameof(Distance));

        static int Solve(IEnumerable<char> seq1, IEnumerable<char> seq2) =>
            seq1.Zip(seq2, (c1, c2) => c1.Equals(c2)).Count(i => !i);
    }
}
