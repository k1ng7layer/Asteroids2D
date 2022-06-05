using Assets.Scripts.Runtime.Components;
using MyECS2;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class AccelerateRotationSystem : IGameSystem
    {
        private EntityFilter<RotationAccelerationComponent, TransformComponent> _filter;
        public void Destroy()
        {
            
        }

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
                ref var rotation = ref _filter.GetFirst(item);
                ref var transform = ref _filter.GetSecond(item);

                if (rotation.accelerate)
                    Accelerate(ref transform.rotation, ref rotation.direction, ref rotation.maxSpeed);
                else Decelerate(ref transform.rotation);
            }
        }
        private void Accelerate(ref float anglePerRot, ref int direction, ref float maxAngle)
        {
            
            anglePerRot += Time.deltaTime * 2.8f * direction;
            anglePerRot = Mathf.Clamp(anglePerRot, -maxAngle, maxAngle);
           
        }
            
        private void Decelerate(ref float anglePerRot)
        {
            var direction = Mathf.Sign(anglePerRot);
            anglePerRot = Mathf.Abs(anglePerRot);
            anglePerRot -= Time.deltaTime * 2f;
            anglePerRot = Mathf.Max(anglePerRot, 0f) * direction;
        }
    }
}
