using UnityEngine;

namespace AsteroidsECS
{
    public struct EnemyComponent
    {
        public GameObject attachedGameObject;
        public EnemyView attachedView;
        public bool alive;
        public string enemyType;
        public bool isSplited;
    }
}
