using Assets.Scripts.Runtime.Components;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems
{
    public class ScreenTeleportSystem : IGameSystem
    {
        private SceneData _sceneData;
        private EntityFilter<ScreenPositionComponent, TransformComponent> _filter;
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
                ref var objScreenSettings = ref _filter.GetFirst(item);
                ref var transform = ref _filter.GetSecond(item);
               
                if (!CheckObjectOutOfBounds(ref transform.Position, objScreenSettings.objectWidth, objScreenSettings.objectHeight, out Vector2 direction))
                {
                    if (direction.x != 0)
                        transform.Position = new Vector3(transform.Position.x * -1, transform.Position.y);
                    if(direction.y!=0)
                        transform.Position = new Vector3(transform.Position.x, transform.Position.y*-1);
                }
            }
        }
                   
        private bool CheckObjectOutOfBounds(ref Vector3 position,float objectWidth, float objectHeight, out Vector2 teleportdirection)
        {
            var positionWoBounds = position;
            var posWithBoundsInNegative = new Vector3(positionWoBounds.x + objectWidth, positionWoBounds.y + objectHeight);
            var posWithBoundsInPositive = new Vector3(positionWoBounds.x - objectWidth, positionWoBounds.y - objectHeight);
            var objNegativePosition = _sceneData.MainCamera.WorldToViewportPoint(posWithBoundsInNegative);
            var objPositivePosition = _sceneData.MainCamera.WorldToViewportPoint(posWithBoundsInPositive);
          
            if (objPositivePosition.x > 1f)
            {
                teleportdirection = new Vector2(1f, 0f);
                return false;
            }
            if (objNegativePosition.x < 0f)
            {
                teleportdirection = new Vector2(1f, 0f);
                return false;
            }
            if (objPositivePosition.y > 1f)
            {
                teleportdirection = new Vector2(0f, 1f);
                return false;
            }
            if (objNegativePosition.y < 0f)
            {
                teleportdirection = new Vector2(0f, 1f);
                return false;
            }
            teleportdirection = new Vector2();
            return true;
        }
    }
}
            
