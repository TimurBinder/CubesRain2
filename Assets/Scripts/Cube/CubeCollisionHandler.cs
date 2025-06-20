using System;
using UnityEngine;

public class CubeCollisionHandler : MonoBehaviour
{
    public event Action Disabled;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
            Disabled?.Invoke();
    }
}
