using UnityEngine;

public class EffectSpawner : SubstitudableSpawner<Effect>
{
    protected override void OnActionGet(Effect effect)
    {
        base.OnActionGet(effect);
        effect.Finished += Pool.Release;
    }

    protected override void OnActionRelease(Effect effect)
    {
        base.OnActionRelease(effect);
        effect.Finished -= Pool.Release;
    }
}
