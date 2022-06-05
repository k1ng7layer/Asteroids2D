using Assets.Interfaces;
using Assets.Scripts.Runtime.Components;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data
{
    public class PlayerEntityData
    {
        public PlayerEntityData(ref TransformComponent transform, ref EntityObject entity)
        {
            PlayerPosition = transform.Position;
            this.transform = transform;
            playerEntity = entity;
        }
        public PlayerEntityData()
        {

        }
        public ITransformableView playerView;
        public TransformComponent transform;
        public Vector3 PlayerPosition;
        public EntityObject playerEntity;
    }

}
