namespace Awaitable
{
    public interface IStrategy
    {
        StrategyStatus Status { get; }
        IStrategy[] Tick();
    }
}
