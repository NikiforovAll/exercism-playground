using System;
using System.Collections.Generic;
using System.Linq;

public class Robot
{
    private Lazy<string> _name = Robot.Init();
    public string Name => _name.Value;

    public void Reset()
    {
        RobotRegistryRandomWithShift.Reset(Name);
        _name = Robot.Init();
    }

    private static Lazy<string> Init() => new Lazy<string>(
        () => RobotRegistryRandomWithShift.Next());
}

internal static class RobotRegistryRandomWithShift
{
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static readonly Dictionary<string, bool> _registry =
        ALPHABET.SelectMany(c => ALPHABET.Select(c2 => string.Concat(c, c2)))
        .SelectMany(prefix => Enumerable.Range(100, 899).Select(d => prefix + d)).ToDictionary(name => name, _ => false);
    internal static string Next()
    {
        var newName = RobotNameUtils.GenerateRandomRobotName(out var robotName)
            && !_registry[robotName]
            ? robotName
            : _registry.First(kvp => !kvp.Value && kvp.Key != robotName).Key;
        _registry[newName] = true;
        return newName;
    }

    internal static void Reset(string robotName) => _registry[robotName] = false;

}

// This doesn't work when registry is almost field, StackOverflow exception
// it could be rewritten without recursion, but probbing strategy is flawed
internal static class RobotRegistryRandom
{
    private static readonly HashSet<string> _registry = new HashSet<string>();
    internal static string Next() =>
        RobotNameUtils.GenerateRandomRobotName(out var rn) && Accept(rn)
            ? rn : Next();
    internal static bool Accept(string robotName)
    {
        if (!_registry.Contains(robotName))
        {
            return false;
        }
        else
        {
            _registry.Add(robotName);
            return true;
        }
    }
    internal static void Reset(string robotName) => _registry.Remove(robotName);
}

internal static class RobotNameUtils
{
    private static readonly Random _generator = new Random();
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    internal static bool GenerateRandomRobotName(out string name)
    {
        var prefix = Enumerable.Repeat(int.MinValue, count: 2)
            .Select(_ => ALPHABET[_generator.Next(ALPHABET.Length)])
            .Aggregate(string.Empty, (curr, c) => curr + c);
        name = $"{prefix}{_generator.Next(100, 999)}";
        // could return $false if if it is not possible to generate
        // next value
        return true;
    }
}
