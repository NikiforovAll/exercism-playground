using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

[Flags]
public enum Allergen
{
    None = 0,
    Eggs = 0b_0000_0001,
    Peanuts = 0b_0000_0010,
    Shellfish = 0b_0000_0100,
    Strawberries = 0b_0000_1000,
    Tomatoes = 0b_0001_0000,
    Chocolate = 0b_0010_0000,
    Pollen = 0b_0100_0000,
    Cats = 0b_1000_0000
}

public class Allergies
{
    private readonly int mask;
    private readonly Lazy<Allergen[]> scanner;
    public Allergies(int mask)
    {
        this.mask = mask & MAX_MASK;
        scanner = BuildScanner(this.mask);
    }

    private static readonly int MAX_MASK = BuildMask<Allergen>();

    public bool IsAllergicTo(Allergen allergen) =>
        (((Allergen)mask) & allergen) == allergen;
    public Allergen[] List() => scanner.Value;

    private static Lazy<Allergen[]> BuildScanner(int mask)
    {
        return new Lazy<Allergen[]>(() =>
        ScanForAllergens(mask)
            .Select((a, i) =>
                a % 2 == 0
                    ? Allergen.None
                    : (Allergen)(1 << i))
            .Where(a => a != Allergen.None)
            .ToArray());

        static IEnumerable<byte> ScanForAllergens(int mask)
        {
            while (mask > 0)
            {
                yield return (byte)(mask % 2);
                mask >>= 1;
            }
        }
    }

    private static int BuildMask<T>() where T : Enum
    {
        Debug.Assert(typeof(T).GetCustomAttribute<FlagsAttribute>() != null);
        var lastEnumFlag = Enum.GetValues(typeof(T)).Cast<int>().Max();
        var allFlags = (lastEnumFlag << 1) - 1;
        return allFlags;
    }
}
