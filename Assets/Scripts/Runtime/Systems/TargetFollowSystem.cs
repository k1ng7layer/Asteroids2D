using MyECS2;
using UnityEngine;

namespace AsteroidsECS
{
    public class TargetFollowSystem : IGameSystem
    {
        private EntityFilter<TargetFollowComponent,TransformComponent> _filter;
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
                ref var target = ref _filter.GetFirst(item);
                ref var transform = ref _filter.GetSecond(item);
                ref var entityPlayer = ref target.targetEntity;
                ref var targetPos = ref target.targetTransform.Position;
                ref var playerTransform = ref target.targetEntity.Add<TransformComponent>();
                Vector3 realitivePos = playerTransform.Position - transform.Position;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, realitivePos);
                transform.Rotation = rotation;
            }
        }
    }
}
               
