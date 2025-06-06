using System.Collections;
using UnityEngine;

public class CubeSpawner : BaseSpawner<Cube>
{
    public event System.Action<Vector3> OnCubeDestroyed;

    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval = 0.2f;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private float _multiplierSpawnCubes = 5f;
    private float _heightSpawnCubes = 30f;
    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnCubes();
            _timer = 0f;
        }
    }

    protected override void InitializeObject(Cube cube) { }

    private void SpawnCubes()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(_spawnArea.position.x - _spawnArea.position.x * _multiplierSpawnCubes,
                         _spawnArea.position.x + _spawnArea.position.x * _multiplierSpawnCubes),
            _spawnArea.position.y + _heightSpawnCubes,
            Random.Range(_spawnArea.position.z - _spawnArea.position.z * _multiplierSpawnCubes,
                         _spawnArea.position.z + _spawnArea.position.z * _multiplierSpawnCubes)
        );

        Cube cube = Spawn(spawnPosition);

        cube.ResetCube();

        cube.TouchedPlatform += HandleCubeTouch;
    }

    private void HandleCubeTouch(Cube cube)
    {
        float lifetime = Random.Range(_minLifeTime, _maxLifeTime + 1f);

        StartCoroutine(DestroyCubeAfterDelay(cube, lifetime));
    }

    private IEnumerator DestroyCubeAfterDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 cubePosition = cube.transform.position;

        cube.TouchedPlatform -= HandleCubeTouch;

        _pool.Release(cube);

        OnCubeDestroyed?.Invoke(cubePosition);
    }
}