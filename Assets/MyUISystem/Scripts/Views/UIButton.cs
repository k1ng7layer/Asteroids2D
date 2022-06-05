using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyUISystem
{
    public abstract class UIButton:MonoBehaviour
    {
        [SerializeField] private Button _button;
        public void Initialize()
        {
            if(this.TryGetComponent<Button>(out Button button))
            {
                _button = button;
                _button.onClick.AddListener(OnClick);
            }
            else
            {
                throw new ArgumentNullException("GameObject should have Button component");
            }
        }
        protected abstract void OnClick();
    }
}
