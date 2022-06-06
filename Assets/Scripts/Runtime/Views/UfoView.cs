using MyObjectPool;
using UnityEngine;

namespace AsteroidsECS
{
    public class UfoView:EnemyView
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerHash.Instance._bulletHash.TryGetValue(collision.gameObject.GetInstanceID(), out BulletView bullet))
            {
                bullet.GetEntity().Destroy();
                bullet.OnViewDestroy();
                //ObjectPoolFacade.ReturnObjectToPool(bullet);
                ref var hit = ref _entityObject.Add<HitComponent>();
            }
        }
    }
}
                

               


