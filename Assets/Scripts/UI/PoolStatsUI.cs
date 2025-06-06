using UnityEngine;
using UnityEngine.UI;

public abstract class PoolStatsUI<T> : MonoBehaviour where T : MonoBehaviour, IPoolStatsNotifier
{
    [SerializeField] protected T _poolComponent;

    [SerializeField] private Text _textSpawned;
    [SerializeField] private Text _textCreated;
    [SerializeField] private Text _textActive;

    private void Start()
    {
        _poolComponent.OnTotalSpawnedChanged += HandleSpawnedChanged;
        _poolComponent.OnTotalCreatedChanged += HandleCreatedChanged;
        _poolComponent.OnActiveCountChanged += HandleActiveCountChanged;

        _textSpawned.text = $"����������: {_poolComponent.TotalSpawned}";
        _textCreated.text = $"�������: {_poolComponent.TotalCreated}";
        _textActive.text = $"�������: {_poolComponent.ActiveCount}";
    }

    private void OnDestroy()
    {
        if (_poolComponent != null)
        {
            _poolComponent.OnTotalSpawnedChanged -= HandleSpawnedChanged;
            _poolComponent.OnTotalCreatedChanged -= HandleCreatedChanged;
            _poolComponent.OnActiveCountChanged -= HandleActiveCountChanged;
        }
    }

    private void HandleSpawnedChanged(int newTotalSpawned)
    {
        _textSpawned.text = $"����������: {newTotalSpawned}";
    }

    private void HandleCreatedChanged(int newTotalCreated)
    {
        _textCreated.text = $"�������: {newTotalCreated}";
    }

    private void HandleActiveCountChanged(int newActiveCount)
    {
        _textActive.text = $"�������: {newActiveCount}";
    }
}