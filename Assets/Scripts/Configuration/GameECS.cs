using Assets.Interfaces;
using Assets.Runtime.Systems;
using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using Assets.Scripts.Runtime.Factories;
using Assets.Scripts.Runtime.Systems;
using Assets.Scripts.Runtime.UI;
using Assets.Scripts.Runtime.Views;
using MyECS2;
using MyObjectPool;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/ECS CFG")]
    public class GameECS: ECSConfiguration
    {
        public override SystemHandler GetSystemHandler(SystemConfiguration systemConfiguration)
        {
            Camera _mainCamera = FindObjectOfType<Camera>();


            ObjectPoolFacade.CreatePoolingSlot<EnemyView>(systemConfiguration.Root.Enemy.AsteroidPrefab, systemConfiguration.Root.Enemy.AsteroidCount * 2);
            ObjectPoolFacade.CreatePoolingSlot<EnemyView>(systemConfiguration.Root.Enemy.UfoPrefab, systemConfiguration.Root.Enemy.UfoCount);
            var entityHandler = systemConfiguration.EntityHandler;

            _systemHandler = new SystemHandler(systemConfiguration.EntityHandler);
            PlayerInputSystem inputSystem = new PlayerInputSystem();
            ViewFactory playerViewFactory = new PlayerViewFactory();
            ITransformableView playerView = playerViewFactory.GetTransformableView();
            PlayerMovementSystem playerMovement = new PlayerMovementSystem(playerView);
            PlayerInputData inputData = new PlayerInputData(new Controlls());
            AccelerationSystem accelerationSystem = new AccelerationSystem();
            PlayerRotationSystem rotationSystem = new PlayerRotationSystem(playerView);
            AccelerateRotationSystem accelerateRotation = new AccelerateRotationSystem();
            GunShootingSystem gunShooting = new GunShootingSystem();
            GunBulletSpawnSystem bulletSpawnSystem = new GunBulletSpawnSystem();
            BulletControlSystem bulletControl = new BulletControlSystem();
            BulletLifeTimeSystem bulletLifeTime = new BulletLifeTimeSystem();
            LaserShotSystem laserShot = new LaserShotSystem();
            LaserChargeSystem laserCharge = new LaserChargeSystem();


            EnemySpawnData spawnData = new EnemySpawnData();
            EnemySpawnControlSystem enemySpawnControl = new EnemySpawnControlSystem();
            EnemyMoveSystem enemyMove = new EnemyMoveSystem();
            TargetFollowSystem targetFollow = new TargetFollowSystem();
            EnemyLifeTimeControlSystem enemyLifeTimeControl = new EnemyLifeTimeControlSystem();
            ScreenTeleportSystem screenTeleportSystem = new ScreenTeleportSystem();
            DamageRegisterSystem damageRegister = new DamageRegisterSystem();


            NewSpawnSystem newSpawnSystem = new NewSpawnSystem();
            NewEnemySplitSystem newEnemySplitSystem = new NewEnemySplitSystem();
            PlayerHitSystem playerHitSystem = new PlayerHitSystem();
            spawnData.SpawnCenters.Add("Asteroid", systemConfiguration.AsteroidsSpawnPoint.transform.position);
            spawnData.SpawnCenters.Add("Ufo", systemConfiguration.UfosSpawnPoint.transform.position);
            spawnData.countToSpawn.Add("Asteroid", systemConfiguration.Root.Enemy.AsteroidCount);
            spawnData.countToSpawn.Add("Ufo", systemConfiguration.Root.Enemy.UfoCount);
            spawnData.prefabMap.Add("Asteroid", systemConfiguration.Root.Enemy.AsteroidPrefab);
            spawnData.prefabMap.Add("Ufo", systemConfiguration.Root.Enemy.UfoPrefab);
            ref EntityObject player = ref entityHandler.CreateEntity();
            ref EntityObject fireGunEntity = ref entityHandler.CreateEntity();
            ref EntityObject laserGunEntity = ref entityHandler.CreateEntity();
            ref EntityObject enemyAsteroidSpawnerEntity = ref entityHandler.CreateEntity();


            WeaponViewFactory viewFactory = new WeaponViewFactory(playerViewFactory.SpawnedPlayerObject);



            ObjectPoolFacade.CreatePoolingSlot(systemConfiguration.Root.Weapon.BulletPrefab, 30);
            BulletSpawnData bulletSpawnData = new BulletSpawnData(systemConfiguration.Root.Weapon.BulletPrefab);
            ref var fireGun = ref fireGunEntity.Add<FireGunComponent>();
            fireGun.bulletSpeed = systemConfiguration.Root.Weapon.BulletSpeed;
            fireGun.bulletLifetime = systemConfiguration.Root.Weapon.BulletLifetime;
            ref var laserGun = ref laserGunEntity.Add<LaserGunComponent>();
            ref var laserChargeComp = ref laserGunEntity.Add<LaserChargeComponent>();
            laserGun.handler = playerView;
            laserGun.maxDuration = systemConfiguration.Root.Weapon.MaxlaserDuration;
            laserGun.weaponView = viewFactory.CreateWeaponView(WeaponType.LASER_GUN);
            laserGun.weaponView.InitializeView();



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
            PlayerEntityData playerEntity = new PlayerEntityData(ref playerTransform, ref player);
            ref var asteroidSpawn = ref enemyAsteroidSpawnerEntity.Add<EnemySpawnerComponent>();
            asteroidSpawn.countToSpawn = systemConfiguration.Root.Enemy.AsteroidCount;
            ref EntityObject enemyUfoSpawnerEntity = ref entityHandler.CreateEntity();
            ref var ufoSpawn = ref enemyUfoSpawnerEntity.Add<EnemySpawnerComponent>();
            ufoSpawn.countToSpawn = systemConfiguration.Root.Enemy.UfoCount;
            ufoSpawn.hasTarget = true;
            ufoSpawn.enemyPooledType = "Ufo";
            ufoSpawn.normalScale = new Vector3(3.7064f, 3.7584f, 1f);
            SceneData sceneData = new SceneData(_mainCamera);
            asteroidSpawn.enemyPooledType = "Asteroid";
            asteroidSpawn.normalScale = new Vector3(1f, 1f, 1f);

            fireGun.handler = playerView;


            playerfireGunWeapon.weaponEntity = fireGunEntity;
            playerAccelRot.maxSpeed = 10f;
            playerAcceleration.acelerationMultiplier = 1f;
            playerAcceleration.maxSpeed = systemConfiguration.Root.Player.MaxSpeed;

            playerLaserGunWeapon.weaponEntity = laserGunEntity;
            playerView.SetEntity(ref player);


            _systemHandler.AddSystem(inputSystem)
                          .AddSystem(accelerationSystem)
                          .AddSystem(playerMovement)
                          .AddSystem(rotationSystem)
                          .AddSystem(accelerateRotation)
                          .AddSystem(gunShooting)
                          .AddSystem(bulletSpawnSystem)
                          .AddSystem(bulletControl)
                          .AddSystem(laserShot)
                          .AddSystem(laserCharge)
                          .AddSystem(enemySpawnControl)
                          .AddSystem(enemyMove)
                          .AddSystem(targetFollow)
                          .AddSystem(enemyLifeTimeControl)
                          .AddSystem(screenTeleportSystem)
                          .AddSystem(damageRegister)
                          .AddSystem(bulletLifeTime)
                          .AddSystem(newSpawnSystem)
                          .AddSystem(playerHitSystem)
                          .AddSystem(newEnemySplitSystem)
                          .Inject<PlayerInputData>(inputData)
                          .Inject<PlayerEntityData>(playerEntity)
                          .Inject<EnemySpawnData>(spawnData)
                          .Inject<SceneData>(sceneData)
                          .Inject<UIPresenter>(systemConfiguration.UI)
                          .Inject<BulletSpawnData>(bulletSpawnData);



            return _systemHandler;
        }

        public override SystemHandler GetLateUpdateSystems(SystemConfiguration systemConfiguration)
        {
            SystemHandler handler = new SystemHandler(systemConfiguration.EntityHandler);
            LaserChargeLateUpdate laserChargeLate = new LaserChargeLateUpdate();
            handler.AddSystem(laserChargeLate);
            return handler;
        }
    }
}
