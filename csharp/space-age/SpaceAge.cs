using System.Collections.Generic;
using System;

public class SpaceAge
{
    private readonly int _seconds;

    private static readonly Dictionary<string, double> _orbitalPeriodTransformationCoefficient
        = new Dictionary<string, double>()
        {
            [nameof(OnEarth)] = 1D,
            [nameof(OnMercury)] = 0.2408467D,
            [nameof(OnVenus)] = 0.61519726D,
            [nameof(OnMars)] = 1.8808158D,
            [nameof(OnJupiter)] = 11.862615D,
            [nameof(OnSaturn)] = 29.447498D,
            [nameof(OnUranus)] = 84.016846D,
            [nameof(OnNeptune)] = 164.7913D,
        };
    private const int SECONDS_IN_EARTH_ORBITAL_PERIOD = 31557600;

    public SpaceAge(int seconds) => _seconds = seconds;

    public double OnEarth() => ToOrbitalPeriod();

    public double OnMercury() => ToOrbitalPeriod();

    public double OnVenus() => ToOrbitalPeriod();

    public double OnMars() => ToOrbitalPeriod();

    public double OnJupiter() => ToOrbitalPeriod();

    public double OnSaturn() => ToOrbitalPeriod();

    public double OnUranus() => ToOrbitalPeriod();

    public double OnNeptune() => ToOrbitalPeriod();

    private double ToOrbitalPeriod(
        [System.Runtime.CompilerServices.CallerMemberName] string orbitalMethodName = "")
        => _orbitalPeriodTransformationCoefficient.ContainsKey(orbitalMethodName)
            ? _seconds / (SECONDS_IN_EARTH_ORBITAL_PERIOD * _orbitalPeriodTransformationCoefficient[orbitalMethodName])
            : throw new InvalidOperationException(nameof(ToOrbitalPeriod));
}
