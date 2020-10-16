using System;
using System.Linq;

public class Anagram
{
    private readonly string baseWord;

    public Anagram(string baseWord) => this.baseWord = baseWord.ToLowerInvariant();

    public string[] FindAnagrams(string[] potentialMatches) =>
        potentialMatches.Where(Matched).ToArray();

    private bool Matched(string toMatch)
    {
        toMatch = toMatch.ToLowerInvariant();
        if (toMatch == baseWord)
            return false;
        var dict = baseWord.ToLower().GroupBy(c => c)
            .ToDictionary(c => c.Key, c => c.Count());
        return toMatch.All(c => dict.ContainsKey(c) && dict[c]-- != 0)
            && dict.Values.All(counter => counter == 0);
    }
}
