using UnityEngine;
using UnityEngine.Pool;

public class GenericPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _maxPoolSize = 20;

    private ObjectPool<T> _pool;
    private int _spawnCount;
    private int _createdCount;

    public int TotalSpawned => _spawnCount;
    public int TotalCreated => _createdCount;
    public int ActiveCount => _pool.CountActive;

    public T Get()
    {
        return _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateInstance,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyInstance,
            collectionCheck: false,
            defaultCapacity: _maxPoolSize,
            maxSize: _maxPoolSize
        );
    }

    private T CreateInstance()
    {
        _createdCount++;

        T newObject = Instantiate(_prefab);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    private void OnGet(T obj)
    {
        _spawnCount++;
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyInstance(T obj)
    {
        Destroy(obj.gameObject);
    }
}