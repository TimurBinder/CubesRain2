using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minExplodeDelay = 2;
    [SerializeField] private float _maxExplodeDelay = 5;
    [SerializeField] private Effect _explodeEffect;
    [SerializeField] private float _explodeRange;

    private float _explodeTimer;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    public event Action<Bomb> Disabled;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        float delay = UnityEngine.Random.Range(_minExplodeDelay, _maxExplodeDelay);
        _explodeTimer = 0;
        StartCoroutine(Destroing(delay));
    }

    private void OnDisable()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private List<Rigidbody> GetExplodables()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRange);
        List<Rigidbody> explodables = new List<Rigidbody>();
        
        foreach (Collider collider in hits)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                explodables.Add(rigidbody);
        }

        return explodables;
    }

    private void Explode()
    {
        List<Rigidbody> explodables = GetExplodables();

        foreach(Rigidbody rigidbody in explodables)
        {
            Vector3 direction = rigidbody.transform.position - transform.position;
            rigidbody.AddForceAtPosition(direction.normalized, transform.position);
        }

        Instantiate(_explodeEffect, transform.position, Quaternion.identity);
        Disabled?.Invoke(this);
    }

    private IEnumerator Destroing(float delay)
    {
        while (_explodeTimer < delay)
        {
            _explodeTimer = Mathf.MoveTowards(_explodeTimer, delay, Time.deltaTime);
            Color newColor = _meshRenderer.material.color;
            newColor.a = delay / _explodeTimer;
            yield return null;
        }

        Explode();
    }
}
