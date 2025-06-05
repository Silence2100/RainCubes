using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GenericPool<Cube> _cubePool;
    [SerializeField] private GenericPool<Bomb> _bombPool;

    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval = 0.2f;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;
    private float _multiplierSpawnCubes = 2f;
    private float _heightSpawnCubes = 30f;
    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnCube();
            _timer = 0f;
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.Get();
        cube.TouchedPlatform += HandleCubeTouch;

        Vector3 spawnPosition = new Vector3(
            Random.Range(_spawnArea.position.x - _spawnArea.localScale.x * _multiplierSpawnCubes, _spawnArea.position.x + _spawnArea.localScale.x * _multiplierSpawnCubes),
            _spawnArea.position.y + _heightSpawnCubes,
            Random.Range(_spawnArea.position.z - _spawnArea.localScale.z * _multiplierSpawnCubes, _spawnArea.position.z + _spawnArea.localScale.z * _multiplierSpawnCubes)
        );

        cube.transform.position = spawnPosition;
        cube.transform.rotation = Quaternion.identity;

        cube.ResetCube();
    }

    private void HandleCubeTouch(Cube cube)
    {
        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime + 1f);
        StartCoroutine(DestroyCubeAfterDelay(cube, lifeTime));
    }

    private IEnumerator DestroyCubeAfterDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 cubePosition = cube.transform.position;
        cube.TouchedPlatform -= HandleCubeTouch;

        if (_bombPool != null)
        {
            Bomb bomb = _bombPool.Get();

            bomb.transform.position = cubePosition;
            bomb.transform.rotation = Quaternion.identity;

            bomb.OnExploded += HandleBombExploded;

            bomb.InitializeBomb();
        }

        _cubePool.Release(cube);
    }

    private void HandleBombExploded(Bomb bomb)
    {
        bomb.OnExploded -= HandleBombExploded;

        _bombPool.Release(bomb);
    }
}