using Assets.Scripts.Runtime.Views;
using MyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data
{
    public class TriggerHash : MonoSingleton<TriggerHash>
    {
        //public Dictionary<int, BulletView> _bulletsHash = new Dictionary<int, BulletView>();
        public Dictionary<int, EnemyView> _enemyHash = new Dictionary<int, EnemyView>();
        public Dictionary<int, BulletView> _bulletHash = new Dictionary<int, BulletView>();
    }
}
