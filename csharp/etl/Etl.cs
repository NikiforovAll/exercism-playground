using System;
using System.Collections.Generic;
using System.Linq;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
        => old.SelectMany(score => score.Value.Select(letter => (score: score.Key, letter)))
            .ToDictionary(sl => sl.letter.ToLower(), sl => sl.score);
}
