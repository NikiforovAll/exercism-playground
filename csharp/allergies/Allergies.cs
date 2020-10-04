using System;
using System.Collections.Generic;
using System.Linq;

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
    public Allergies(int mask) => this.mask = mask & MAX_MASK;
    private static readonly int MAX_MASK = (Enum.GetValues(typeof(Allergen)).Cast<int>().Max() << 1) - 1;

    public bool IsAllergicTo(Allergen allergen) =>
        (((Allergen)Enum.ToObject(typeof(Allergen), mask)) & allergen) == allergen;
    public Allergen[] List()
    {
        return ScanForAllergens(mask)
            .Select((a, i) =>
                a % 2 == 0
                    ? Allergen.None
                    : (Allergen)(1 << i))
            .Where(a => a != Allergen.None)
            .ToArray();

        static IEnumerable<byte> ScanForAllergens(int mask)
        {
            while (mask > 0)
            {
                yield return (byte)(mask % 2);
                mask >>= 1;
            }
        }
    }
}
