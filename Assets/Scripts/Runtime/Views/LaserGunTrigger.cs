using Assets.Scripts.Runtime.Data;
using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Views
{
    public class LaserGunTrigger : BulletView
    {  
        public event Action<BulletView> OnTargetReached;
        private Collider2D _collider;
        private IDestroyableEntity _destroyableEntity; 
        private void Start()
        {
            OnCreated();
            TriggerHash.Instance._bulletHash.Add(this.gameObject.GetInstanceID(), this);
        }

        public void Enable()
        {
            if (_collider == null)
                _collider = this.GetComponent<BoxCollider2D>();
            _collider.enabled = true;

        }

        public void Disable()
        {
            if (_collider == null)
                _collider = this.GetComponent<BoxCollider2D>();
            _collider.enabled = false;

        }

        public override void SetEntity(ref IDestroyableEntity entity)
        {
            _destroyableEntity = entity;
        }
        
        public override ref IDestroyableEntity GetEntity()
        {
            
            return ref _destroyableEntity;
        }

        public override void OnViewDestroy()
        {
            
        }

        private void  OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerHash.Instance._enemyHash.ContainsKey(collision.gameObject.GetInstanceID()))
            {
                Debug.Log("enemy triggered");
                OnTargetReached?.Invoke(this);
            }

        }
        
    }
}



    
