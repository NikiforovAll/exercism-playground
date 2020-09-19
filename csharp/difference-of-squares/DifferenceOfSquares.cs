using System;
using System.Linq;

public static class DifferenceOfSquares
{
    /// <summary>
    /// (1+2+...)^2 = (n*(n+1)/2)^2
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int CalculateSquareOfSum(int max)
    {
        return Enumerable.Repeat(NaturalNumberSeriesSum(max), 2)
            .Aggregate((a1, a2) => a1 * a2);
        static int NaturalNumberSeriesSum(int n) => ((n * (n + 1)) << 1) / 4;
    }

    public static int CalculateSumOfSquares(int max) =>
        Enumerable.Range(1, max).Aggregate((prev, curr) => curr * curr + prev);

    public static int CalculateDifferenceOfSquares(int max)
    {
        var naturals = Enumerable.Range(1, max);
        return 2 * naturals.SelectMany(i =>
            naturals.SkipWhile(j => j <= i).Select(j => i * j)).Sum();
    }

    public static int CalculateDifferenceOfSquares1(int max)
    {
        var acc = 0;
        max++;
        for (int i = 1; i < max; i++)
        {
            for (int j = i + 1; j < max; j++)
            {
                acc += i * j;
            }
        }
        return 2 * acc;
    }

    public static int CalculateDifferenceOfSquares2(int max) =>
        CalculateSquareOfSum(max) - CalculateSumOfSquares(max);
}
