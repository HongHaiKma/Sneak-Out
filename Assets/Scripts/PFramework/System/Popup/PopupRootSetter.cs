using UnityEngine;

namespace PFramework
{
    public class PopupRootSetter : MonoBehaviour
    {
        void Awake()
        {
            PopupHelper.PopupRoot = transform;
        }
    }
}