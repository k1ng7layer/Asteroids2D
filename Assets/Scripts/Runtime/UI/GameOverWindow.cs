using Assets.Scripts.Runtime.Data;
using MyUISystem;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.UI
{
    public class GameOverWindow : UIWindow
    {
        [SerializeField] TextMeshProUGUI _playerScore;
        public override void Close()
        {
            base.Close();
        }

        public override void Open()
        {
            base.Open();
            _playerScore.text = PlayerSceneData.Instance.PlayerScore.ToString();
        }
    }
}
