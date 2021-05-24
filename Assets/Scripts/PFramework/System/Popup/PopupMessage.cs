using TMPro;
using UnityEngine;

namespace PFramework
{
    public class PopupMessage : PopupBehaviour
    {
        [Header("Reference")]
        [SerializeField] TextMeshProUGUI _txtContent;

        public void Construct(string msg)
        {
            _txtContent.text = msg;
        }
    }
}