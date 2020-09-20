using System;
using System.Collections.Generic;
public static class PythagoreanTriplet
{

    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        return TripletsWithSumBrute(sum);
        // foreach (var t in TripletsWIthSumEuclidGeneralized(sum))
        // {
        //     var (a, b, c) = t;
        //     if (a + b + c == sum && a * a + b * b == c * c)
        //         yield return t;
        // }

    }
    /// <summary>
    /// Brute force approach
    /// </summary>
    /// <param name="sum"></param>
    private static IEnumerable<(int a, int b, int c)> TripletsWithSumBrute(int sum)
    {
        for (int i = 1; i < sum / 3; i++)
        {
            for (int j = i + 1; j < sum / 2; j++)
            {
                int c = sum - i - j;
                if (c > j && c * c == i * i + j * j)
                    yield return (i, j, c);
                // for (int k = j + 1; k <= sum / 2; k++)
                // {
                //     yield return (i, j, k);
                // }
            }
        }
    }

    /// <summary>
    /// The solution is based on Euclid's formula
    /// a = m ^ 2 - n ^ 2
    /// b = = 2 * m * n
    /// c = m ^ 2 + n ^ 2
    /// m > n > 0
    /// </summary>
    /// <param name="sum">a + b + c = sum</param>
    private static IEnumerable<(int a, int b, int c)> TripletsWithSumEuclid(int sum)
    {
        static bool probe(int m, int n, int c) => m * (m + 2 * n) < c;

        for (int n = 1; n < sum; n++)
        {
            for (int m = n + 1; probe(m, n, sum); m++)
            {
                int mm = m * m, nn = n * n;
                var ans = (a: mm - nn, b: 2 * m * n, c: mm + nn);
                // if (ans.a + ans.b + ans.c == sum)
                yield return ans;
            }
        }
    }

    private static IEnumerable<(int a, int b, int c)> TripletsWIthSumEuclidGeneralized(int sum)
    {
        throw new NotImplementedException();
    }

    private static int GCD(int x, int y)
    {
        (x, y) = (x > y ? x : y, x > y ? y : x);
        while (x % y != 0)
            (x, y) = (y, x % y);
        return y;
    }

}
