using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class TouchPanel : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Callback<PointerEventData> OnPointerClick;
        public event Callback<PointerEventData> OnPointerDown;
        public event Callback<PointerEventData> OnPointerUp;

        public event Callback<PointerEventData> OnBeginDrag;
        public event Callback<PointerEventData> OnDrag;
        public event Callback<PointerEventData> OnEndDrag;

        bool _isDragging = false;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            OnBeginDrag?.Invoke(eventData);

            PanelInGame aaa = FindObjectOfType<PanelInGame>().GetComponent<PanelInGame>();
            aaa.btn_Outfit.gameObject.SetActive(false);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            OnEndDrag?.Invoke(eventData);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_isDragging)
                return;

            OnPointerClick?.Invoke(eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnPointerDown?.Invoke(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnPointerUp?.Invoke(eventData);
        }
    }
}