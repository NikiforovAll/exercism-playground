namespace Awaitable
{
    public abstract class Strategy : IStrategy
    {
        private readonly AsyncTicker ticker;

        protected Strategy()
        {
            ticker = new AsyncTicker(Run);
        }

        protected abstract StrategyTask<bool> Run();

        public StrategyStatus Status { get; private set; }

        public IStrategy[] Tick()
        {
            var tickerResult = ticker.Tick();
            Status = tickerResult.Status;
            return tickerResult.Strategies;
        }
    }
}
