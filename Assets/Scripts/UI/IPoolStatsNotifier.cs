public interface IPoolStatsNotifier : IPoolStats
{
    event System.Action<int> OnTotalSpawnedChanged;
    event System.Action<int> OnTotalCreatedChanged;
    event System.Action<int> OnActiveCountChanged;
}