using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        EnsureValidBase(inputBase, inputDigits);
        EnsureValidBase(outputBase);

        return ToOutputBase().ToArray();

        IEnumerable<int> ToOutputBase()
        {
            var S = FromInputBase().Sum();
            var sum = S;
            var multiplier = 1;
            while ((sum /= outputBase) > 0) { multiplier *= outputBase; }
            sum = S;
            while (multiplier > 0)
            {
                yield return sum / multiplier;
                sum %= multiplier;
                multiplier /= outputBase;
            }
        }

        IEnumerable<int> FromInputBase()
        {
            var multiplier = 1;
            foreach (var item in inputDigits.Reverse())
            {
                yield return item * multiplier;
                multiplier *= inputBase;
            }
        }

        void EnsureValidBase(int @base, int[] digits = default)
        {
            if (@base < 2)
                throw new ArgumentException(nameof(EnsureValidBase));

            if (digits != default(int[])
                && digits.Any(d => d >= inputBase || d < 0))
                throw new ArgumentException(nameof(EnsureValidBase));
        }
    }
}
