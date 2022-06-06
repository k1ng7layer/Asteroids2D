using MyECS2;
using MyObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemySpawnSystem : IGameSystem
    {
        private EntityFilter<SpawnComponent> _filter;
        private EnemySpawnData _enemySpawnData;
        private EntityHandler entityHandler;
        private float _minTargetPontDistance = 2f;
        private float _maxTargetPontSpawnDistance = 4f;
        private float _minSpawnDistance = 10f;
        private float _maxSpawnDistance = 15f;
        private Dictionary<string, int> _spawnedCount;
        private PlayerEntityData _playerData;

        public void Initialize()
        {
            _spawnedCount = new Dictionary<string, int>();
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
                var spawnPoint = _enemySpawnData.SpawnCenters[spawnRequest.enemyPooledType];
                
                var enemyType = spawnRequest.enemyPooledType;
               
                Vector3 spawnPos = GetSpawnPoint(spawnPoint);
                Vector3 targetPoint = GetTargetPoint(spawnPoint);
                Vector3 realitivePos = targetPoint - spawnPos;
                Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, realitivePos);
              
                EnemyView obj = ObjectPoolFacade.GetObjectFromPool(spawnRequest.viewPrefab, spawnPos, quaternion, spawnRequest.spawnScale);
                
                ref var enemy = ref entityHandler.CreateEntity();
               
             
                ref var enemyComp = ref enemy.Add<EnemyComponent>();
                if (spawnRequest.hasTarget == true)
                {
                    ref var target = ref enemy.Add<TargetFollowComponent>();
                    target.targetEntity = _playerData.playerEntity;
                    target.targetTransform = _playerData.transform;
                }
                  

                ref var enemyTransform = ref enemy.Add<TransformComponent>();
                enemyTransform.acceleration = 3f;
                enemyTransform.Position = spawnPos;
                enemyTransform.Rotation = quaternion;
                obj.SetPosition(ref enemyTransform.Position);
                obj.SetRotation(enemyTransform.Rotation);
              
                obj._entityObject = enemy;
                enemyComp.alive = true;
                enemyComp.attachedView = obj;
                enemyComp.attachedGameObject = obj.gameObject;
                enemyComp.enemyType = enemyType;
                enemySpawnerEntity.Remove<SpawnComponent>();
            }
                
             
        }
               
                

        private Vector3 GetTargetPoint(Vector3 inintialSpawnPoint)
        {
            Vector3 spawnPoint = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(_minTargetPontDistance, _maxTargetPontSpawnDistance);
            Vector3 spawnPosition = spawnPoint + inintialSpawnPoint;
            return spawnPosition;
        }

        private Vector3 GetSpawnPoint(Vector3 initialSpawnPoint)
        {
            Vector3 spawnPoint = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(_minSpawnDistance, _maxSpawnDistance);
            Vector3 spawnPosition = spawnPoint + initialSpawnPoint;
            return spawnPosition;
        }
    }
}
