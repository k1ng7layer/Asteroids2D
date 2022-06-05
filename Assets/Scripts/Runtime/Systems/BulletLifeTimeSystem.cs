using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using MyObjectPool;

namespace Assets.Scripts.Runtime.Systems
{
    public class BulletLifeTimeSystem : IGameSystem
    {
        private EntityFilter<BulletComponent, TransformComponent> _filter;

        public void Initialize()
        {
            
        }

        public void LateUpdate()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {   
            foreach (int item in _filter)
            {
                ref var bulletEntity = ref _filter.GetEntity(item);
                ref var bullet = ref _filter.GetFirst(item);
                if (bullet.lifeTime <= 0f)
                {                  
                    bulletEntity.Remove<BulletComponent>();                  
                    ObjectPoolFacade.ReturnObjectToPool(bullet.attachedView);
                    TriggerHash.Instance._bulletHash.Remove(bullet.attachedBullet.GetInstanceID());
                    bulletEntity.Remove<TransformComponent>();
                    bulletEntity.Destroy();
                }
            }
        }
    }
}
