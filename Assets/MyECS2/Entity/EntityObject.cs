using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    public struct EntityObject: IDestroyableEntity
    {
        internal int globalIndex;
        internal int[] registeredComponentsHash;
        internal int[] registeredComonentsIndexInPool;
        internal int componentsHashCount;
        internal int comonentsIndexInPoolCount;
        internal EntityHandler _handler;

        public ref T Add<T>()where T : struct
        {
            var hash = typeof(T).Name.GetHashCode();
            //_handler.PoolProvider.AddComponentToPool<T>(globalIndex,hash, out int pooledIndex);
            //if (componentsHashCount == registeredComponentsHash.Length)
            //    Array.Resize(ref registeredComponentsHash, registeredComponentsHash.Length + 1);

            //if (comonentsIndexInPoolCount == registeredComonentsIndexInPool.Length)
            //    Array.Resize(ref registeredComonentsIndexInPool, registeredComonentsIndexInPool.Length + 1);

            //registeredComponentsHash[componentsHashCount++] = hash;
            //registeredComonentsIndexInPool[comonentsIndexInPoolCount++] = pooledIndex;
            //var pool = _handler.PoolProvider.GetPool<T>(hash);
            //return ref pool.GetComponentFromPool<T>(pooledIndex);

            
           
            var pool = _handler.PoolProvider.GetPool<T>(hash);

            if(!pool.ContainsEntitySlot(globalIndex))
                _handler.AddComponentToPool<T>(ref this, hash);

            var index = _handler.PoolProvider.GetEntityPooledIndex(ref this, hash);
            return ref pool.GetComponentFromPool<T>(index);

            //return ref component;
        }
        public void Remove<T>()
        {
            var hash = typeof(T).Name.GetHashCode();
            _handler.ProcessRemoveComponent<T>(ref this, hash);
        }
        public bool HasComponent<T>() where T:struct
        {
            var hash = typeof(T).Name.GetHashCode();
            var pool = _handler.PoolProvider.GetPool<T>(hash);
            if (pool.ContainsEntitySlot(globalIndex))
                return true;
            return false;
        }
        public void Destroy()
        {
            _handler.DestroyEntity(ref this);
        }
    }
}
