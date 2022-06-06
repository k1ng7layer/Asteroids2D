using MyECS2;
using MyObjectPool;
using UnityEngine;

namespace AsteroidsECS
{
    public class BulletControlSystem : IGameSystem
    {
        private EntityFilter<BulletComponent, TransformComponent> _filter;

        public void Initialize()
        {
            
        }

      
        public void OnDestroy()
        {
            
        }

        public void Update()
        {   
            foreach (int item in _filter)
            {

                ref var bullet = ref _filter.GetFirst(item);
                ref var bulletTransform = ref _filter.GetSecond(item);
                ref var bulletEntity = ref _filter.GetEntity(item);
                if (bullet.lifeTime <= 0f)
                {
                    
                    bulletEntity.Remove<BulletComponent>();
                    bulletEntity.Remove<TransformComponent>();
                 
                    TriggerHash.Instance._bulletHash.Remove(bullet.attachedView.gameObject.GetInstanceID());
                    ObjectPoolFacade.ReturnObjectToPool(bullet.attachedView);
                    
                }
                else
                {
                    bulletTransform.Position += bulletTransform.Forward * bullet.speed * Time.deltaTime;

                    bullet.attachedView.SetRotation(bulletTransform.Rotation);
                    bullet.attachedView.SetPosition(ref bulletTransform.Position);
                    bullet.lifeTime -= Time.deltaTime;
                   
                }
                
             
                

            }
        }
    }
}
