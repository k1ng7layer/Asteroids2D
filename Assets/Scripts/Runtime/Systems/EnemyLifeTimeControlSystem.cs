using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
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
                    Debug.Log("EnemyDestroyed by EnemyLifeTimeControlSystem");
                    
                  
                  
                    Debug.Log($"spawned Count = {_enemySpawnData.spawnedCount["Asteroid"]}");
                    entity.Destroy();
                    //entity.Remove<TransformComponent>();
                    //entity.Remove<EnemyComponent>();
                }
            }
        }
    }
}
