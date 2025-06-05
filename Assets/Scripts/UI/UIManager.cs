using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GenericPool<Cube> _cubePool;
    [SerializeField] private GenericPool<Bomb> _bombPool;

    [SerializeField] private Text _textCubeSpawned;
    [SerializeField] private Text _textCubeCreated;
    [SerializeField] private Text _textCubeActive;

    [SerializeField] private Text _textBombSpawned;
    [SerializeField] private Text _textBombCreated;
    [SerializeField] private Text _textBombActive;

    private void Update()
    {
        if (_cubePool != null)
        {
            _textCubeSpawned.text = $"Кубов заспавнено: {_cubePool.TotalSpawned}";
            _textCubeCreated.text = $"Кубов создано: {_cubePool.TotalCreated}";
            _textCubeActive.text = $"Кубов активно: {_cubePool.ActiveCount}";
        }
        else
        {
            Debug.LogWarning("UIManager: Ссылка _cubePool не задана! Статистика по кубам не будет отображаться.");
            _cubePool = null;
        }

        if (_bombPool != null)
        {
            _textBombSpawned.text = $"Бомб заспавнено: {_bombPool.TotalSpawned}";
            _textBombCreated.text = $"Бомб создано: {_bombPool.TotalCreated}";
            _textBombActive.text = $"Бомб активно: {_bombPool.ActiveCount}";
        }
        else
        {
            Debug.LogWarning("UIManager: Ссылка _bombPool не задана! Статистика по бомбам не будет отображаться.");
            _bombPool = null;
        }
    }
}