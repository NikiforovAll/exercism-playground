using System;
using System.Collections.Generic;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        PrimeGuard();
        int[] sieve = new int[limit + 1];
        return GeneratePrimes().ToArray();

        IEnumerable<int> GeneratePrimes()
        {
            var curr = 2;
            while (curr <= limit)
            {
                if (sieve[curr] == 0)
                {
                    yield return curr;
                    var j = curr;
                    while (j <= limit)
                    {
                        sieve[j] = 1;
                        j += curr;

                    }
                }
                curr++;
            }
        }
        void PrimeGuard()
        {
            if (limit < 2)
                throw new ArgumentOutOfRangeException(nameof(PrimeGuard));
        }
    }
}
