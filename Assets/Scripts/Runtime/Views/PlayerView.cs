using Assets.Interfaces;
using Assets.Scripts.Runtime.Components;
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
    public class PlayerView : MonoBehaviour, ITransformableView
    {
        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        private EntityObject _entityObject;

        public void SetEntity(ref EntityObject entity)
        {
            _entityObject = entity;
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

        public void OnViewDestroy()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {


            if (TriggerHash.Instance._enemyHash.ContainsKey(collision.gameObject.GetInstanceID()))
            {
                Debug.Log("Player Hit");
                var distance = collision.gameObject.transform.position - this.transform.position;
                if (Mathf.Abs(distance.magnitude) < 1.5f)
                    _entityObject.Add<PlayerHitComponent>();
            }

        }
    }
}
