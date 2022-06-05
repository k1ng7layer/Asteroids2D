using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;

namespace Assets.Scripts.Runtime.Systems
{
    public class GunShootingSystem : IGameSystem
    {
        private EntityFilter<FireGunComponent, ShootComponent> _filter;
        private EntityHandler _entityHandler;
        private BulletSpawnData _bulletSpawnData;


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
                //Debug.Log("Shooting update");
                //ref var fireGun = ref _filter.GetFirst(item);
                //ref var weaponHandler = ref fireGun.handler;
                //var bullet = GameObjectPoolFacade.GetObjectFromPool("Bullet", fireGun._bulletPrefab, fireGun.handler.Position, fireGun.handler.Rotation);
                //ref var bulletEntity = ref _entityHandler.CreateEntity();
                //ref var bulletComp = ref bulletEntity.AddComponent<BulletComponent>();
                //bulletComp.attachedBullet = bullet;
                //bulletComp.speed = 10f;
                //ref var bulletTransform = ref bulletEntity.AddComponent<TransformComponent>();
                //bulletTransform.Position = fireGun.handler.Position;
                //bulletTransform.Rotation = fireGun.handler.Rotation;
                ref var gunEntity = ref _filter.GetEntity(item);
                ref var spawnBullet = ref gunEntity.Add<BulletSpawnComponent>();
                spawnBullet.bulletViewPrefab = _bulletSpawnData.bulletViewPrefab;
                gunEntity.Remove<ShootComponent>();
            }
        }



    }
}


                

