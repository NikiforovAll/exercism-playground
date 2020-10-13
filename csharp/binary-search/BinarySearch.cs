using System;
using System.Diagnostics;

public static class BinarySearch
{
    public static int Find(int[] input, int value) =>
        BFind(input, value);

    private static int BFind<T>(ReadOnlySpan<T> source, T value)
        where T : IComparable
    {
        if (source.Length is 0)
            return -1;

        var mid_i = source.Length / 2;
        var mid = source[mid_i];
        return mid.CompareTo(value) switch
        {
            0 => mid_i,
            1 when BFind(source[..mid_i], value) is var ind && ind != 1
                => ind,
            -1 when BFind(source[(mid_i + 1)..], value) is var ind && ind != -1
                => mid_i + 1 + ind,
            _ => -1
        };
    }

}
