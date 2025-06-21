using UnityEngine;

public class BombSpawner : SubstitudableSpawner<Bomb>
{
    [SerializeField] private EffectSpawner _effectSpawner;

    protected override void OnActionGet(Bomb bomb)
    {
        bomb.Destroyed += Pool.Release;
        base.OnActionGet(bomb);
    }

    protected override void OnActionRelease(Bomb bomb)
    {
        bomb.Destroyed -= Pool.Release;
        _effectSpawner.Substitude(bomb.transform);
        base.OnActionRelease(bomb);
    }
}
