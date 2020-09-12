using System;
using System.Linq;
using System.Collections.Generic;

public static class ScrabbleScore
{
    //```text
    // Letter                           Value
    // A, E, I, O, U, L, N, R, S, T       1
    // D, G                               2
    // B, C, M, P                         3
    // F, H, V, W, Y                      4
    // K                                  5
    // J, X                               8
    // Q, Z                               10
    // ```
    private static readonly IDictionary<int, ICollection<char>> _scores =
        new Dictionary<int, ICollection<char>>
        {
            [1] = "A, E, I, O, U, L, N, R, S, T".ToScrabbleScores(),
            [2] = "D, G".ToScrabbleScores(),
            [3] = "B, C, M, P".ToScrabbleScores(),
            [4] = "F, H, V, W, Y".ToScrabbleScores(),
            [5] = "K".ToScrabbleScores(),
            [8] = "J, X".ToScrabbleScores(),
            [10] = "Q, Z".ToScrabbleScores(),
        };

    private static int GetScore(char letter) =>
        _scores.First(kvp => kvp.Value.Contains(char.ToUpper(letter))).Key;
    public static int Score(string input) =>
        input.Aggregate(0, (score, c) => score + GetScore(c));
}

public static class ScrabbleExtensions
{
    public static ICollection<char> ToScrabbleScores(this string input) =>
        new HashSet<char>(input.Replace(",", string.Empty).Replace(" ", string.Empty));
}
