using UnityEngine;
using UnityEngine.UI;

public class PoolStatsUI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _poolComponent;

    [SerializeField] private Text _textSpawned;
    [SerializeField] private Text _textCreated;
    [SerializeField] private Text _textActive;

    private IPoolStatsNotifier _statsNotifier;

    private void Awake()
    {
        _statsNotifier = _poolComponent as IPoolStatsNotifier;

        _statsNotifier.OnPoolStatsChanged += HandleStatsChanged;

        HandleStatsChanged(
            _statsNotifier.TotalSpawned,
            _statsNotifier.TotalCreated,
            _statsNotifier.ActiveCount
        );
    }

    private void OnDestroy()
    {
        if (_statsNotifier != null)
        {
            _statsNotifier.OnPoolStatsChanged -= HandleStatsChanged;
        }
    }

    private void HandleStatsChanged(int totalSpawned, int totalCreated, int activeCount)
    {
        _textSpawned.text = $"Заспавнено: {totalSpawned}";
        _textCreated.text = $"Создано: {totalCreated}";
        _textActive.text = $"Активно: {activeCount}";
    }
}