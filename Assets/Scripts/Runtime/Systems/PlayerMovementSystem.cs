using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using UnityEngine;
using MyECS2;
using Assets.Interfaces;
using Assets.Scripts.Runtime.UI;

namespace Assets.Scripts.Runtime.Systems
{
    public class PlayerMovementSystem : IGameSystem
    {
        private EntityFilter<PlayerInputComponent, TransformComponent> _filter;
        
        private PlayerInputData _playerInputData;
        
        private ITransformableView _playerView;
        private float Velocity;
        private Vector3 _prevPosition;
        private UIPresenter _uIPresenter;

        public PlayerMovementSystem(ITransformableView view)
        {
            _playerView = view;
        }

        public void Initialize()
        {
            
        }

        public void LateUpdate()
        {

        }

        public void Update()
        {
            foreach (int item in _filter)
            {
                ref var input = ref _filter.GetFirst(item);
                ref var playerTransform = ref _filter.GetSecond(item);
                playerTransform.Position = playerTransform.Position + (playerTransform.Rotation * Vector3.up * (playerTransform.acceleration+ input.inputMovement/2) * Time.deltaTime);
                _playerView.SetPosition(ref playerTransform.Position);
                var currentPos = playerTransform.Position - _prevPosition;
                Velocity = currentPos.magnitude / Time.fixedDeltaTime;
                _prevPosition = playerTransform.Position;
                _uIPresenter.PlayerVelocityInfo.SetValue(Velocity);
                _uIPresenter.PlayerCoordsInfo.SetValue(playerTransform.Position);
                Debug.Log("PlayerMovementSystem");
            }
            
        }

        public void OnDestroy()
        {
            
        }
    }
}
                
                
               
                


           
               
