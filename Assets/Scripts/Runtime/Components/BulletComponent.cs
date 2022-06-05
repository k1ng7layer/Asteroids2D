using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct BulletComponent
    {
        public GameObject attachedBullet;
        public BulletView attachedView;
        public Vector3 direction;
        public float speed;
        public float lifeTime;
    }
}
