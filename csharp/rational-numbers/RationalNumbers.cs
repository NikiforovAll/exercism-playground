using System;
using System.Diagnostics;

using static System.Math;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) =>
        Pow(Pow(realNumber, r.numerator), 1D / r.denominator);
}

public struct RationalNumber
{
    public readonly int numerator;
    public readonly int denominator;

    public RationalNumber(int numerator, int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(
            r1.numerator * r2.denominator + r2.numerator * r1.denominator,
            r1.denominator * r2.denominator
        ).Reduce();

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(
            r1.numerator * r2.denominator - r2.numerator * r1.denominator,
            r1.denominator * r2.denominator
        ).Reduce();


    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2) =>
        new RationalNumber(
            r1.numerator * r2.numerator,
            r1.denominator * r2.denominator
        ).Reduce();

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2) =>
    new RationalNumber(
            r1.numerator * r2.denominator * r2.Sign(),
            r1.denominator * Math.Abs(r2.numerator)
        ).Reduce();

    public RationalNumber Abs() =>
        new RationalNumber(Math.Abs(numerator), Math.Abs(denominator));

    public RationalNumber Reduce()
    {
        int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
        return new RationalNumber(
            Math.Abs(numerator / gcd) * Sign(),
            Math.Abs(denominator / gcd));
    }

    public RationalNumber Exprational(int power) =>
        new RationalNumber(
            (int)Pow(numerator, power),
            (int)Pow(denominator, power)).Reduce();


    // public double Expreal(int baseNumber)
    // {
    //     throw new NotImplementedException("You need to implement this function.");
    // }

    private int Sign() =>
        Math.Sign(numerator) * Math.Sign(denominator);

    private int GCD(int a, int b)
    {
        (a, b) = a > b ? (a, b) : (b, a);
        while (b != 0)
            (a, b) = (b, a % b);
        return a;
    }
}
