using TMPro;
using UnityEngine;

namespace AsteroidsECS
{
    public class GameInfoView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinates;
        [SerializeField] private TextMeshProUGUI _angle;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _laserCharge;
        [SerializeField] private TextMeshProUGUI _laserCooldown;
        [SerializeField] private TextMeshProUGUI _playerScoreInfo;
        public TextMeshProUGUI Сoordinates => _coordinates;
        public TextMeshProUGUI Angle => _angle;
        public TextMeshProUGUI Velocity => _velocity;
        public TextMeshProUGUI LaserCharge => _laserCharge;
        public TextMeshProUGUI LaserCooldown => _laserCooldown;
        public TextMeshProUGUI PlayerScore => _playerScoreInfo;
    }
}
