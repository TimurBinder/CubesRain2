using UnityEngine;

abstract public class SubstitudableSpawner<T> : Spawner<T> where T : MonoBehaviour
{
    public void Substitude(Transform transform)
    {
        T spawnable = Pool.Get();
        spawnable.transform.rotation = Quaternion.identity;
        spawnable.transform.position = transform.position;
    }
}
