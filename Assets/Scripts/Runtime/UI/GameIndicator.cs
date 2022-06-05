using MyUISystem;
using TMPro;

namespace Assets.Scripts.Runtime.UI
{
    public class GameIndicator<T>: IIndicator<T>
    {
        private TextMeshProUGUI _textField;
        public GameIndicator(TextMeshProUGUI textMesh)
        {
            _textField = textMesh;
        }
        public void SetInintialValue(T Value)
        {
            _textField.text = Value.ToString();
        }

        public void SetValue(T value)
        {
            _textField.text = value.ToString();
        }
    }
}
