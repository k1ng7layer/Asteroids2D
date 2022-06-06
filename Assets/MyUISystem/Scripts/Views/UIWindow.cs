using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyUISystem
{
    public abstract class UIWindow:MonoBehaviour
    {
        [SerializeField] private bool _isStartWindow;
        public bool IsStartWindow => _isStartWindow;
        protected List<INavigator> navigationButtons = new List<INavigator>();
        protected List<UIButton> uIButtons = new List<UIButton>();
        internal event Action<string> OnNavigate;
        [SerializeField] private string _id;
        public string Id => _id;
        public void Initialize()
        {
            var navButtons = this.GetComponentsInChildren<INavigator>();
            var buttons = this.GetComponentsInChildren<UIButton>();
            if (navButtons != null && navButtons.Length != 0)
                navigationButtons = navButtons.ToList();
            if (buttons != null && buttons.Length != 0)
                uIButtons = buttons.ToList();
            foreach (var button in navigationButtons)  
            {
                OnNavigate += NavigateToWindow;
            }
            foreach (var button in uIButtons)   
            {
                button.Initialize();
            }
        }
        public void OnDestroy()
        {
            if (navigationButtons != null)
            {
                foreach (var button in navigationButtons)  
                {
                    button.OnNavigate -= NavigateToWindow;
                }
            }
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
        private void NavigateToWindow(string id)
        {
            OnNavigate?.Invoke(id);
        }
    }
}

