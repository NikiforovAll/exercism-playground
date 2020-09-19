using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) =>
        Enumerable.Range(0, max)
            .Where(n => multiples.Where(m => m != 0).Any(m => n % m == 0))
            .Sum();
}
