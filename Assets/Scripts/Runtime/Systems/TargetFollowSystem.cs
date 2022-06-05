using Assets.Scripts.Runtime.Components;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
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
               
