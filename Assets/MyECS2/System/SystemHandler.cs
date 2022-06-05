using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MyECS2
{
    public class SystemHandler
    {
        public SystemHandler(EntityHandler entityHandler)
        {
            _entityHandler = entityHandler;
            _gameSystems = new List<IGameSystem>();
            _filters = new List<IFilter>();
            _objectContainer = new ObjectContainer();
            _entityHandler.OnComponentRemoveFromEntity += HandleComponentRemove;
            _entityHandler.OnComponentAddToEntity += HandleComponentAdd;
            _entityHandler.OnEntityDestroyEvent += DestroyEntity;
        }

        private ObjectContainer _objectContainer;   
        private EntityHandler _entityHandler;
        private List<IGameSystem> _gameSystems;
        private List<IFilter> _filters;
        private bool _initialized;
        private bool _running;

        public void Initialize()
        {
            if (!_initialized)
                    StartUp();
        }

        /// <summary>
        /// Инициализация системы
        /// </summary>
        private void StartUp()
        {
            _initialized = true;
            foreach (var system in _gameSystems)
            {
              
                var systemType = system.GetType();
                var properties = systemType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (var item in properties)
                {
                    var filterProperty = item.FieldType;
                    if (filterProperty.GetInterface("IFilter") == typeof(IFilter))
                    {
                        var filter = (IFilter)Activator.CreateInstance(item.FieldType, _entityHandler);
                        item.SetValue(system, filter);
                        _filters.Add(filter);

                        InitialFilterProcessing(filter);
                    }
                    if (filterProperty == typeof(EntityHandler))
                    {
                        item.SetValue(system, _entityHandler);
                    }
                    InjectDependencies(item, system);
                }
            }
            InitializeSystems();
        }

        /// <summary>
        /// Первоначальная обработка фильтров
        /// </su
        /// <param name="filter"></param>
        private void InitialFilterProcessing(IFilter filter)
        {
            for (int i = 0; i < _entityHandler.RegisteredEntitiesCount; i++)
            {
                if (HashCheck(filter, ref _entityHandler.RegisteredEntities[i]))
                    filter.RegisterEntity(ref _entityHandler.RegisteredEntities[i]);
            }
        }

        internal void DestroyEntity(OnEntityDestroyArgs args)
        {
            foreach (var filter in _filters)    
            {
                filter.UnregisterEntity(ref args.entityObject);
            }
        }
        /// <summary>
        /// Внедрение зависимостей в систему
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="system"></param>
        private void InjectDependencies(FieldInfo fieldInfo, IGameSystem system)
        {
            var filedType = fieldInfo.FieldType;
            if (_objectContainer.Contains(filedType))
                fieldInfo.SetValue(system, _objectContainer.ResolveDependency(filedType));
        }

        /// <summary>
        /// Добавить систему в обработчик систем
        /// </summary>
        /// <param name="gameSystem"></param>
        //public void AddSystem(IGameSystem gameSystem)
        //{
        //    _gameSystems.Add(gameSystem);
        //}
        public SystemHandler AddSystem(IGameSystem gameSystem)
        {
            _gameSystems.Add(gameSystem);
            return this;
        }

        /// <summary>
        /// Проверка на соответствие Entity ограничениям фильтра
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool HashCheck(IFilter filter, ref EntityObject entity)
        {
            var checkCount = filter.AttachedComponentsHash.Count;
            int passedChecks = 0;
            for (int i = 0; i < checkCount; i++)
            {
                if (entity.registeredComponentsHash.Contains(filter.AttachedComponentsHash[i]))
                    passedChecks++;
            }
            if (passedChecks == checkCount)
                return true;

            return false;
        }
        /// <summary>
        /// Обработка удаления компонена из Entity
        /// </summary>
        /// <param name="args"></param>
        internal void HandleComponentRemove(OnComponentRemoveArgs args)
        {
            var filters = _filters.Where(f => f.AttachedComponentsHash.Contains(args.removedComponentHash));
            foreach (var filter in filters)    
            {
                filter.UnregisterEntity(ref args.entityObject);
            }
            _entityHandler.PoolProvider.RemoveComponentFromPoolByEntityId(args.removedComponentHash,args.entityObject.globalIndex);
        }

        /// <summary>
        /// Обработка добавления компонена к Entity
        /// </summary>
        /// <param name="args"></param>
        internal void HandleComponentAdd(OnComponentRemoveArgs args)
        {
            if (_running)
            {
                var filters = _filters.Where(f => f.AttachedComponentsHash.Contains(args.removedComponentHash));
                foreach (var filter in filters)
                {
                    if (HashCheck(filter, ref args.entityObject))
                        filter.RegisterEntity(ref args.entityObject);
                }
            }
           
        }

        /// <summary>
        /// Добавить зависимость в систему
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public SystemHandler Inject<T>(T instance)
        {
            _objectContainer.AddDependency<T>(instance);
            return this;
        }

        internal void InitializeSystems()
        {
            foreach (var system in _gameSystems)
            {
                system.Initialize();
            }
        }

        public void Update()
        {
            _running = true;
            foreach (var system in _gameSystems)
            {
                system.Update();    
            }
        }

        public void Destroy()
        {
            _entityHandler.OnComponentRemoveFromEntity -= HandleComponentRemove;
            _entityHandler.OnComponentAddToEntity -= HandleComponentAdd;
            _entityHandler.OnEntityDestroyEvent -= DestroyEntity;
            foreach (var system in _gameSystems)
            {
                system.OnDestroy();
            }
        }
    }
}

