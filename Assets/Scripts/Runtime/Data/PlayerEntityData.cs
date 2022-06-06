using MyECS2;
using UnityEngine;

namespace AsteroidsECS
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
