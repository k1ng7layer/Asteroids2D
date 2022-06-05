using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using Assets.Scripts.Runtime.Views;
using MyECS2;
using MyObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class NewSpawnSystem : IGameSystem
    {
        private EntityFilter<SpawnComponent> _filter;
        private EntityHandler entityHandler;
        private PlayerEntityData _playerData;
        private float _minSpawnAngleZ = -180f;
        private float _maxSpawnAngleZ = 180f;
        public void Initialize()
        {

        }

        public void OnDestroy()
        {

        }

        public void Update()
        {
            foreach (int item in _filter)
            {
                ref var enemySpawnerEntity = ref _filter.GetEntity(item);
                ref var spawnRequest = ref _filter.Get(item);
                // var enemyType = spawnRequest.enemyPooledType;
                if (spawnRequest.spawned<spawnRequest.countToSpawn)
                    Spawn(ref spawnRequest, ref enemySpawnerEntity);
                else enemySpawnerEntity.Remove<SpawnComponent>();



            }
        }
        private void Spawn(ref SpawnComponent spawnRequest, ref EntityObject spawner)
        {
            EnemyView obj = ObjectPoolFacade.GetObjectFromPool(spawnRequest.viewPrefab,
                                                                   spawnRequest.spawnPosition,
                                                                   spawnRequest.spawnRotation,
                                                                   spawnRequest.spawnScale);
            Debug.Log($"EnemySpawned at POSITION = {obj.transform.position}");
            //var obj = GameObject.Instantiate(spawnRequest.viewPrefab, spawnRequest.spawnPosition, spawnRequest.spawnRotation);

            ref var enemy = ref entityHandler.CreateEntity();
            ref var enemyComp = ref enemy.Add<EnemyComponent>();
            if (spawnRequest.hasTarget == true)
            {
                ref var target = ref enemy.Add<TargetFollowComponent>();
                target.targetEntity = _playerData.playerEntity;
                target.targetTransform = _playerData.transform;
            }
            Quaternion spawnRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(_minSpawnAngleZ, _maxSpawnAngleZ), Vector3.forward);
            Debug.Log($"Spawned rotation  = {spawnRotation.eulerAngles}");
            ref var enemyTransform = ref enemy.Add<TransformComponent>();
            enemyTransform.acceleration = 3f;
            enemyTransform.Position = spawnRequest.spawnPosition;

            if (spawnRequest.randomRotation)
                enemyTransform.Rotation = spawnRotation;
            else enemyTransform.Rotation = spawnRequest.spawnRotation;

            obj.SetPosition(ref enemyTransform.Position);
            obj.SetRotation(enemyTransform.Rotation);
            obj._entityObject = enemy;
            enemyComp.alive = true;
            enemyComp.attachedView = obj;
            enemyComp.attachedGameObject = obj.gameObject;
            enemyComp.enemyType = spawnRequest.enemyPooledType;
            enemyComp.isSplited = spawnRequest.isSplited;
            spawnRequest.spawned++;
            //spawner.Remove<SpawnComponent>();
        }

    }








}
