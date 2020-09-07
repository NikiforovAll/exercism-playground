using System.Linq;
using System.Collections;
using System;
using System.Collections.Generic;

public static class Pangram
{

    public static bool IsPangram(string input) =>
        new PangramVerifier().Verify(input);

    private class PangramVerifier
    {
        private const int ENG_ALPHABET_SIZE = 26;
        private readonly ISet<char> _storage = new HashSet<char>(ENG_ALPHABET_SIZE);

        public bool Verify(string input)
        {
            foreach (var item in input.Where(c => char.IsLetter(c)))
            {
                _storage.Add(char.ToLower(item));
            }
            return _storage.Count == ENG_ALPHABET_SIZE;
        }
    }

}
