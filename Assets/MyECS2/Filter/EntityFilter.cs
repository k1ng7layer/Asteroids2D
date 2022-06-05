using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    public class EntityFilter<T> : EntityFilterBase where T : struct
    {
        public EntityFilter(EntityHandler entityHandler):base(entityHandler)
        {
            int hash = typeof(T).Name.GetHashCode();
            AttachedComponentsHash.Add(hash);
            _componentPool = PoolProvider.GetPool<T>(hash);
            entitiyIndexInPool = new int[0];
        }
        internal event Action<int, int> OnEntityRemovedFromFilter;

        private EntityComponentPool<T> _componentPool;
        private int[] entitiyIndexInPool;
        public override void RegisterEntity(ref EntityObject entity)
        {
            if (registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, true))
                return;

            if (RegisteredEntityCount == RegisteredEntities.Length)
                Array.Resize(ref RegisteredEntities, RegisteredEntities.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool.Length)
                Array.Resize(ref entitiyIndexInPool, entitiyIndexInPool.Length + 1);

            RegisteredEntities[RegisteredEntityCount] = entity;
            registeredEntityMap.Add(entity.globalIndex, RegisteredEntityCount);
            entitiyIndexInPool[RegisteredEntityCount++] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[0]);
        }
            

        public override void UnregisterEntity(ref EntityObject entity)
        {
            if (!registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, false))
                return;

            var entityGlobalIndex = entity.globalIndex;
            var entityIndexInFilter = registeredEntityMap[entityGlobalIndex];
            registeredEntityMap.Remove(entityGlobalIndex);
            RegisteredEntityCount--;
            if(entityIndexInFilter< RegisteredEntityCount)
            {
                RegisteredEntities[entityIndexInFilter] = RegisteredEntities[RegisteredEntityCount]; //Сдвигаем элемент, находящийся после удаляемого
                var swappedEntity = RegisteredEntities[entityIndexInFilter].globalIndex;
                registeredEntityMap[swappedEntity] = entityIndexInFilter;
                entitiyIndexInPool[entityIndexInFilter] = entitiyIndexInPool[RegisteredEntityCount];
            }
            OnEntityRemovedFromFilter?.Invoke(AttachedComponentsHash[0], entitiyIndexInPool[entityIndexInFilter]);
        }

        public ref T Get( int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool[entityIndex];
            return ref _componentPool.GetComponentFromPool<T>(index);
        }
    }
    public class EntityFilter<T1,T2> : EntityFilterBase where T1 : struct where T2:struct
    {
        public EntityFilter(EntityHandler entityHandler) : base(entityHandler)
        {
            int hash = typeof(T1).Name.GetHashCode();
            int hash1 = typeof(T2).Name.GetHashCode();
            AttachedComponentsHash.Add(hash);
            AttachedComponentsHash.Add(hash1);
            _componentPool1 = PoolProvider.GetPool<T1>(hash);
            _componentPool2 = PoolProvider.GetPool<T2>(hash1);
            entitiyIndexInPool1 = new int[0];
            entitiyIndexInPool2 = new int[0];
        }
        internal event Action<int, int> OnEntityRemovedFromFilter;

        private EntityComponentPool<T1> _componentPool1;
        private EntityComponentPool<T2> _componentPool2;
        private int[] entitiyIndexInPool1;
        private int[] entitiyIndexInPool2;
        public override void RegisterEntity(ref EntityObject entity)
        {
            if (registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, true))
                return;

            if (RegisteredEntityCount == RegisteredEntities.Length)
                Array.Resize(ref RegisteredEntities, RegisteredEntities.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool1.Length)
                Array.Resize(ref entitiyIndexInPool1, entitiyIndexInPool1.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool2.Length)
                Array.Resize(ref entitiyIndexInPool2, entitiyIndexInPool2.Length + 1);

            RegisteredEntities[RegisteredEntityCount] = entity;
            registeredEntityMap.Add(entity.globalIndex, RegisteredEntityCount);
            entitiyIndexInPool1[RegisteredEntityCount] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[0]);
            entitiyIndexInPool2[RegisteredEntityCount++] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[1]);
        }


        public override void UnregisterEntity(ref EntityObject entity)
        {
            if (!registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, false))
                return;

            var entityGlobalIndex = entity.globalIndex;
            var entityIndexInFilter = registeredEntityMap[entityGlobalIndex];
            registeredEntityMap.Remove(entityGlobalIndex);
            RegisteredEntityCount--;
            if (entityIndexInFilter < RegisteredEntityCount)
            {
                RegisteredEntities[entityIndexInFilter] = RegisteredEntities[RegisteredEntityCount]; //Сдвигаем элемент, находящийся после удаляемого
                var swappedEntity = RegisteredEntities[entityIndexInFilter].globalIndex;
                registeredEntityMap[swappedEntity] = entityIndexInFilter;
                entitiyIndexInPool1[entityIndexInFilter] = entitiyIndexInPool1[RegisteredEntityCount];
                entitiyIndexInPool2[entityIndexInFilter] = entitiyIndexInPool2[RegisteredEntityCount];
            }
            //OnEntityRemovedFromFilter?.Invoke(AttachedComponentsHash[0], entitiyIndexInPool[entityIndexInFilter]);
        }

        public ref T1 GetFirst(int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool1[entityIndex];

            return ref _componentPool1.GetComponentFromPool<T1>(index);
        }
        public ref T2 GetSecond( int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool2[entityIndex];
            return ref _componentPool2.GetComponentFromPool<T2>(index);

        }
           
    }
    public class EntityFilter<T1, T2,T3> : EntityFilterBase where T1 : struct where T2 : struct where T3:struct
    {
        public EntityFilter(EntityHandler entityHandler) : base(entityHandler)
        {
            int hash = typeof(T1).Name.GetHashCode();
            int hash1 = typeof(T2).Name.GetHashCode();
            int hash2 = typeof(T3).Name.GetHashCode();
            AttachedComponentsHash.Add(hash);
            AttachedComponentsHash.Add(hash1);
            AttachedComponentsHash.Add(hash2);
            _componentPool1 = PoolProvider.GetPool<T1>(hash);
            _componentPool2 = PoolProvider.GetPool<T2>(hash1);
            _componentPool3 = PoolProvider.GetPool<T3>(hash2);
            entitiyIndexInPool1 = new int[0];
            entitiyIndexInPool2 = new int[0];
            entitiyIndexInPool3 = new int[0];
        }
        internal event Action<int, int> OnEntityRemovedFromFilter;

        private EntityComponentPool<T1> _componentPool1;
        private EntityComponentPool<T2> _componentPool2;
        private EntityComponentPool<T3> _componentPool3;
        private int[] entitiyIndexInPool1;
        private int[] entitiyIndexInPool2;
        private int[] entitiyIndexInPool3;
        public override void RegisterEntity(ref EntityObject entity)
        {
            if (registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, true))
                return;

            if (RegisteredEntityCount == RegisteredEntities.Length)
                Array.Resize(ref RegisteredEntities, RegisteredEntities.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool1.Length)
                Array.Resize(ref entitiyIndexInPool1, entitiyIndexInPool1.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool2.Length)
                Array.Resize(ref entitiyIndexInPool2, entitiyIndexInPool2.Length + 1);
            if (RegisteredEntityCount == entitiyIndexInPool3.Length)
                Array.Resize(ref entitiyIndexInPool3, entitiyIndexInPool3.Length + 1);

            RegisteredEntities[RegisteredEntityCount] = entity;
            registeredEntityMap.Add(entity.globalIndex, RegisteredEntityCount);
            entitiyIndexInPool1[RegisteredEntityCount] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[0]);
            entitiyIndexInPool2[RegisteredEntityCount] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[1]);
            entitiyIndexInPool3[RegisteredEntityCount++] = PoolProvider.GetEntityPooledIndex(ref entity, AttachedComponentsHash[2]);
        }


        public override void UnregisterEntity(ref EntityObject entity)
        {
            if (!registeredEntityMap.ContainsKey(entity.globalIndex))
                return;

            if (CheckIsLocked(ref entity, false))
                return;


            var entityGlobalIndex = entity.globalIndex;
            var entityIndexInFilter = registeredEntityMap[entityGlobalIndex];
            registeredEntityMap.Remove(entityGlobalIndex);
            RegisteredEntityCount--;
            if (entityIndexInFilter < RegisteredEntityCount)
            {
                RegisteredEntities[entityIndexInFilter] = RegisteredEntities[RegisteredEntityCount]; //Сдвигаем элемент, находящийся после удаляемого
                var swappedEntity = RegisteredEntities[entityIndexInFilter].globalIndex;
                registeredEntityMap[swappedEntity] = entityIndexInFilter;
                entitiyIndexInPool1[entityIndexInFilter] = entitiyIndexInPool1[RegisteredEntityCount];
                entitiyIndexInPool2[entityIndexInFilter] = entitiyIndexInPool2[RegisteredEntityCount];
                entitiyIndexInPool3[entityIndexInFilter] = entitiyIndexInPool3[RegisteredEntityCount];
            }
            //OnEntityRemovedFromFilter?.Invoke(AttachedComponentsHash[0], entitiyIndexInPool[entityIndexInFilter]);
        }

        public ref T1 GetFirst(int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool1[entityIndex];

            return ref _componentPool1.GetComponentFromPool<T1>(index);
        }
        public ref T2 GetSecond(int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool2[entityIndex];
            return ref _componentPool2.GetComponentFromPool<T2>(index);

          
        }
        public ref T3 GetThird(int entityIndex)
        {
            //возможно нужно будет заменить на взятие компонента по ключу в мапе, находящейся в пуле
            var index = entitiyIndexInPool3[entityIndex];
            return ref _componentPool3.GetComponentFromPool<T3>(index);

        }

    }
}
