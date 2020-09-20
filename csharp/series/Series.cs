using System;
using System.Linq;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        SlicesGuard();
        return Enumerable.Range(0, numbers.Length - sliceLength + 1)
            .Select(d => new string(numbers[d..(d + sliceLength)]))
            .ToArray();

        void SlicesGuard()
        {
            if (sliceLength > numbers.Length
                || sliceLength <= 0
                || numbers == string.Empty)
                throw new ArgumentException(nameof(Slices));
        }
    }
}
