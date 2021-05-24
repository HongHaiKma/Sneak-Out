using TMPro;
using UnityEngine;

namespace PFramework
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBase : MonoBehaviour
    {
        protected TextMeshProUGUI _text;
        protected virtual string _strText => string.Empty;

        protected virtual void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = _strText;
        }
    }
}
