using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void Substitude(Cube cube)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.rotation = Quaternion.identity;
        bomb.transform.position = cube.transform.position;
    }

    protected override void OnActionGet(Bomb bomb)
    {
        bomb.Disabled += Pool.Release;
        base.OnActionGet(bomb);
    }

    protected override void OnActionRelease(Bomb bomb)
    {
        bomb.Disabled -= Pool.Release;
        base.OnActionRelease(bomb);
    }
}
