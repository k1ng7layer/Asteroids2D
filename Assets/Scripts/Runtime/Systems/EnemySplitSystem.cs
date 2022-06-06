using MyECS2;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemySplitSystem : IGameSystem
    {
        private EntityFilter<EnemyComponent, SplitComponent> _filter;
        private EntityHandler _entityHandler;
        private EnemySpawnData _enemySpawnData;
        private float _minSpawnAngleZ = -180f;
        private float _maxSpawnAngleZ = 180f;
        private bool _countDecreased;
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
                ref var enemyComp = ref _filter.GetFirst(item);
                ref var split = ref _filter.GetSecond(item);
                ref var entity = ref _filter.GetEntity(item);
                PlayerSceneData.Instance.PlayerScore++;
                entity.Remove<TransformComponent>();
                var splitCount = split.splitCount;
                if (!split.isOriginalDestroyed&& enemyComp.isSplited == false)
                {
                    _countDecreased = true;
                    split.isOriginalDestroyed = true;
                    var spawnedCount = _enemySpawnData.spawnedCount["Asteroid"];
                    _enemySpawnData.spawnedCount["Asteroid"] = --spawnedCount;
                }
               

               
                if (split.spawnedCount < splitCount&& enemyComp.isSplited ==false)
                {
                    SplitEnemy(ref split);
                  
                }
                else
                {
                    
                    entity.Remove<SplitComponent>();
                    entity.Destroy();
                }
            }
                

        }
        private void SplitEnemy(ref SplitComponent split)
        {
            ref var spawner = ref _entityHandler.CreateEntity();
            ref var spawnRequest = ref spawner.Add<SpawnComponent>();
            spawnRequest.enemyPooledType = "Asteroid";
            spawnRequest.viewPrefab = _enemySpawnData.prefabMap["Asteroid"];
            spawnRequest.spawnPosition = split.splitPoint;
            spawnRequest.randomRotation = true;
            spawnRequest.spawnScale = new Vector3(0.5f, 0.5f, 0.5f);
            spawnRequest.isSplited = true;
            split.spawnedCount++;
        }
          
    }
  
  

}
