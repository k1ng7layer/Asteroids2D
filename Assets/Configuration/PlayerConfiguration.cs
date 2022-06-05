using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Configuration
{
    [CreateAssetMenu(fileName = "new config", menuName = "Configuration/PlayerCFG")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] GameObject _playerPrefab;
    }
}
