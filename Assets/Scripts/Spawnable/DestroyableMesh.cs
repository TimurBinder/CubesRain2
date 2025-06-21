using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
abstract public class DestroyableMesh : MonoBehaviour
{
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 5f;

    private Rigidbody _rigidbody;
    private Color _defaultColor;

    protected MeshRenderer MeshRenderer;
    protected float Delay;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    protected virtual void OnEnable()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        MeshRenderer.material.color = _defaultColor;
        Delay = Random.Range(_minDelay, _maxDelay);
    }
}
