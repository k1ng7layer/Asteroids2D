using MyUISystem;
using UnityEngine;

namespace AsteroidsECS
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/RootCFG")]
    public class RootConfiguration : ScriptableObject
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private EnemyConfiguration _enemyConfiguration;
        [SerializeField] private WeaponConfiguration _weaponConfiguration;
        [SerializeField] private UIConfiguration _uIConfiguration;
        [SerializeField] private ECSConfiguration _eCSConfiguration;
        public PlayerConfiguration Player => _playerConfiguration;
        public EnemyConfiguration Enemy => _enemyConfiguration;
        public WeaponConfiguration Weapon => _weaponConfiguration;
        public UIConfiguration UI => _uIConfiguration;
        public ECSConfiguration ECS => _eCSConfiguration;
    }
}
