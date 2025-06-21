using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _defaultCapacity;

    private int _totalCount;
    protected ObjectPool<T> Pool;

    public event Action<int> TotalCountChanged;
    public event Action<int> CreatedCountChanged;
    public event Action<int> ActiveCountChanged;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => CreateInstance(),
            actionOnGet: (obj) => OnActionGet(obj),
            actionOnRelease: (obj) => OnActionRelease(obj),
            actionOnDestroy: (obj) => OnActionDestroy(obj),
            collectionCheck: true,
            maxSize: _maxSize,
            defaultCapacity: _defaultCapacity
        );
    }

    protected virtual T CreateInstance()
    {
        CreatedCountChanged?.Invoke(Pool.CountAll + 1);
        return Instantiate(_prefab);
    }

    protected virtual void OnActionGet(T obj)
    {
        obj.gameObject.SetActive(true);
        ActiveCountChanged?.Invoke(Pool.CountActive);
        _totalCount++;
        TotalCountChanged?.Invoke(_totalCount);
    }

    protected virtual void OnActionRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        ActiveCountChanged?.Invoke(Pool.CountActive - 1);
    }

    protected virtual void OnActionDestroy(T obj)
    {
        Destroy(obj.gameObject);
        CreatedCountChanged?.Invoke(Pool.CountAll);
    }
}
