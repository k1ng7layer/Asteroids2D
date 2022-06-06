using MyExtensions;
using System.Collections.Generic;

namespace AsteroidsECS
{
    public class TriggerHash : MonoSingleton<TriggerHash>
    {
        //public Dictionary<int, BulletView> _bulletsHash = new Dictionary<int, BulletView>();
        public Dictionary<int, EnemyView> _enemyHash = new Dictionary<int, EnemyView>();
        public Dictionary<int, BulletView> _bulletHash = new Dictionary<int, BulletView>();
    }
}
