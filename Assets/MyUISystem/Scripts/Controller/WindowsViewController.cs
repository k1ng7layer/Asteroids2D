using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyUISystem
{
    public sealed class WindowsViewController:MonoBehaviour
    {
        private Dictionary<string,UIWindow> _uIWindows = new Dictionary<string,UIWindow>();
        internal UIWindow ActiveWindow { get; private set; }
        internal event Action<UIWindow> OnActiveWindowChanged;
        public void Initialize()
        {
            var children = GetComponentsInChildren<UIWindow>();
            if (children != null)
            {
                foreach (var child in children)
                {
                    if (child.Id == null)
                        throw new InvalidOperationException("Invalid window ID");
                    _uIWindows.Add(child.Id,child);
                    child.Initialize();
                    child.OnNavigate += OpenWindowFromButton;
                    child.Close();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var window in _uIWindows.Values)
            {   
                window.OnNavigate -= OpenWindowFromButton;
            }
        }

        internal UIWindow GetWindow(string windowId)
        {
            if(_uIWindows.TryGetValue(windowId,out UIWindow uIWindow))
            {
                return uIWindow; 
            }
            return default;
        }

        internal UIWindow FindWindow(Func<UIWindow, bool> func)
        {
            var window = _uIWindows.Values.Where(func).FirstOrDefault();
            return window;
        }
      

        private void OpenWindowFromButton(string windowId)
        {
            if (_uIWindows.TryGetValue(windowId, out UIWindow uIWindow))
            {
                OnActiveWindowChanged?.Invoke(ActiveWindow);
            }
        }
           
    }
}
              
