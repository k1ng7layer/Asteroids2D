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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Configuration
{
    public class SystemConfiguration
    {
        private SystemHandler _systemHandler;
        //public SystemHandler GetSystemHandler(EntityHandler entityHandler, RootConfiguration configuration)
        //{
        //    Time.timeScale = 1f;
        //    uI = new UIPresenter(configuration.UI);
        //    ObjectPoolFacade.CreatePoolingSlot<EnemyView>(configuration.Enemy.AsteroidPrefab, configuration.Enemy.AsteroidCount * 2);
        //    ObjectPoolFacade.CreatePoolingSlot<EnemyView>(configuration.Enemy.UfoPrefab, configuration.Enemy.UfoCount);
        //    //EntityHandler entityHandler = new EntityHandler();
        //    _systemHandler = new SystemHandler(entityHandler);
        //    PlayerInputSystem inputSystem = new PlayerInputSystem();
        //    ViewFactory playerViewFactory = new PlayerViewFactory();
        //    ITransformableView playerView = playerViewFactory.GetTransformableView();
        //    PlayerMovementSystem playerMovement = new PlayerMovementSystem(playerView);
        //    PlayerInputData inputData = new PlayerInputData(new Controlls());
        //    AccelerationSystem accelerationSystem = new AccelerationSystem();
        //    PlayerRotationSystem rotationSystem = new PlayerRotationSystem(playerView);
        //    AccelerateRotationSystem accelerateRotation = new AccelerateRotationSystem();
        //    GunShootingSystem gunShooting = new GunShootingSystem();
        //    GunBulletSpawnSystem bulletSpawnSystem = new GunBulletSpawnSystem();
        //    BulletControlSystem bulletControl = new BulletControlSystem();
        //    BulletLifeTimeSystem bulletLifeTime = new BulletLifeTimeSystem();
        //    LaserShotSystem laserShot = new LaserShotSystem();
        //    LaserChargeSystem laserCharge = new LaserChargeSystem();
        //    LaserChargeLateUpdate laserChargeLate = new LaserChargeLateUpdate();
        //    EnemySpawnSystem enemySpawn = new EnemySpawnSystem();
        //    EnemySpawnData spawnData = new EnemySpawnData();
        //    EnemySpawnControlSystem enemySpawnControl = new EnemySpawnControlSystem();
        //    EnemyMoveSystem enemyMove = new EnemyMoveSystem();
        //    TargetFollowSystem targetFollow = new TargetFollowSystem();
        //    EnemyLifeTimeControlSystem enemyLifeTimeControl = new EnemyLifeTimeControlSystem();
        //    ScreenTeleportSystem screenTeleportSystem = new ScreenTeleportSystem();
        //    DamageRegisterSystem damageRegister = new DamageRegisterSystem();
        //    SpawnSystem spawnSystem = new SpawnSystem();
        //    EnemySplitSystem splitSistem = new EnemySplitSystem();
        //    NewSpawnSystem newSpawnSystem = new NewSpawnSystem();
        //    NewEnemySplitSystem newEnemySplitSystem = new NewEnemySplitSystem();
        //    PlayerHitSystem playerHitSystem = new PlayerHitSystem();
        //    spawnData.SpawnCenters.Add("Asteroid", _asteroidsSpawnPoint.transform.position);
        //    spawnData.SpawnCenters.Add("Ufo", _asteroidsSpawnPoint.transform.position);
        //    spawnData.countToSpawn.Add("Asteroid", configuration.Enemy.AsteroidCount);
        //    spawnData.countToSpawn.Add("Ufo", configuration.Enemy.UfoCount);
        //    spawnData.prefabMap.Add("Asteroid", configuration.Enemy.AsteroidPrefab);
        //    spawnData.prefabMap.Add("Ufo", configuration.Enemy.UfoPrefab);
        //    ref EntityObject player = ref entityHandler.CreateEntity();
        //    ref EntityObject fireGunEntity = ref entityHandler.CreateEntity();
        //    ref EntityObject laserGunEntity = ref entityHandler.CreateEntity();
        //    ref EntityObject enemyAsteroidSpawnerEntity = ref entityHandler.CreateEntity();


        //    WeaponViewFactory viewFactory = new WeaponViewFactory(playerViewFactory.SpawnedPlayerObject);


        //    //GameObjectPoolFacade.CreatePoolingSlot("Bullet", _rootConfiguration.Weapon.BulletPrefab, 3);
        //    ObjectPoolFacade.CreatePoolingSlot(configuration.Weapon.BulletPrefab, 30);
        //    BulletSpawnData bulletSpawnData = new BulletSpawnData(configuration.Weapon.BulletPrefab);
        //    ref var fireGun = ref fireGunEntity.Add<FireGunComponent>();
        //    ref var laserGun = ref laserGunEntity.Add<LaserGunComponent>();
        //    ref var laserChargeComp = ref laserGunEntity.Add<LaserChargeComponent>();
        //    laserGun.handler = playerView;
        //    laserGun.maxDuration = 3f;
        //    laserGun.weaponView = viewFactory.CreateWeaponView(WeaponType.LASER_GUN);
        //    laserGun.weaponView.InitializeView();



        //    ref var playerInput = ref player.Add<PlayerInputComponent>();
        //    ref var playerTransform = ref player.Add<TransformComponent>();
        //    ref var playerRotation = ref player.Add<RotationComponent>();
        //    ref var playerAcceleration = ref player.Add<AccelerationComponent>();
        //    ref var playerAccelRot = ref player.Add<RotationAccelerationComponent>();
        //    ref var playerfireGunWeapon = ref player.Add<PlayerFireGunWeaponComponent>();
        //    ref var playerLaserGunWeapon = ref player.Add<PlayerLaserGunWeaponComponent>();
        //    ref var playerScreenPos = ref player.Add<ScreenPositionComponent>();
        //    var playerWidth = playerViewFactory.SpawnedPlayerObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //    var playerHeight = playerViewFactory.SpawnedPlayerObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //    playerScreenPos.objectHeight = playerHeight;
        //    playerScreenPos.objectWidth = playerWidth;
        //    PlayerEntityData playerEntity = new PlayerEntityData(ref playerTransform, ref player);
        //    ref var asteroidSpawn = ref enemyAsteroidSpawnerEntity.Add<EnemySpawnerComponent>();
        //    asteroidSpawn.countToSpawn = configuration.Enemy.AsteroidCount;
        //    ref EntityObject enemyUfoSpawnerEntity = ref entityHandler.CreateEntity();
        //    ref var ufoSpawn = ref enemyUfoSpawnerEntity.Add<EnemySpawnerComponent>();
        //    ufoSpawn.countToSpawn = configuration.Enemy.UfoCount;
        //    ufoSpawn.hasTarget = true;
        //    ufoSpawn.enemyPooledType = "Ufo";
        //    ufoSpawn.normalScale = new Vector3(3.7064f, 3.7584f, 1f);
        //    SceneData sceneData = new SceneData(_mainCamera);
        //    asteroidSpawn.enemyPooledType = "Asteroid";
        //    asteroidSpawn.normalScale = new Vector3(1f, 1f, 1f);

        //    fireGun.handler = playerView;
        //    //fireGun._bulletPrefab = _rootConfiguration.Weapon.BulletPrefab;

        //    playerfireGunWeapon.weaponEntity = fireGunEntity;
        //    playerAccelRot.maxSpeed = 10f;
        //    playerAcceleration.acelerationMultiplier = 1f;
        //    playerAcceleration.maxSpeed = 3f;

        //    playerLaserGunWeapon.weaponEntity = laserGunEntity;
        //    playerView.SetEntity(ref player);


        //    _systemHandler.AddSystem(inputSystem)
        //                  .AddSystem(accelerationSystem)
        //                  .AddSystem(playerMovement)
        //                  .AddSystem(rotationSystem)
        //                  .AddSystem(accelerateRotation)
        //                  .AddSystem(gunShooting)
        //                  .AddSystem(bulletSpawnSystem)
        //                  .AddSystem(bulletControl)
        //                  .AddSystem(laserShot)
        //                  .AddSystem(laserCharge)
        //                  //.AddSystem(enemySpawn)
        //                  .AddSystem(enemySpawnControl)
        //                  .AddSystem(enemyMove)
        //                  .AddSystem(targetFollow)
        //                  .AddSystem(enemyLifeTimeControl)
        //                  .AddSystem(screenTeleportSystem)
        //                  .AddSystem(damageRegister)
        //                  //.AddSystem(spawnSystem)
        //                  .AddSystem(newSpawnSystem)
        //                  .AddSystem(playerHitSystem)
        //                  //.AddSystem(splitSistem)
        //                  .AddSystem(newEnemySplitSystem)
        //                  .Inject<PlayerInputData>(inputData)
        //                  .Inject<PlayerEntityData>(playerEntity)
        //                  .Inject<EnemySpawnData>(spawnData)
        //                  .Inject<SceneData>(sceneData)
        //                  .Inject<UIPresenter>(uI)
        //                  .Inject<BulletSpawnData>(bulletSpawnData);


        //    _lateUpdateSystems = new SystemHandler(entityHandler);

        //    _lateUpdateSystems.AddSystem(laserChargeLate);
        //    return _systemHandler;
        //}
       
    }
}
