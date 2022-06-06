using UnityEngine;

namespace AsteroidsECS
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
