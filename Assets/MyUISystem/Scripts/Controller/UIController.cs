using System;
using UnityEngine;

namespace MyUISystem
{
    public class UIController
    {
        private UIConfiguration _uIConfiguration;
        
        private Canvas _mainCanvas;
        private Canvas _indicatorsCanvas;
        public Canvas IndicatorsCanvas => _indicatorsCanvas;
        private MainLayerController _mainLayerController;
        private UIWindow _activeWindow;
        public UIController(UIConfiguration configuration)
        {
            _uIConfiguration = configuration;
        }

        public void Initialize()
        {
            GenerateObjects();
            if (_mainLayerController == null)
                throw new ArgumentNullException();

            _mainLayerController.Initialize();
            _mainLayerController.OnActiveWindowChanged += SetActiveWindow;

            var startWindow = _mainLayerController.FindWindow(w => w.IsStartWindow == true);
            if (startWindow != null)
                SetActiveWindow(startWindow);
        }

        public void OnDestroy()
        {
            _mainLayerController.OnActiveWindowChanged -= SetActiveWindow;
        }

        private void GenerateObjects()
        {
            if (_uIConfiguration.MainCanvasPrefab != null)
            {
                _mainCanvas = GameObject.Instantiate(_uIConfiguration.MainCanvasPrefab);
            }
            else throw new ArgumentNullException();
           

            if(_uIConfiguration.IndicatorsCanvas)
            _indicatorsCanvas = GameObject.Instantiate(_uIConfiguration.IndicatorsCanvas);
            //else throw new ArgumentNullException();

            GameObject mainLayer = new GameObject("MainLayerPresenter");
            GameObject windowsLayer = new GameObject("WindowsLayer");
            //GameObject panelLayer = new GameObject("PanelsLayer");

            //Установка родителей
            mainLayer.transform.SetParent(_mainCanvas.transform);
            windowsLayer.transform.SetParent(mainLayer.transform);
            //panelLayer.transform.SetParent(mainLayer.transform);
            //Добавление необходимых компонентов
            _mainLayerController = mainLayer.AddComponent<MainLayerController>();
            var mainLayerRT = mainLayer.AddComponent<RectTransform>();
            var windowsLayerRT = windowsLayer.AddComponent<RectTransform>();
            //var panelLayerRT = panelLayer.AddComponent<RectTransform>();
            windowsLayer.AddComponent<WindowsViewController>();
            //panelLayer.AddComponent<PanelLayerPresenter>();
            //Настройка MainLayer
            mainLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            mainLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            mainLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            mainLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            mainLayerRT.anchorMax = new Vector2(1f, 1f);
            mainLayerRT.anchorMin = Vector2.zero;
            mainLayerRT.localScale = Vector3.one;
            mainLayerRT.anchoredPosition = Vector2.zero;

            //Настройка WindowsLayer
            windowsLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            windowsLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            windowsLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            windowsLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);

            windowsLayerRT.anchorMax = new Vector2(1f, 1f);
            windowsLayerRT.anchorMin = Vector2.zero;
            windowsLayerRT.anchoredPosition = Vector2.zero;
            windowsLayerRT.localScale = Vector3.one;

            //Настройка PanelLayer
            //panelLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            //panelLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            //panelLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            //panelLayerRT.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);

            //panelLayerRT.anchorMax = new Vector2(1f, 1f);
            //panelLayerRT.anchorMin = Vector2.zero;
            //panelLayerRT.anchoredPosition = Vector2.zero;
            //panelLayerRT.localScale = Vector3.one;    
            foreach (var window in _uIConfiguration.UIWindows)
            {
                GameObject.Instantiate(window, windowsLayer.transform);
            }
        }

        public void OpenWindow(string windowID)
        {
            var window = _mainLayerController.GetWindow(windowID);
            if (window == null)
                throw new ArgumentNullException("no window founded");

            if (_activeWindow != null)
                _activeWindow.Close();
            _activeWindow = window;
            _activeWindow.Open();
        }

        private void SetActiveWindow(UIWindow uIWindow)
        {
            OpenWindow(uIWindow.Id);
        }

        public void CloseActiveWindow()
        {
            if (_activeWindow != null)
                _activeWindow.Close();
        }

    }
}


