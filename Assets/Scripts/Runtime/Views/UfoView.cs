using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Views
{
    public class UfoView:EnemyView
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerHash.Instance._bulletHash.TryGetValue(collision.gameObject.GetInstanceID(), out BulletView bullet))
            {
                bullet.GetEntity().Destroy();
                bullet.OnViewDestroy();
                ObjectPoolFacade.ReturnObjectToPool(bullet);
                ref var hit = ref _entityObject.Add<HitComponent>();
            }
        }
    }
}
                

               


