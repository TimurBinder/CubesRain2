using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Platform _area;
    [SerializeField] private float _areaPadding;
    [SerializeField] private float _positionY;
    [SerializeField] private float _repeatRate;
    [SerializeField] private BombSpawner _bombSpawner;

    private WaitForSeconds _wait;

    protected override void Awake()
    {
        base.Awake();
        _wait = new WaitForSeconds(_repeatRate);
    }

    private void OnEnable()
    {
        StartCoroutine(Getting());
    }

    protected override void OnActionGet(Cube cube)
    {
        Vector3 position = _area.GetRandomPosition(_areaPadding);
        position.y = _positionY;
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.Disabled += Pool.Release;
        base.OnActionGet(cube);
    }

    protected override void OnActionRelease(Cube cube)
    {
        base.OnActionRelease(cube);
        cube.Disabled -= Pool.Release;
        _bombSpawner.Substitude(cube);
    }

    private IEnumerator Getting()
    {
        while (enabled)
        {
            yield return _wait;
            Pool.Get();
        }
    }
}
