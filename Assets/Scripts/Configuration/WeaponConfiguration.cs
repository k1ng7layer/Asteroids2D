using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/WeaponCFG")]
    public class WeaponConfiguration : ScriptableObject
    {
        [SerializeField] BulletView _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletLifetime;
        [SerializeField] private float _maxLaserDuration;
        public BulletView BulletPrefab => _bulletPrefab;
        public float BulletSpeed => _bulletSpeed;
        public float BulletLifetime => _bulletLifetime;
        public float MaxlaserDuration => _maxLaserDuration;
    }
}
