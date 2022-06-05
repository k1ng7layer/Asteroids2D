using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Configuration
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/WeaponCFG")]
    public class WeaponConfiguration : ScriptableObject
    {
        [SerializeField] BulletView _bulletPrefab;
        public BulletView BulletPrefab => _bulletPrefab;
    }
}
