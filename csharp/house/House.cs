using System;
using System.Linq;
using System.Text;

public static class House
{
    private static readonly string template0 = "This is {0} that Jack built.";

    private static readonly string[] folks = new string[] {
        "the house",
        "the malt that lay in",
        "the rat that ate",
        "the cat that killed",
        "the dog that worried",
        "the cow with the crumpled horn that tossed",
        "the maiden all forlorn that milked",
        "the man all tattered and torn that kissed",
        "the priest all shaven and shorn that married",
        "the rooster that crowed in the morn that woke",
        "the farmer sowing his corn that kept",
        "the horse and the hound and the horn that belonged to",
    };

    public static string Recite(int verseNumber)
    {
        verseNumber--;
        StringBuilder sb = new StringBuilder(template0);
        while (verseNumber > 0)
        {
            sb.Replace("{0}", $"{folks[verseNumber]} {{0}}");
            verseNumber--;
        }
        sb.Replace("{0}", $"{folks[0]}");
        return sb.ToString();
    }

    public static string Recite(int startVerse, int endVerse) =>
        Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(v => Recite(v))
            .Aggregate(new StringBuilder(), (sb, s) => sb.AppendLine(s)).ToString().TrimEnd('\n');

}
