using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeCollisionHandler))]
public class Cube : DestroyableMesh
{
    private CubeCollisionHandler _collisionHandler;
    private WaitForSeconds _wait;
    private bool _isDestroying;

    public event Action<Cube> Destroyed;

    protected override void Awake()
    {
        base.Awake();
        _collisionHandler = GetComponent<CubeCollisionHandler>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isDestroying = false;
        _wait = new WaitForSeconds(Delay);
        _collisionHandler.PlatformEntered += StartDestroying;
    }

    private void OnDisable()
    {
        _collisionHandler.PlatformEntered -= StartDestroying;
    }

    private IEnumerator Destroing()
    {
        Color activatedColor = UnityEngine.Random.ColorHSV(); ;
        MeshRenderer.material.color = activatedColor;
        yield return _wait;
        Destroyed?.Invoke(this);
    }

    private void StartDestroying()
    {
        if (_isDestroying == false)
        { 
            _isDestroying = true;
            StartCoroutine(Destroing());
        }
    }
}