using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using MyObjectPool;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class GunBulletSpawnSystem : IGameSystem
    {
        private EntityFilter<FireGunComponent, BulletSpawnComponent> _filter;
        private EntityHandler _entityHandler;


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
                ref var gun = ref _filter.GetFirst(item);
                ref var spawnInfo = ref _filter.GetSecond(item);
               
                var bulletView = ObjectPoolFacade.GetObjectFromPool(spawnInfo.bulletViewPrefab, gun.handler.Position, gun.handler.Rotation);
                TriggerHash.Instance._bulletHash.Add(bulletView.gameObject.GetInstanceID(), bulletView);
                ref var bulletEntity = ref _entityHandler.CreateEntity();
                ref var bulletComp = ref bulletEntity.Add<BulletComponent>();
                bulletComp.attachedView = bulletView;
                
                bulletComp.attachedView = bulletView;
                bulletComp.speed = 15f;
                bulletComp.lifeTime = 4f;
                ref var bulletTransform = ref bulletEntity.Add<TransformComponent>();
                bulletTransform.Position = gun.handler.Position;
                bulletTransform.Rotation = gun.handler.Rotation;
                bulletView.SetEntity(ref bulletEntity);
                ref var gunEntity = ref _filter.GetEntity(item);
                
                gunEntity.Remove<BulletSpawnComponent>();
                
            }
        }
    }
}
