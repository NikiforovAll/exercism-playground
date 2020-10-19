using System;
using System.IO;
using System.Drawing;
using Pastel;
using System.Threading.Tasks;
using System.Linq;

namespace Awaitable
{
    public class Solver
    {
        private Random rnd = new Random();
        private Color color;

        public TextWriter StdOut { get; set; }

        public bool RunDriver<T, K>(params T[] strategies)
            where T : Strategy<K>
        {
            var mode = DriverMode.Normal;

            switch (mode)
            {
                case DriverMode.Normal:
                    SequentialScheduling<T, K>(strategies[0]);
                    break;
                case DriverMode.Chaos:
                    ThreadpoolScheduling<T, K>(strategies);
                    break;
            }

            return new StrategyTask(strategies).GetAwaiter().GetResult();
        }

        private void SequentialScheduling<T, K>(T strategy) where T : Strategy<K>
        {
            RecursiveTick(strategy: strategy, 0);
        }

        private void ThreadpoolScheduling<T, K>(params T[] strategies) where T : Strategy<K>
        {
            var tasks = strategies.Select(t => Task.Run(
                () =>
                {
                    var color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    RecursiveTick(t, 0, color);
                }));
            Task.WaitAll(tasks.ToArray());
        }
        private void RecursiveTick(
            IStrategy strategy,
            int indentation = 0,
            Color? overrideColor = default)
        {
            if (strategy is SlidingWindowStrategy)
                color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            var line = new string('-', indentation * 5)
                .Pastel(overrideColor ?? color);
            StdOut?.WriteLine($"{line}{strategy}");
            while (strategy.Status == StrategyStatus.InProgress)
            {
                var strategies = strategy.Tick();
                if (strategies != null)
                {
                    indentation++;
                    foreach (var child in strategies)
                    {
                        RecursiveTick(child, indentation, overrideColor);

                    }
                    indentation--;
                }
            }
        }
        enum DriverMode
        {
            Normal,
            Chaos
        }
    }
}
