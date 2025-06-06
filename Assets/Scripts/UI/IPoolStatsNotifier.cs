public interface IPoolStatsNotifier : IPoolStats
{
    event System.Action<int, int, int> OnPoolStatsChanged;
}
