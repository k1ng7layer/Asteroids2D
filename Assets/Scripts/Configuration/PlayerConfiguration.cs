using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/PlayerCFG")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] GameObject _playerPrefab;
        [SerializeField] private float _maxSpeed;
        public float MaxSpeed => _maxSpeed;
    }
}
