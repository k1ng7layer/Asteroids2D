using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    internal abstract class ComponentPool
    {
        public ComponentPool()
        {
            EntityPoolMap = new Dictionary<int, int>();
        }
        protected Dictionary<int, int> EntityPoolMap;

        internal abstract int HashedComponentsCount { get; set; }

        internal int GetEntityPooledIndex(int entityId)
        {
            if (EntityPoolMap.TryGetValue(entityId, out int pooledIndex))
                return pooledIndex;
            else throw new InvalidOperationException($"there is no pooled component for this entity (id = {entityId})");
        }
        internal bool ContainsEntitySlot(int entityId)
        {
            if (EntityPoolMap.TryGetValue(entityId, out int pooledIndex))
            {
                return true;
            }
            return false;
        }
                
        //internal bool TryGetEntityPooledIndex(int entityId, out int index)
        //{
        //    if (EntityPoolMap.TryGetValue(entityId, out int pooledIndex))
        //    {
        //        index = pooledIndex;
        //        return true;
        //    }
        //    return false;
        //}

        internal void RemoveEntityFromPool(int entityId)
        {
            if (EntityPoolMap.ContainsKey(entityId))
                EntityPoolMap.Remove(entityId);
        }
   
        
    }
}
