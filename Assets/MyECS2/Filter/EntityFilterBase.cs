using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyECS2
{
    public abstract class EntityFilterBase : IFilter
    {
        public EntityFilterBase(EntityHandler entityHandler)
        {
            RegisteredEntities = new EntityObject[0];
            AttachedComponentsHash = new List<int>();
            registeredEntityMap = new Dictionary<int, int>();
            PoolProvider = entityHandler.PoolProvider;
            _filterOperations = new FilterOperation[0];
        }
        internal ComponentPoolProvider PoolProvider { get; private set; }
        protected EntityObject[] RegisteredEntities;
        public int RegisteredEntityCount { get;  set; }
        public List<int> AttachedComponentsHash { get ; set ; }

        int IFilter.RegisteredEntitiesCount => RegisteredEntityCount;

        private bool _isLocked;
        private FilterOperation[] _filterOperations;
        private int _filterOperationsCount;
        protected Dictionary<int, int> registeredEntityMap;
        

        public abstract void UnregisterEntity(ref EntityObject entity);
       

        public void Lock()
        {
            _isLocked = true;
        }

        public abstract void RegisterEntity(ref EntityObject entity);
       
        public ref EntityObject GetEntity(int index)
        {
            return ref RegisteredEntities[index];
        }
        public void Unlock()
        {
            _isLocked = false;
            if (_filterOperationsCount != 0)
            {
                for (int i = 0; i < _filterOperationsCount; i++)    
                {
                    ref var operation = ref _filterOperations[i];
                    if (operation.IsAdd)
                        RegisterEntity(ref operation.entity);
                    else UnregisterEntity(ref operation.entity);
                }
                _filterOperationsCount = 0;
            }
        }

        protected bool CheckIsLocked(ref EntityObject entity, bool isAdd)
        {
            if (_isLocked == false)
                return false;

            if (_filterOperationsCount == _filterOperations.Length)
                Array.Resize(ref _filterOperations, _filterOperations.Length + 1);

            ref var operation = ref _filterOperations[_filterOperationsCount++];
            operation.entity = entity;
            operation.IsAdd = isAdd;
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            return new FilterEnumerator(this);
        }

      

      
    }
}
