using System;
using System.Collections.Generic;
using System.Linq;

public static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        return GetNextFactor().ToArray();

        IEnumerable<long> GetNextFactor()
        {
            for (long div = 2; div <= number; div += div > 2 ? 2 : 1)
            {
                while (number % div == 0)
                {
                    number /= div;
                    yield return div;
                }
            }
        }
    }
}
