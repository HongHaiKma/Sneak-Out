using PFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] TouchPanel _touchPanel;

        #region MonoBehaviour

        void Awake()
        {
            _touchPanel.OnBeginDrag += TouchPanel_OnBeginDrag;
            _touchPanel.OnDrag += TouchPanel_OnDrag;
            _touchPanel.OnEndDrag += TouchPanel_OnEndDrag;
        }

        #endregion

        void TouchPanel_OnBeginDrag(PointerEventData pointer)
        {
            Messenger.Broadcast(GameEvent.Control_DragBegin, pointer);
        }

        void TouchPanel_OnDrag(PointerEventData pointer)
        {
            Messenger.Broadcast(GameEvent.Control_Drag, pointer);
        }

        void TouchPanel_OnEndDrag(PointerEventData pointer)
        {
            Messenger.Broadcast(GameEvent.Control_DragEnd, pointer);
        }
    }
}