namespace Awaitable
{
    public abstract class Strategy<T> : IStrategy
    {
        private readonly AsyncTicker<T> ticker;

        protected Strategy()
        {
            ticker = new AsyncTicker<T>(Run);
        }

        protected abstract StrategyTask<T> Run();

        public StrategyStatus Status { get; private set; }

        public IStrategy[] Tick()
        {
            var tickerResult = ticker.Tick();
            Status = tickerResult.Status;
            return tickerResult.Strategies;
        }
    }
}
