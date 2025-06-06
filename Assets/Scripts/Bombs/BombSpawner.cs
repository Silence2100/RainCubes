using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb>
{
    private void OnEnable()
    {
        CubeSpawner.OnCubeDestroyed += HandleCubeDestroyed;
    }

    private void OnDisable()
    {
        CubeSpawner.OnCubeDestroyed -= HandleCubeDestroyed;
    }

    public Bomb SpawnAtPosition(Vector3 position)
    {
        return Spawn(position);
    }

    protected override void InitializeObject(Bomb bomb)
    {
        bomb.OnExploded += HandleBombExploded;

        bomb.InitializeBomb();
    }

    private void HandleCubeDestroyed(Vector3 position)
    {
        SpawnAtPosition(position);
    }

    private void HandleBombExploded(Bomb bomb)
    {
        bomb.OnExploded -= HandleBombExploded;
        _pool.Release(bomb);
    }
}