using System;
using System.Linq;

public static class RotationalCipher
{
    private const int ALPHABET_SIZE = 26;
    private const char ALPAHBET_START = 'a';
    public static string Rotate(string text, int shiftKey)
    {
        return string.Concat(
            text.Select(c => char.IsLetter(c)
                ? RestoreCase(Shift(c), c)
                : c));

        char Shift(char c) =>
            (char)((char.ToLowerInvariant(c) - ALPAHBET_START + shiftKey)
                    % ALPHABET_SIZE + ALPAHBET_START);
        static char RestoreCase(char c1, char c2) =>
            char.IsUpper(c2) ? char.ToUpperInvariant(c1) : c1;
    }
}
