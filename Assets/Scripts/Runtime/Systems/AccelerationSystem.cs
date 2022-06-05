using Assets.Scripts.Runtime.Components;
using MyECS2;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class AccelerationSystem : IGameSystem
    {
        private EntityFilter<AccelerationComponent,TransformComponent> _filter;
  

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
                ref var accelerationComp = ref _filter.GetFirst(item);
                ref var transform = ref _filter.GetSecond(item);

                
                if (accelerationComp.accelerate)
                    Accelerate(ref transform.acceleration, ref accelerationComp.acelerationMultiplier, ref accelerationComp.maxSpeed);
                else Decelerate(ref transform.acceleration, ref accelerationComp.acelerationMultiplier);
              

            }
        }

        private void Accelerate(ref float speed, ref float multiplier, ref float maxSpeed)
        {
            speed += multiplier/1.5f * Time.deltaTime;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        }

        private void Decelerate(ref float speed, ref float multiplier)
        {
            var directionMultiplier = Mathf.Sign(speed);
            speed = Mathf.Abs(speed);
            speed -= multiplier/2.1f * Time.deltaTime;
            speed = Mathf.Max(speed, 0f) * directionMultiplier;
        }

    }
}
