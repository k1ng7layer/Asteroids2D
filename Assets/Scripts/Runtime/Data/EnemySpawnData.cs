using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemySpawnData
    {
       
        public Dictionary<string, Vector3> SpawnCenters;
        public Dictionary<string, Quaternion> SpawnRotation;
        public Dictionary<string,int> countToSpawn;
        public Dictionary<string, EnemyView> prefabMap;
        public Dictionary<string, int> spawnedCount;
        public float minTargetPontDistance = 2f;
        public float maxTargetPontSpawnDistance = 4f;
        public float minSpawnDistance = 10f;
        public float maxSpawnDistance = 15f;

        public EnemySpawnData()
        {
            SpawnCenters = new Dictionary<string, Vector3>();
            SpawnRotation = new Dictionary<string, Quaternion>();
            countToSpawn = new Dictionary<string, int>();
            prefabMap = new Dictionary<string, EnemyView>();
            spawnedCount = new Dictionary<string, int>();
        }
    }
}
