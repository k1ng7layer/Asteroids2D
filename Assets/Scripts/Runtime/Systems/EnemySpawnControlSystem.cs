using MyECS2;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemySpawnControlSystem : IGameSystem
    {
        private EntityFilter<EnemySpawnerComponent> _filter;
        private EnemySpawnData _enemySpawnData; 
        private float _minTargetPontDistance = 2f;
        private float _maxTargetPontSpawnDistance = 4f;
        private float _minSpawnDistance = 10f;
        private float _maxSpawnDistance = 15f;
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
                ref var spawner = ref _filter.Get(item);
                ref var enemySpawnerEntity = ref _filter.GetEntity(item);
                var counToSpawn = _enemySpawnData.countToSpawn[spawner.enemyPooledType];
                var enemyType = spawner.enemyPooledType;
                
                if (_enemySpawnData.spawnedCount.TryGetValue(enemyType, out int count))
                {
                   
                    if (count < spawner.countToSpawn)
                    {
                       
                        ref var spawnRequest = ref enemySpawnerEntity.Add<SpawnComponent>();

                        if (spawner.hasTarget)
                            spawnRequest.hasTarget = true;

                        spawnRequest.enemyPooledType = enemyType;
                        spawnRequest.spawnScale = spawner.normalScale;
                        var spawnPoint = _enemySpawnData.SpawnCenters[spawnRequest.enemyPooledType];
                        Vector3 spawnPos = GetSpawnPoint(spawnPoint);
                        Vector3 targetPoint = GetTargetPoint(spawnPoint);
                        Vector3 realitivePos = targetPoint - spawnPos;
                        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, realitivePos);
                        spawnRequest.spawnPosition = spawnPos;
                        spawnRequest.spawnRotation = spawnRotation;
                        spawnRequest.viewPrefab = _enemySpawnData.prefabMap[enemyType];
                        spawnRequest.countToSpawn = 1;
                        spawnRequest.randomRotation = false;
                        count++;                     
                        _enemySpawnData.spawnedCount[enemyType] = count;
                    }
                }
                else
                {
                    _enemySpawnData.spawnedCount.Add(enemyType, 0);
                }

             
                  

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
