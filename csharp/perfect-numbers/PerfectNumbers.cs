using System;
using System.Collections.Generic;
using System.Linq;

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        if (number < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(Classification));
        }
        var sum = GenerateFactors(greedy: false)
            .Where(i => number % i == 0)
            .Sum();
        return sum switch
        {
            var c when c < number => Classification.Deficient,
            var c when c == number => Classification.Perfect,
            var c when c > number => Classification.Abundant
        };

        IEnumerable<int> GenerateFactors(bool greedy)
        {
            return greedy ? FullSequence() : OptimizedSequence();

            IEnumerable<int> OptimizedSequence()
            {
                for (int i = 1; i < Math.Sqrt(number); i++)
                {
                    var v = number / i;
                    if (v == number)
                        yield return 1;
                    if (v == i)
                    {
                        yield return i;
                    }
                    else if (v != number)
                    {
                        yield return i;
                        yield return v;
                    }
                }
            }
            IEnumerable<int> FullSequence() => Enumerable.Range(1, number - 1);
        }
    }
}
