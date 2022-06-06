using MyECS2;
using MyObjectPool;
using UnityEngine;

namespace AsteroidsECS
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
          
            

            ref var enemy = ref entityHandler.CreateEntity();
            ref var enemyComp = ref enemy.Add<EnemyComponent>();
            if (spawnRequest.hasTarget == true)
            {
                ref var target = ref enemy.Add<TargetFollowComponent>();
                target.targetEntity = _playerData.playerEntity;
                target.targetTransform = _playerData.transform;
            }
            Quaternion spawnRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(_minSpawnAngleZ, _maxSpawnAngleZ), Vector3.forward);
          
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
            if(spawnRequest.spawned==spawnRequest.countToSpawn)
                spawner.Remove<SpawnComponent>();
        }

    }








}
