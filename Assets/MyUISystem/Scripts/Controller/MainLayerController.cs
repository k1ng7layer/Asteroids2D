using System;
using UnityEngine;

namespace MyUISystem
{
    public sealed class MainLayerController:MonoBehaviour
    {
        private WindowsViewController _windowController;
        internal event Action<UIWindow> OnActiveWindowChanged;
        public void Initialize()
        {
            _windowController = GetComponentInChildren<WindowsViewController>();
            if (_windowController == null)
                throw new ArgumentNullException();

            _windowController.Initialize();
            _windowController.OnActiveWindowChanged += NotifyActiveWindowChange;
        }
        private void OnDestroy()
        {
            _windowController.OnActiveWindowChanged -= NotifyActiveWindowChange;
        }
        internal UIWindow GetWindow(string windowiD)
        {
            var window = _windowController.GetWindow(windowiD);
            return window;
        }
        internal UIWindow FindWindow(Func<UIWindow, bool> func)
        {
            var window = _windowController.FindWindow(func);
            return window;
        }
        private void NotifyActiveWindowChange(UIWindow uIWindow)
        {
            OnActiveWindowChanged?.Invoke(uIWindow);
        }

    }
}
