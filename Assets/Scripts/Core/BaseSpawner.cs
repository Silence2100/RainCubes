using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour, IPoolStats where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _maxPoolSize = 20;

    protected ObjectPool<T> _pool;

    private int _spawnCount;
    private int _createdCount;

    public int TotalSpawned => _spawnCount;
    public int TotalCreated => _createdCount;
    public int ActiveCount => _pool.CountActive;

    protected virtual void Awake()
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

    public T Spawn(Vector3 position)
    {
        T obj = _pool.Get();

        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;

        InitializeObject(obj);

        return obj;
    }

    protected abstract void InitializeObject(T obj);

    private T CreateInstance()
    {
        _createdCount++;
        T newObj = Instantiate(_prefab);
        newObj.gameObject.SetActive(false);

        return newObj;
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