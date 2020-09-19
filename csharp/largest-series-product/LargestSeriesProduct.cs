using System;
using System.Collections.Generic;
using System.Linq;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        RunLargestSeriesProductGuard();

        if(span == 0)
            return 1;

        var max = 0;
        var curr = 1;
        var array = ToDigits(digits);
        var length = array.Count;
        int start = 0, end = 0;
        for (int i = 0; i < length; i++)
        {
            end = i;
            if (array[i] == 0)
            {
                start = i + 1;
                end = start;
                curr = 1;
                continue;
            }

            var full = end - start >= span;
            if (full)
            {
                curr /= array[start];
                curr *= array[i];
                start++;
            }
            else
            {
                curr *= array[end];
            }

            max = curr > max && (end - start + 1 == span) ? curr : max;
        }

        return max;

        static IList<byte> ToDigits(string digits) =>
            digits.Select<char, byte>(
                d => char.IsDigit(d)
                    ? (byte)(d - '0')
                    : throw new ArgumentException(nameof(ToDigits)))
                .ToList<byte>();

        void RunLargestSeriesProductGuard()
        {
            if (digits == string.Empty && span > 0
                || span < 0
                || digits.Length < span)
                throw new ArgumentException(nameof(LargestSeriesProduct));
        }
    }

}
