using Assets.Interfaces;
using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using Assets.Scripts.Runtime.Factories;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class PlayerInit : IGameSystem
    {
        private EntityHandler _entityHandler;
        private PlayerEntityData _playerEntityData;
        public void Initialize()
        {
            ViewFactory playerViewFactory = new PlayerViewFactory();
            ITransformableView playerView = playerViewFactory.GetTransformableView();
            //PlayerMovementSystem playerMovement = new PlayerMovementSystem(playerView);
            ref EntityObject player = ref _entityHandler.CreateEntity();
            ref var playerInput = ref player.Add<PlayerInputComponent>();
            ref var playerTransform = ref player.Add<TransformComponent>();
            ref var playerRotation = ref player.Add<RotationComponent>();
            ref var playerAcceleration = ref player.Add<AccelerationComponent>();
            ref var playerAccelRot = ref player.Add<RotationAccelerationComponent>();
            ref var playerfireGunWeapon = ref player.Add<PlayerFireGunWeaponComponent>();
            ref var playerLaserGunWeapon = ref player.Add<PlayerLaserGunWeaponComponent>();
            ref var playerScreenPos = ref player.Add<ScreenPositionComponent>();
            var playerWidth = playerViewFactory.SpawnedPlayerObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            var playerHeight = playerViewFactory.SpawnedPlayerObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            playerScreenPos.objectHeight = playerHeight;
            playerScreenPos.objectWidth = playerWidth;
            ref EntityObject fireGunEntity = ref _entityHandler.CreateEntity();
            ref EntityObject laserGunEntity = ref _entityHandler.CreateEntity();
            ref var fireGun = ref fireGunEntity.Add<FireGunComponent>();
            ref var laserGun = ref laserGunEntity.Add<LaserGunComponent>();
            WeaponViewFactory viewFactory = new WeaponViewFactory(playerViewFactory.SpawnedPlayerObject);
            laserGun.handler = playerView;
            laserGun.maxDuration = 3f;
            laserGun.weaponView = viewFactory.CreateWeaponView(WeaponType.LASER_GUN);
            laserGun.weaponView.InitializeView();
            fireGun.handler = playerView;
            playerfireGunWeapon.weaponEntity = fireGunEntity;
            playerLaserGunWeapon.weaponEntity = laserGunEntity;
            playerView.SetEntity(ref player);
            _playerEntityData.playerEntity = player;
            _playerEntityData.transform = playerTransform;
            _playerEntityData.PlayerPosition = playerTransform.Position;
            _playerEntityData.playerView = playerView;
            //PlayerEntityData playerEntity = new PlayerEntityData(ref playerTransform, ref player);
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
