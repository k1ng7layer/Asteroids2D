using MyECS2;
using MyObjectPool;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemyLifeTimeControlSystem : IGameSystem
    {
        EntityFilter<EnemyComponent, TransformComponent> _filter;
        private EnemySpawnData _enemySpawnData;
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
                ref var entity = ref _filter.GetEntity(item);
                ref var enemy = ref _filter.GetFirst(item);
                ref var enemyTransform = ref _filter.GetSecond(item);
                var spawnPoint = _enemySpawnData.SpawnCenters[enemy.enemyType];
                var distance = Vector3.Distance(enemyTransform.Position, spawnPoint);
                if (distance > 15f)
                {
                    ObjectPoolFacade.ReturnObjectToPool(enemy.attachedView);
                    enemy.attachedView.OnViewDestroy();
                    if (enemy.isSplited == false)
                    {
                        var spawnedCount = _enemySpawnData.spawnedCount[enemy.enemyType];

                        _enemySpawnData.spawnedCount[enemy.enemyType] = --spawnedCount;
                    }
                    entity.Destroy();
                }
                   
                    
                  
                  
                   
                   
            }
        }
    }
}
