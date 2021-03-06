using MyECS2;
using MyObjectPool;

namespace AsteroidsECS
{
    public class SpawnSystem : IGameSystem
    {
        private EntityFilter<SpawnComponent> _filter;
        private EntityHandler entityHandler;
        private PlayerEntityData _playerData;
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
                var enemyType = spawnRequest.enemyPooledType;
               
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
                ref var enemyTransform = ref enemy.Add<TransformComponent>();
                enemyTransform.acceleration = 3f;
                enemyTransform.Position = spawnRequest.spawnPosition;
                enemyTransform.Rotation = spawnRequest.spawnRotation;
                obj.SetPosition(ref enemyTransform.Position);
                obj.SetRotation(enemyTransform.Rotation);
                obj._entityObject = enemy;
                enemyComp.alive = true;
                enemyComp.attachedView = obj;
                enemyComp.attachedGameObject = obj.gameObject;
                enemyComp.enemyType = enemyType;
                enemyComp.isSplited = spawnRequest.isSplited;
                enemySpawnerEntity.Remove<SpawnComponent>();
            }
        }
      
    }

                





              
}
