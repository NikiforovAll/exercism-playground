using System.Numerics;
using System;

public static class Grains
{


    public static ulong Square(int num) => num switch
    {
        var n when n > 0 && n < 65 => (ulong)1 << (n - 1),
        _ => throw new ArgumentOutOfRangeException(nameof(num))
    };

    public static ulong Total() => (ulong)(BigInteger.Pow(2, 64) - 1);
}
