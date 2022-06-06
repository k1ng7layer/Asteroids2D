using UnityEngine;

namespace AsteroidsECS
{
    public struct SplitComponent
    {
        public int splitCount;
        public int spawnedCount;
        public EnemyView splitedView;
        public Vector3 splitPoint;
        public bool isOriginalDestroyed;
    }
}
