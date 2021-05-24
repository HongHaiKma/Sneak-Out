using DG.Tweening;
using PFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ControlTutorialUI : CacheMonoBehaviour
    {
        static readonly float FadeOutDuration = 0.4f;

        [Header("Reference")]
        [SerializeField] RectTransform _rtHand;

        [Header("Config")]
        [SerializeField] Vector2 _range;
        [SerializeField] float _xDuration;
        [SerializeField] Ease _xEase;
        [SerializeField] float _yDuration;
        [SerializeField] Ease _yEase;

        Tween _tweenX;
        Tween _tweenY;
        Tween _tweenCanvas;

        Vector2 _position;

        #region MonoBehaviour

        void Start()
        {
            InitTween();

            Messenger.AddListener<PointerEventData>(GameEvent.Control_DragBegin, GameEvent_ControlDragBegin);
        }

        void OnDestroy()
        {
            _tweenX?.Kill();
            _tweenY?.Kill();
            _tweenCanvas?.Kill();

            Messenger.RemoveListener<PointerEventData>(GameEvent.Control_DragBegin, GameEvent_ControlDragBegin);
        }

        void Update()
        {
            _rtHand.anchoredPosition = _position;
        }

        #endregion

        void GameEvent_ControlDragBegin(PointerEventData pointer)
        {
            if (_tweenCanvas.IsActive())
                return;

            _tweenCanvas = GetComponent<CanvasGroup>().DOFade(0f, FadeOutDuration)
                .OnComplete(() => { Destroy(CacheGameObject); });
        }

        void InitTween()
        {
            _tweenX = DOVirtual.Float(-_range.x, _range.x, _xDuration, UpdateX)
                .SetEase(_xEase)
                .SetLoops(-1, LoopType.Yoyo);

            _tweenY = DOVirtual.Float(-_range.y, _range.y, _yDuration, UpdateY)
                .SetEase(_yEase)
                .SetLoops(-1, LoopType.Yoyo);

            _tweenY.Goto(0.5f, true);
        }

        void UpdateX(float x)
        {
            _position.x = x;
        }

        void UpdateY(float y)
        {
            _position.y = y;
        }
    }
}