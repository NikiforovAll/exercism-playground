using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word)
    {
        var query = word
            .Where(c => char.IsLetter(c))
            .Select(c => char.ToLower(c));
        return query.Distinct().Count() == query.Count();
    }
}
