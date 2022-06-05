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
    public class AsteroidView:EnemyView
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerHash.Instance._bulletHash.TryGetValue(collision.gameObject.GetInstanceID(), out BulletView bullet))
            {
                
                //ObjectPoolFacade.ReturnObjectToPool(bullet);
                bullet.OnViewDestroy();
                ref var hit = ref _entityObject.Add<SplitComponent>();
                hit.splitPoint = Position;
                hit.splitCount = 2;
                ref var bulletEntity = ref bullet.GetEntity();

                bullet.GetEntity().Destroy();
            }
        }
    }
}
                
              
               


                
                
                

