using UnityEngine;

namespace AsteroidsECS
{
    public struct BulletComponent
    {
        public GameObject attachedBullet;
        public BulletView attachedView;
        public Vector3 direction;
        public float speed;
        public float lifeTime;
    }
}
