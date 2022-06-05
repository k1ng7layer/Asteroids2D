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
    public class LaserIndicator: MonoBehaviour, IIndicator<float>
    {
        [SerializeField] private Slider _slider;

        public void SetInintialValue(float Value)
        {
            _slider.maxValue = Value;
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }
    }
}
