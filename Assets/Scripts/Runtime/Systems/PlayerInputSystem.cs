using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Runtime.Systems
{
    public class PlayerInputSystem : IGameSystem
    {
        private EntityFilter<PlayerInputComponent> _filter;

        private PlayerInputData _playerInputData;
        private bool _move;
        private bool _rotation;
        private EntityHandler _entityHandler;
        private Vector2 _rotationDir;
        private bool _onGunShot;
        private bool _onLaserShot;

        public void Initialize()
        {
            _playerInputData.InputActions.Player.Enable();
            _playerInputData.InputActions.Player.Movement.performed += HandlePlayerInput;
            _playerInputData.InputActions.Player.Rotation.performed += HandleRotationInput;
            _playerInputData.InputActions.Player.Rotation.canceled += HandleRotationInput;
            _playerInputData.InputActions.Player.GunShot.performed += HandleGunShooting;
            _playerInputData.InputActions.Player.GunShot.canceled += HandleGunShooting;
            _playerInputData.InputActions.Player.LaserShot.performed += HandleLaserShot;
            _playerInputData.InputActions.Player.LaserShot.canceled += HandleLaserShot;
        }

        public void OnDestroy()
        {       
            _playerInputData.InputActions.Player.Movement.performed -= HandlePlayerInput;
            _playerInputData.InputActions.Player.Rotation.performed -= HandleRotationInput;
            _playerInputData.InputActions.Player.Rotation.canceled -= HandleRotationInput;
            _playerInputData.InputActions.Player.GunShot.performed -= HandleGunShooting;
            _playerInputData.InputActions.Player.GunShot.canceled -= HandleGunShooting;
            _playerInputData.InputActions.Player.LaserShot.performed -= HandleLaserShot;
            _playerInputData.InputActions.Player.LaserShot.canceled -= HandleLaserShot;
            _playerInputData.InputActions.Player.Disable();
        }


        public void Update()
        {
            foreach (int item in _filter)
            {
                ref var player = ref _filter.GetEntity(item);
                ref var input = ref _filter.Get(item);

                if (_move)
                {
                    input.inputMovement = 1f;

                    if (player.HasComponent<AccelerationComponent>())
                    {
                        ref var acel = ref player.Add<AccelerationComponent>();
                        acel.accelerate = true;
                    }
                }
                else
                {
                    input.inputMovement = 0f;
                    if (player.HasComponent<AccelerationComponent>())
                    {
                       
                        ref var acel = ref player.Add<AccelerationComponent>();
                        acel.accelerate = false;
                    }
                }
                       
                if (_rotation)
                {
                    
                    input.inputRotation = _rotationDir;
                    if (player.HasComponent<RotationAccelerationComponent>())
                    {
                        ref var acel = ref player.Add<RotationAccelerationComponent>();
                        acel.accelerate = true;
                        if (_rotationDir.x > 0)
                            acel.direction = 1;
                        else if (_rotationDir.x<0)
                            acel.direction = -1;
                    }
                }
                else
                {
                    input.inputRotation = _rotationDir;
                    if (player.HasComponent<RotationAccelerationComponent>())
                    {
                        ref var acel = ref player.Add<RotationAccelerationComponent>();
                        acel.accelerate = false;
                    }
                }
                      
                if (_onGunShot)
                {
                    if(player.HasComponent<PlayerFireGunWeaponComponent>())
                    {
                       
                        ref var gun = ref player.Add<PlayerFireGunWeaponComponent>();
                        ref var shoot = ref gun.weaponEntity.Add<ShootComponent>();
                        _onGunShot = false;
                    }
                }

                if (_onLaserShot)
                {
                    ref var gun = ref player.Add<PlayerLaserGunWeaponComponent>();
                    ref var shoot = ref gun.weaponEntity.Add<ShootComponent>();
                    if(gun.weaponEntity.HasComponent<LaserLateChargeComponent>())
                        gun.weaponEntity.Remove<LaserLateChargeComponent>();
                    //Debug.Log("LaserShot");
                }
                else
                {
                    ref var gun = ref player.Add<PlayerLaserGunWeaponComponent>();
                    if(gun.weaponEntity.HasComponent<ShootComponent>())
                        gun.weaponEntity.Remove<ShootComponent>();
                    gun.weaponEntity.Add<LaserLateChargeComponent>();
                  
                }
            }
        }

                        
                       

               

                      
                        
                

                



        private void HandlePlayerInput(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<float>();
            if (input == 1)
                _move = true;
            else _move = false;
        }
            

        private void HandleRotationInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            if (value.x != 0 || value.y != 0)
            {
                _rotation = true;
                _rotationDir = value;
            }
            else
            {
                _rotation = false;
                _rotationDir = Vector2.zero;
            }
        }

        private void HandleGunShooting(InputAction.CallbackContext context)
        {
            var input = context.ReadValueAsButton();
            _onGunShot = input;
        }

        private void HandleLaserShot(InputAction.CallbackContext context)
        {
            _onLaserShot = context.ReadValueAsButton();
        }
        
    }
}
