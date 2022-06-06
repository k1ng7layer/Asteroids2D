using UnityEngine;

namespace AsteroidsECS
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/EnemyCFG")]
    public class EnemyConfiguration : ScriptableObject
    {
        [SerializeField] private EnemyView _asteroidPrefab;
        [SerializeField] private EnemyView _ufoPrefab;
        [SerializeField] private int _asteroidCount;
        [SerializeField] private int _ufoCount;
        public EnemyView AsteroidPrefab => _asteroidPrefab;
        public EnemyView UfoPrefab => _ufoPrefab;
        public int AsteroidCount => _asteroidCount;
        public int UfoCount => _ufoCount;
    }
}
