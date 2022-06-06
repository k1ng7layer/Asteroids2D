using MyECS2;
using UnityEngine;

namespace AsteroidsECS
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
                   
                    transform.Position +=  transform.Forward * Time.deltaTime* transform.acceleration;
                    entity.attachedView.SetPosition(ref transform.Position);
                    entity.attachedView.SetRotation(transform.Rotation);
                   ;
                }
                
            }
        }
    }
}
