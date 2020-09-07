using System.Linq;
using System;
using System.Text.RegularExpressions;

public static class Acronym
{
    public static string Abbreviate(string phrase) =>
        Regex.Replace(phrase, "[^A-Za-z0-9 -]", "")
            .Split(new char[] {' ', '-'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => char.ToUpper(word[0]))
            .Aggregate("", (acc, c) => acc + c);
}
