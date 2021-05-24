using UnityEngine.UI;

namespace PFramework
{
    public class ButtonBase : CacheMonoBehaviour
    {
        protected Button _button;

        public Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();
                return _button;
            }
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(Button_OnClicked);
        }

        protected virtual void Button_OnClicked()
        {
        }
    }
}