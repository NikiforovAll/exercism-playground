using System;
using Awaitable;
namespace example
{
    class Program
    {
        // ? Change driver mode in Core/Solver.cs to change scheduler
        static void Main(string mode = "simple")
        {
            var input = "290010345";
            // var input = "291345";
            long res = mode switch
            {
                "simple" => LargestSeriesProductV2
                    .GetLargestProduct<SlideStrategy>(input, 2, Console.Out),
                "multi" => LargestSeriesProductV2
                    .GetLargestProduct<MultipleSlidingWindowsStrategy>(input, 2, Console.Out),
                "chaos" => LargestSeriesProductV2
                    .GetLargestProduct<ChaosWindowStrategy>(input, 2, Console.Out),
                _ => throw new NotImplementedException(),
            };
            Console.WriteLine($"Result {res}");
        }
    }
}
