using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CubeCollisionHandler))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private CubeCollisionHandler _collisionHandler;
    private Rigidbody _rigidbody;

    public event Action<Cube> Disabled;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _collisionHandler.Disabled += Disable;
    }

    private void OnDisable()
    {
        _collisionHandler.Disabled -= Disable;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void Disable()
    {
        Color activatedColor = UnityEngine.Random.ColorHSV(); ;
        _meshRenderer.material.color = activatedColor;
        Disabled?.Invoke(this);
    }
}