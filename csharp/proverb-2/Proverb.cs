using System;
using System.Linq;

public static class Proverb
{
    public static string[] Recite(string[] subjects) =>
        subjects.Any() ? subjects.Zip(subjects.Skip(1))
            .Select(z => $"For want of a {z.First} the {z.Second} was lost.")
            .Append($"And all for the want of a {subjects.First()}.")
            .ToArray() : Array.Empty<string>();
}
