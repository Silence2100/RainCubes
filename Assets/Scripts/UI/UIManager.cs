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
            _textCubeSpawned.text = $"����� ����������: {_cubePool.TotalSpawned}";
            _textCubeCreated.text = $"����� �������: {_cubePool.TotalCreated}";
            _textCubeActive.text = $"����� �������: {_cubePool.ActiveCount}";
        }
        else
        {
            Debug.LogWarning("UIManager: ������ _cubePool �� ������! ���������� �� ����� �� ����� ������������.");
            _cubePool = null;
        }

        if (_bombPool != null)
        {
            _textBombSpawned.text = $"���� ����������: {_bombPool.TotalSpawned}";
            _textBombCreated.text = $"���� �������: {_bombPool.TotalCreated}";
            _textBombActive.text = $"���� �������: {_bombPool.ActiveCount}";
        }
        else
        {
            Debug.LogWarning("UIManager: ������ _bombPool �� ������! ���������� �� ������ �� ����� ������������.");
            _bombPool = null;
        }
    }
}