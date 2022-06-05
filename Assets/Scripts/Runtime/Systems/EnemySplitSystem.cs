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
                    //ref var spawner = ref _entityHandler.CreateEntity();
                    //ref var spawnRequest = ref spawner.Add<SpawnComponent>();
                    //spawnRequest.enemyPooledType = "Asteroid";
                    //spawnRequest.viewPrefab = _enemySpawnData.prefabMap["Asteroid"];
                    ////ref var transform = ref entity.Add<TransformComponent>();
                    //spawnRequest.spawnPosition = split.splitPoint;
                    ////Debug.Log($"EnemySplitSystem position = { spawnRequest.spawnPosition}");
                    //Debug.Log($"EnemySplitted at POSITION = {spawnRequest.spawnPosition}");
                    //Quaternion spawnRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(_minSpawnAngleZ, _maxSpawnAngleZ), Vector3.forward);
                    //spawnRequest.spawnRotation = spawnRotation;
                    //spawnRequest.spawnScale = new Vector3(0.5f, 0.5f, 0.5f);
                    //spawnRequest.isSplited = true;
                    //split.spawnedCount++;
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
            //var splitCount = split.splitCount;
            //if (!split.isOriginalDestroyed)
            //{
            //    _countDecreased = true;
            //    split.isOriginalDestroyed = true;
            //    var spawnedCount = _enemySpawnData.spawnedCount["Asteroid"];
            //    _enemySpawnData.spawnedCount["Asteroid"] = --spawnedCount;
            //}
            ref var spawner = ref _entityHandler.CreateEntity();
            ref var spawnRequest = ref spawner.Add<SpawnComponent>();
            spawnRequest.enemyPooledType = "Asteroid";
            spawnRequest.viewPrefab = _enemySpawnData.prefabMap["Asteroid"];
            //ref var transform = ref entity.Add<TransformComponent>();
            spawnRequest.spawnPosition = split.splitPoint;
            //Debug.Log($"EnemySplitSystem position = { spawnRequest.spawnPosition}");
            Debug.Log($"EnemySplitted at POSITION = {spawnRequest.spawnPosition}");

            spawnRequest.randomRotation = true;
            spawnRequest.spawnScale = new Vector3(0.5f, 0.5f, 0.5f);
            spawnRequest.isSplited = true;
            split.spawnedCount++;
        }
    }
}
