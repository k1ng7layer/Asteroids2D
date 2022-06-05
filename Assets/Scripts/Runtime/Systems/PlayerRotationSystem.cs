using Assets.Interfaces;
using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.UI;
using MyECS2;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class PlayerRotationSystem : IGameSystem
    {
        public PlayerRotationSystem(ITransformableView view)
        {
            _view = view;
        }
        private ITransformableView _view;
        private EntityFilter<PlayerInputComponent, RotationComponent,TransformComponent> _filter;
        private UIPresenter _uiPresenter;

        public void Initialize()
        {
            
        }

  
        public void Update()
        {
            foreach (int item in _filter)   
            {
                ref var input = ref _filter.GetFirst(item);
                ref var rotation = ref _filter.GetSecond(item);
                ref var transform = ref _filter.GetThird(item);
                rotation.RotationZ = Mathf.Repeat(rotation.RotationZ + -(transform.rotation +input.inputRotation.x)* Time.deltaTime * 100f, 360f);
                transform.Rotation = rotation.Rotation;
                _view.SetRotation(rotation.Rotation);
                _uiPresenter.PlayerRotationInfo.SetValue(transform.RotationZ);
            }
        }

        public void OnDestroy()
        {
            
        }
    }
}
