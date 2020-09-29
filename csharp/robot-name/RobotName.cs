#define DEBUG
// #undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;

using static RobotNameUtils;
public class Robot
{
    private Lazy<string> _name = Robot.Init();
    public string Name => _name.Value;

    public void Reset()
    {
        RobotFactorySingletonProvider.Instance.Reset(Name);
        _name = Robot.Init();
    }

    private static Lazy<string> Init() => new Lazy<string>(
        () => RobotFactorySingletonProvider.Instance.Next());
}

internal interface IRobotNameFactory
{
    string Next();
    void Reset(string robotName);
}

internal sealed class RobotFactorySingletonProvider
{
    static RobotFactorySingletonProvider() { }

    private RobotFactorySingletonProvider() { }

    #if DEBUG
        public static IRobotNameFactory Instance { get; } = new RobotRegistryRandom();
    #else
        public static IRobotNameFactory Instance { get; } = new RobotRegistryRandomWithShift();
    #endif

}

internal class RobotRegistryRandomWithShift : IRobotNameFactory
{
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static readonly Dictionary<string, bool> _registry =
        ALPHABET.SelectMany(c => ALPHABET.Select(c2 => string.Concat(c, c2)))
        .SelectMany(prefix => Enumerable.Range(0, MAX_ROBOT_NUMBER)
                .Select(d => $"{prefix}{d}"))
                .ToDictionary(name => RobotName($"{name:robotName}"), _ => false);
    public string Next()
    {
        var newName = GenerateRandomRobotName(out var robotName) && !_registry[robotName]
                ? robotName
                : _registry.First(kvp => !kvp.Value && kvp.Key != robotName).Key;
        _registry[newName] = true;
        return newName;
    }

    public void Reset(string robotName) => _registry[robotName] = false;

}

// This doesn't work when registry is almost filled, StackOverflow exception
// it could be rewritten without recursion, but probbing strategy is flawed
internal class RobotRegistryRandom : IRobotNameFactory
{
    private static readonly HashSet<string> _registry = new HashSet<string>();
    public string Next() =>
        RobotNameUtils.GenerateRandomRobotName(out var rn) && Accept(rn)
            ? rn : Next();
    public void Reset(string robotName) => _registry.Remove(robotName);
    private bool Accept(string robotName) => _registry.Add(robotName);
}

internal static class RobotNameUtils
{
    public const int MAX_ROBOT_NUMBER = 1000;
    private static readonly Random _generator = new Random();
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    internal static bool GenerateRandomRobotName(out string name)
    {
        var prefix = Enumerable.Repeat(int.MinValue, count: 2)
            .Select(_ => ALPHABET[_generator.Next(ALPHABET.Length)])
            .Aggregate(string.Empty, (curr, c) => curr + c);
        name = RobotName($"{prefix}{_generator.Next(0, MAX_ROBOT_NUMBER):robotNumber}");
        return true;
    }

    internal static string RobotName(FormattableString name) =>
        name.ToString(new RobotNameFormatter());
}

internal class RobotNameFormatter : IFormatProvider, ICustomFormatter
{
    public string Format(string format, object arg, IFormatProvider formatProvider) =>
        format switch
        {
            "robotName" when arg is string str => $"{str[..2]}{str[2..].PadLeft(3, '0')}",
            "robotNumber" => $"{arg:000}",
            "robotPrefix" => arg.ToString().ToUpper(),
            _ => arg.ToString()
        };

    public object GetFormat(Type formatType) => this;
}
