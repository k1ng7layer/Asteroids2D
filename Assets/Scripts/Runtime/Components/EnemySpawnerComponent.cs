using UnityEngine;

namespace AsteroidsECS
{
    public struct EnemySpawnerComponent
    {
        public string enemyPooledType;
        public Vector3 normalScale;
        public int countToSpawn;
        public bool hasTarget;
    }
}
