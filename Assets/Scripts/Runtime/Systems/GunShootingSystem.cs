﻿using Assets.Scripts.Runtime.Components;
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

                ref var gunEntity = ref _filter.GetEntity(item);
                ref var spawnBullet = ref gunEntity.Add<BulletSpawnComponent>();
                spawnBullet.bulletViewPrefab = _bulletSpawnData.bulletViewPrefab;
                gunEntity.Remove<ShootComponent>();
            }
        }



    }
}


                

