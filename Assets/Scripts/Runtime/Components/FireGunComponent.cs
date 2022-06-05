using Assets.Interfaces;
using Assets.Scripts.Runtime.Views;
using MyECS2;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct FireGunComponent
    {
        public ITransformableView handler;
        public GameObject _bulletPrefab;
        public float bulletSpeed;
        public float bulletLifetime;
        public BulletView _bulletView;
        public Vector3 shootingPoint;
        public Vector3 shootingDirection;
    }
}
       
