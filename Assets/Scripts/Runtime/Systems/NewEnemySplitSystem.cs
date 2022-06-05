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
    public class NewEnemySplitSystem:IGameSystem
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

                var splitCount = split.splitCount;
                if (enemyComp.isSplited == false)
                {
                    _countDecreased = true;
                    split.isOriginalDestroyed = true;
                    var spawnedCount = _enemySpawnData.spawnedCount["Asteroid"];
                    _enemySpawnData.spawnedCount["Asteroid"] = --spawnedCount;
                    SplitEnemy(ref split);
                }
                PlayerSceneData.Instance.PlayerScore++;

                ObjectPoolFacade.ReturnObjectToPool(enemyComp.attachedView);
                entity.Destroy();

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
            spawnRequest.countToSpawn = split.splitCount;

        }
    }
}
