using MyUISystem;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsECS
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
