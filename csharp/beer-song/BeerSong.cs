using System;
using System.Text;

public static class BeerSong
{
    public static string Recite(int bottles, int takeDown)
    {
        StringBuilder sb = new StringBuilder();
        var max_bottles = 99;
        var template0 = "{0} bottles of beer on the wall, {0} bottles of beer.";
        var template1 = "Take one down and pass it around, {0} bottle{1} of beer on the wall.";
        var temlate2 = "1 bottle of beer on the wall, 1 bottle of beer.";
        var temlate3 = "Take it down and pass it around, no more bottles of beer on the wall.";
        var template4 = "No more bottles of beer on the wall, no more bottles of beer.";
        var template5 = "Go to the store and buy some more, 99 bottles of beer on the wall.";

        while (takeDown > 0)
        {
            if (bottles > 1)
            {
                sb.AppendLine(string.Format(template0, bottles));
                sb.AppendLine(
                    string.Format(
                        template1, bottles - 1, bottles > 2 ? "s" : string.Empty));
            }
            else if (bottles == 1)
            {
                sb.AppendLine(temlate2);
                sb.AppendLine(temlate3);
            }
            else
            {
                sb.AppendLine(template4);
                sb.AppendLine(template5);
                bottles = max_bottles;
            }
            takeDown--;
            bottles--;
            if (takeDown > 0)
            {
                sb.AppendLine();
            }
        }
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
}
