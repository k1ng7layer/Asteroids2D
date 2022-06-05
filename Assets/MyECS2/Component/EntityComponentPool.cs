using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;

namespace MyECS2
{
    internal class EntityComponentPool<T> : ComponentPool where T : struct
    {
        public EntityComponentPool()
        {
            Components = new T[10];
        }
        internal T[] Components;
        internal override int HashedComponentsCount { get ; set; }

        internal void AddComponentToPool(ref T instance)
        {
            if (Components.Length == HashedComponentsCount)
                Array.Resize(ref Components, Components.Length + 1);

            Components[HashedComponentsCount++] = instance;
        }
        //internal void AddComponentToPool(ref T instance, out int pooledIndex)
        //{
        //    if (Components.Length == HashedComponentsCount)
        //        Array.Resize(ref Components, Components.Length + 1);

        //    pooledIndex = HashedComponentsCount;
        //    Components[HashedComponentsCount++] = instance;
        //}
        internal ref T AddComponentToPool(int entityId, ref T instance, out int pooledIndex)
        {
            if (!EntityPoolMap.ContainsKey(entityId))
            {
                if (Components.Length == HashedComponentsCount)
                    Array.Resize(ref Components, Components.Length + 1);

                pooledIndex = HashedComponentsCount;
                EntityPoolMap.Add(entityId, HashedComponentsCount);
                Components[HashedComponentsCount++] = instance;
                return ref Components[pooledIndex];
            }
            var index = EntityPoolMap[entityId];
            pooledIndex = index;
            return ref Components[index];


        }
           

        internal void RemoveComponentFromPool<T>(ref T component, int pooledIndex)
        {
            if (HashedComponentsCount != 0)
                RemoveByIndex(ref Components, pooledIndex);

            HashedComponentsCount--;
            Components[pooledIndex] = Components[HashedComponentsCount];
        }

        internal void RemoveFromPoolByEntityId(int entityGlobalId)
        {
            if (EntityPoolMap.TryGetValue(entityGlobalId, out int pooledIndex))
            {

                var entityMapIndex = pooledIndex;
                EntityPoolMap.Remove(entityGlobalId);
                HashedComponentsCount--;
                if(entityMapIndex< HashedComponentsCount)
                {
                    Components[entityMapIndex] = Components[HashedComponentsCount];
                    EntityPoolMap[entityMapIndex] = HashedComponentsCount;
                }
                
            }
        }


        private void RemoveByIndex<T>(ref T[] arrayIn, int index) where T : struct
        {
            var newArray = new T[arrayIn.Length - 1];
            Array.Copy(arrayIn, 0, newArray, 0, index);
            Array.Copy(arrayIn, (index + 1), newArray, index, (arrayIn.Length - 1 - index));
            arrayIn = newArray;
        }
        internal ref T GetComponentFromPool<T1>(int index) where T1:struct
        {
            ref var component = ref Components[index];
            return ref component;
        }
    }
}



