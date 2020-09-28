using System;
using System.Linq;

public static class Raindrops
{
    public static string Convert(int number)
    {
        var factors = new int[] { 3, 5, 7 };
        return factors.Any(isDevisor)
                ? factors.Where(isDevisor)
                    .Select(ToDropString)
                    .Aggregate(string.Concat)
                : number.ToString();

        bool isDevisor(int i) => number % i == 0;
    }

    private static string ToDropString(int num) => num switch
    {
        var k when k % 3 == 0 => "Pling",
        var k when k % 5 == 0 => "Plang",
        var k when k % 7 == 0 => "Plong",
        _ => ""
    };
}
// var drop = Enumerable.Range(1, number)
//     .Select<int, string>(i => i switch
//     {
//         var k when k % 3 == 0 => "Pling",
//         var k when k % 5 == 0 => "Plang",
//         var k when k % 7 == 0 => "Plong",
//         _ => ""
//     }).Aggregate(string.Concat);
