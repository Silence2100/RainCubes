using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour, IPoolStatsNotifier where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _maxPoolSize = 20;

    protected ObjectPool<T> _pool;

    public int TotalSpawned { get; private set; }
    public int TotalCreated { get; private set; }
    public int ActiveCount => _pool.CountActive;

    public event System.Action<int, int, int> OnPoolStatsChanged;

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
        TotalCreated++;

        OnPoolStatsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);

        T newObj = Instantiate(_prefab);
        newObj.gameObject.SetActive(false);

        return newObj;
    }

    private void OnGet(T obj)
    {
        TotalSpawned++;
        obj.gameObject.SetActive(true);

        OnPoolStatsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);
    }

    private void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);

        OnPoolStatsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);
    }

    private void OnDestroyInstance(T obj)
    {
        Destroy(obj.gameObject);

        OnPoolStatsChanged?.Invoke(TotalSpawned, TotalCreated, ActiveCount);
    }
}