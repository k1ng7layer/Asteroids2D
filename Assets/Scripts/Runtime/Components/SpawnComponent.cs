using UnityEngine;

namespace AsteroidsECS
{
    public struct SpawnComponent
    {
        public EnemyView viewPrefab;
        public string enemyPooledType;
        public Vector3 spawnScale;
        public Vector3 spawnPosition;
        public Quaternion spawnRotation;
        public bool randomRotation;
        public bool isSplited;
        public bool hasTarget;
        public int countToSpawn;
        public int spawned;
    }
}
