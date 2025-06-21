using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CubeCollisionHandler : MonoBehaviour
{
    public event Action PlatformEntered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
            PlatformEntered?.Invoke();
    }
}
