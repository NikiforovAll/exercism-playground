using System;
using System.Linq;

public static class ResistorColor
{
    private static readonly string[] _colors = new string[]{
        "black",
        "brown",
        "red",
        "orange",
        "yellow",
        "green",
        "blue",
        "violet",
        "grey",
        "white",
    };
    public static int ColorCode(string color) => 
        Array.IndexOf(_colors, color.ToLower());

    public static string[] Colors() => _colors.Clone() as string[];
}
