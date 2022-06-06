using UnityEngine;

namespace AsteroidsECS
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/PlayerCFG")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] GameObject _playerPrefab;
        [SerializeField] private float _maxSpeed;
        public float MaxSpeed => _maxSpeed;
    }
}
