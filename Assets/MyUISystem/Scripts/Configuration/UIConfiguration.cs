using System.Collections.Generic;
using UnityEngine;

namespace MyUISystem
{
    [CreateAssetMenu(fileName = "new ui config", menuName = "Configuration/UIConfiguration")]
    public class UIConfiguration:ScriptableObject
    {
        [SerializeField] private Canvas _mainCanvasPrefab;
        [SerializeField] private Canvas _indicatorsCanvas;
        [SerializeField] private List<UIWindow> _uIWindows = new List<UIWindow>();
        public List<UIWindow> UIWindows => _uIWindows;
        public Canvas MainCanvasPrefab => _mainCanvasPrefab;
        public Canvas IndicatorsCanvas => _indicatorsCanvas;
    }
}
