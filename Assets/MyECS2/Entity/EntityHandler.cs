using System;

namespace MyECS2
{
    public class EntityHandler
    {
        public EntityHandler()
        {
            PoolProvider = new ComponentPoolProvider();
            RegisteredEntities = new EntityObject[15];
        }

        internal EntityObject[] RegisteredEntities;
        internal int RegisteredEntitiesCount { get; private set; }
        internal ComponentPoolProvider PoolProvider { get; private set; }

        //Событие, сигнализирующее об удалении компнонента с Entity
        internal event Action<OnComponentRemoveArgs> OnComponentRemoveFromEntity;
        //Событие, сигнализирующее о добавлении компнонента к Entity
        internal event Action<OnComponentRemoveArgs> OnComponentAddToEntity;
        internal event Action<OnEntityDestroyArgs> OnEntityDestroyEvent;

        public ref EntityObject CreateEntity()
        {
            if (RegisteredEntities.Length == RegisteredEntitiesCount)
                Array.Resize(ref RegisteredEntities, RegisteredEntities.Length + 1);

            EntityObject entityObject = new EntityObject();
            entityObject._handler = this;
            entityObject.globalIndex = RegisteredEntitiesCount;
            entityObject.registeredComponentsHash = new int[0];
            entityObject.registeredComonentsIndexInPool = new int[0];
            RegisteredEntities[RegisteredEntitiesCount] = entityObject;
            return ref RegisteredEntities[RegisteredEntitiesCount++];

        }

        public void RemoveEntity(ref EntityObject entity)
        {
            if (RegisteredEntitiesCount != 0 && entity.globalIndex < RegisteredEntitiesCount)
                RemoveByIndex(ref RegisteredEntities, entity.globalIndex);
        }

        internal void DestroyEntity(ref EntityObject entity)
        {
            OnEntityDestroyArgs args = new OnEntityDestroyArgs(ref entity);
            OnEntityDestroyEvent?.Invoke(args);
        }

        internal void ProcessRemoveComponent<T>(ref EntityObject entity,int hash)
        {
            OnComponentRemoveArgs removeArgs = new OnComponentRemoveArgs(ref entity, hash);
            OnComponentRemoveFromEntity?.Invoke(removeArgs);
        }

        internal void ProcessAddComponent<T>(ref EntityObject entity, int hash)
        {
            OnComponentRemoveArgs addArgs = new OnComponentRemoveArgs(ref entity, hash);
            OnComponentAddToEntity?.Invoke(addArgs);
        }

        internal ref T AddComponentToPool<T>(ref EntityObject entity, int hash) where T:struct
        {
            PoolProvider.AddComponentToPool<T>(entity.globalIndex, hash, out int pooledIndex);
            if (entity.componentsHashCount == entity.registeredComponentsHash.Length)
                Array.Resize(ref entity.registeredComponentsHash, entity.registeredComponentsHash.Length + 1);

            if (entity.comonentsIndexInPoolCount == entity.registeredComonentsIndexInPool.Length)
                Array.Resize(ref entity.registeredComonentsIndexInPool, entity.registeredComonentsIndexInPool.Length + 1);

            entity.registeredComponentsHash[entity.componentsHashCount++] = hash;
            entity.registeredComonentsIndexInPool[entity.comonentsIndexInPoolCount++] = pooledIndex;
            var pool = PoolProvider.GetPool<T>(hash);
            ref var comonent =ref pool.Components[pooledIndex];
            ProcessAddComponent<T>(ref entity, hash);
            return ref comonent;
        }

        private void RemoveByIndex<T>(ref T[] arrayIn, int index) where T : struct
        {
            var newArray = new T[arrayIn.Length - 1];
            Array.Copy(arrayIn, 0, newArray, 0, index);
            Array.Copy(arrayIn, (index + 1), newArray, index, (arrayIn.Length - 1 - index));
            arrayIn = newArray;
        }
    }
}


