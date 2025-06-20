using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private WaitForSeconds _effectDurationWait;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _effectDurationWait = new WaitForSeconds(_particleSystem.main.duration);
    }

    private void OnEnable()
    {
        StartCoroutine(Destroying());
    }

    private IEnumerator Destroying()
    {
        yield return _effectDurationWait;
        Destroy(gameObject);
    }
}
