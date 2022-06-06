using UnityEngine;

namespace MyObjectPool
{
    public abstract class PooledObject : MonoBehaviour
    {
        [HideInInspector] public int id;
        public abstract void OnCreated();
    }
}
