using System;
using System.Linq;

public static class SecretHandshake
{
    [Flags]
    enum Command : short
    {
        Wink = 0b_00001,
        DoubleBlink = 0b_00010,
        CloseYourEyes = 0b_00100,
        Jump = 0b_01000,
        Reverse = 0b_10000,
    }

    public static string[] Commands(int commandValue)
    {
        var commands = new Command[] {
            Command.Wink, Command.DoubleBlink, Command.CloseYourEyes, Command.Jump
        };
        var query = commands.Where(c => ((short)c & commandValue) != 0);
        if ((commandValue & (short)Command.Reverse) != 0)
        {
            query = query.Reverse();
        }
        return query.Select(c => c switch
        {
            Command.Wink => "wink",
            Command.DoubleBlink => "double blink",
            Command.CloseYourEyes => "close your eyes",
            Command.Jump => "jump",
            _ => throw new NotImplementedException(),
        }).ToArray();

    }
}
