using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Platform : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public Vector3 GetRandomPosition(float padding)
    {
        float minX = _collider.bounds.min.x + padding;
        float maxX = _collider.bounds.max.x - padding;
        float minZ = _collider.bounds.min.z + padding;
        float maxZ = _collider.bounds.max.z - padding;

        float positionX = Random.Range(minX, maxX);
        float positionY = _collider.bounds.min.y;
        float positionZ = Random.Range(minZ, maxZ);
        return new Vector3(positionX, positionY, positionZ);
    }
}
