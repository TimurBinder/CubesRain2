using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : DestroyableMesh
{
    [SerializeField] private Effect _explodeEffect;
    [SerializeField] private float _explodeRange;

    private float _explodeTimer;

    public event Action<Bomb> Destroyed;

    protected override void OnEnable()
    {
        base.OnEnable();
        _explodeTimer = 0;
        StartCoroutine(Destroing());
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

        Destroyed?.Invoke(this);
    }

    private IEnumerator Destroing()
    {
        while (_explodeTimer < Delay)
        {
            _explodeTimer = Mathf.MoveTowards(_explodeTimer, Delay, Time.deltaTime);
            Color newColor = MeshRenderer.material.color;
            newColor.a = Delay / _explodeTimer;
            yield return null;
        }

        Explode();
    }
}
