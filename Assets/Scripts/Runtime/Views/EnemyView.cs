using MyECS2;
using MyObjectPool;
using System;
using UnityEngine;

namespace AsteroidsECS
{
    public class EnemyView : PooledObject, ITransformableView
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get => this.transform.localScale; set => transform.localScale = value; }
        public EntityObject _entityObject;
     
        public event Action OnBeingHitted;
        public override void OnCreated()
        {
            TriggerHash.Instance._enemyHash.TryAdd(this.gameObject.GetInstanceID(), this);
        }

        public void SetPosition(ref Vector3 position)
        {
            Position = position;
            transform.position = Position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
            transform.rotation = Rotation;
        }

        public void SetScale(Vector3 scale)
        {
            this.transform.localScale = scale;
        }

       

        public void SetEntity(ref EntityObject entity)
        {
            _entityObject = entity;
        }

        public void OnViewDestroy()
        {
           
        }
    }
}
