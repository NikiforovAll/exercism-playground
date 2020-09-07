using System;

public static class ReverseString
{
    public static string Reverse(string input)
    {
        var chars = input.ToCharArray();
        int start = 0, end = chars.Length - 1;
        while (start < end)
        {
            (chars[start], chars[end]) = (chars[end--], chars[start++]);
        }
        return new string(chars);

    }
}
