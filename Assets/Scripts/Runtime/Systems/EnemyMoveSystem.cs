using MyECS2;
using Assets.Scripts.Runtime.Components;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class EnemyMoveSystem : IGameSystem
    {
        private EntityFilter<TransformComponent, EnemyComponent> _filter;

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
                
                ref var entity = ref _filter.GetSecond(item);
                if (entity.alive)
                {

                   
                    ref var transform = ref _filter.GetFirst(item);
                    Debug.Log($"EnemyMoveSystem at POSITION = {transform.Position}");
                    transform.Position +=  transform.Forward * Time.deltaTime* transform.acceleration;
                    entity.attachedView.SetPosition(ref transform.Position);
                    entity.attachedView.SetRotation(transform.Rotation);
                   ;
                }
                
            }
        }
    }
}
