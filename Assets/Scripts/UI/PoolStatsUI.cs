using UnityEngine;
using UnityEngine.UI;

public class PoolStatsUI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _poolComponent;
    [SerializeField] private Text _textSpawned;
    [SerializeField] private Text _textCreated;
    [SerializeField] private Text _textActive;

    private IPoolStats _poolStats;

    private void Awake()
    {
        _poolStats = _poolComponent as IPoolStats;
    }

    private void Update()
    {
        _textSpawned.text = $"����������: {_poolStats.TotalSpawned}";
        _textCreated.text = $"�������: {_poolStats.TotalCreated}";
        _textActive.text = $"�������: {_poolStats.ActiveCount}";
    }
}