using Assets.Interfaces;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using MyObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Views
{
    public class BulletView : PooledObject,ITransformableView
    {
        private IDestroyableEntity attachedEntity;

        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public override void OnCreated()
        {
            
        }
        public virtual void SetEntity(ref IDestroyableEntity entity)
        {
            attachedEntity = entity;
        }
        public virtual ref IDestroyableEntity GetEntity()
        {
            return ref attachedEntity;
        }

        public void SetPosition(ref Vector3 position)
        {
            Position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
            this.transform.rotation = Rotation;
        }

        public void SetScale(Vector3 scale)
        {
            
        }

        public virtual void OnViewDestroy()
        {
            TriggerHash.Instance._bulletHash.Remove(this.gameObject.GetInstanceID());
            ObjectPoolFacade.ReturnObjectToPool(this);
        }

        public void SetEntity(ref EntityObject entity)
        {
            attachedEntity = entity;
        }
    }
}
