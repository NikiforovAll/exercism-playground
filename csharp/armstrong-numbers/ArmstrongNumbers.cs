using System.Collections.Generic;
using System;
using System.Linq;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var query = StreamArmstrongPart(number);
        var N = query.Count();
        return query
            .Select(n => Math.Pow(n, N))
            .Sum() == number;

        static IEnumerable<long> StreamArmstrongPart(int number)
        {
            while (number % 10 > 0)
            {
                yield return number % 10;
                number /= 10;
            }
        }
    }
}
