using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Runtime.Data
{
    public class BulletSpawnData
    {
        public BulletSpawnData(BulletView bulletViewPrefab)
        {
            this.bulletViewPrefab = bulletViewPrefab;
        }
        public BulletView bulletViewPrefab;
    }
}
