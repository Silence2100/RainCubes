public interface IPoolStats
{
    int TotalSpawned { get; }
    int TotalCreated { get; }
    int ActiveCount { get; }
}