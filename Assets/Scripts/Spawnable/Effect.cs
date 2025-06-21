using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private WaitForSeconds _effectDurationWait;

    public event Action<Effect> Finished;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _effectDurationWait = new WaitForSeconds(_particleSystem.main.duration);
    }

    private void OnEnable()
    {
        StartCoroutine(Playing());
    }

    private IEnumerator Playing()
    {
        _particleSystem.Play();
        yield return _effectDurationWait;
        Finished?.Invoke(this);
    }
}
