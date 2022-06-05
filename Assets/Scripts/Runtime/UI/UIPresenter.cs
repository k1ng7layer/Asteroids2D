using Assets.Scripts.Actions;
using Assets.Scripts.Runtime.Data;
using MyExtensions;
using MyUISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.UI
{
    public class UIPresenter
    {
        public IIndicator<float> PlayerVelocityInfo { get; private set; }
        public IIndicator<Vector2> PlayerCoordsInfo { get; private set; }
        public IIndicator<float> PlayerRotationInfo { get; private set; }
        public IIndicator<float> PlayerLaserChargeInfo { get; private set; }
        public IIndicator<float> PlayerLaserCoolDownInfo { get; private set; }
        private IIndicator<int> _playerScoreInfo;
        private UIController _uIController;
        public UIPresenter(UIConfiguration uIConfiguration)
        {
            _indicatorsCanvas = uIConfiguration.IndicatorsCanvas;
            _uIController = new UIController(uIConfiguration);
           //Setup();
        }
        //private UIPresenter() { }

        //private static UIPresenter _instance;
        //public static UIPresenter Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new UIPresenter();
        //        }
        //        return _instance;
        //    }
        //}

        private Canvas _indicatorsCanvas;
        public Slider _laserCharge;
        private Controlls _inputs;

        public void SetConfig(UIConfiguration uIConfiguration)
        {
            _indicatorsCanvas = uIConfiguration.IndicatorsCanvas;
            _uIController = new UIController(uIConfiguration);
            Setup();
        }

        private void Setup()
        {
            _uIController.Initialize();
        }

        public void OpenUIWindow(string id)
        {
            _uIController.OpenWindow(id);
        }

        public T FindIndicatorCanvasObject<T>() where T : MonoBehaviour
        {
            return _uIController.IndicatorsCanvas.GetOrCreateComponentInChildren<T>();
        }

        public void Initialize()
        {
            Setup();
            ActionContainer.ResolveAction<GameOverAction>().AddListener(SetGameOver);
            if (_uIController.IndicatorsCanvas != null)
            {
                GameInfoView gameInfoView = FindIndicatorCanvasObject<GameInfoView>();
                PlayerVelocityInfo = new GameIndicator<float>(gameInfoView.Velocity);
                PlayerCoordsInfo = new GameIndicator<Vector2>(gameInfoView.Сoordinates);
                PlayerRotationInfo = new GameIndicator<float>(gameInfoView.Angle);
                PlayerLaserChargeInfo = new GameIndicator<float>(gameInfoView.LaserCharge);
                PlayerLaserCoolDownInfo = new GameIndicator<float>(gameInfoView.LaserCooldown);
                _playerScoreInfo = new GameIndicator<int>(gameInfoView.PlayerScore);
            }
        }

        public void OnDestroy()
        {
            ActionContainer.ResolveAction<GameOverAction>().RemoveListener(SetGameOver);
        }

        public void OnLateUpdate()
        {

        }

        public void OnUpdate()
        {
            if (_playerScoreInfo != null)
                _playerScoreInfo.SetInintialValue(PlayerSceneData.Instance.PlayerScore);
        }
        private void SetGameOver()
        {
            //SetPause();
            OpenUIWindow("gameOverMenu");
        }
    }
}
