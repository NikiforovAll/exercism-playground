using System;
using System.Linq;

namespace Awaitable
{
    public class AsyncTicker<T>
    {
        private readonly Func<StrategyTask<T>> run;
        private StrategyTask<T> task;

        public AsyncTicker(Func<StrategyTask<T>> run)
        {
            this.run = run;
        }

        public StrategyResult Tick()
        {
            if (task?.Strategies != null && task.Strategies.Any(s => s.Status == StrategyStatus.InProgress))
            {
                return new StrategyResult(StrategyStatus.InProgress, null);
            }

            if (task == null)
                task = run();
            else
                task.Continue();

            if (task.IsComplete)
                // return task.Result ? new StrategyResult(StrategyStatus.Done, null) : new StrategyResult(StrategyStatus.Failed, null);
                return task.Result switch
                {
                    null => new StrategyResult(StrategyStatus.Failed, null),
                    _ => new StrategyResult(StrategyStatus.Done, null),
                };
            return new StrategyResult(StrategyStatus.InProgress, task.Strategies);
        }
    }
}
