using System;
using System.Linq;

public static class RnaTranscription
{
    public static string ToRna(string nucleotide) =>
        nucleotide
            .Select(Trans)
            .Select(c => c.ToString())
            .DefaultIfEmpty(string.Empty)
            .Aggregate(string.Concat);


    private static char Trans(char c) => c switch
    {
        'G' => 'C',
        'C' => 'G',
        'T' => 'A',
        'A' => 'U',
    };
}
