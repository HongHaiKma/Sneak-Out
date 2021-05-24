using DG.Tweening;
using PFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class OverlayUI : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Image _imgSneak;
        [SerializeField] Image _imgFlash;
        [SerializeField] List<RectTransform> _rtBushes;

        [Header("Config")]
        [Range(0f, 1f)]
        [SerializeField] float _sneakAlpha;
        [SerializeField] float _sneakInDuration;
        [SerializeField] float _sneakOutDuration;

        [Space]

        [SerializeField] float _bushDuration;
        [SerializeField] Ease _bushEase;

        [Space]

        [SerializeField] float _flashAlpha;
        [SerializeField] float _flashInDuration;
        [SerializeField] Ease _flashInEase;
        [SerializeField] float _flashDuration;
        [SerializeField] float _flashOutDuration;
        [SerializeField] Ease _flashOutEase;

        Tween _tweenSneak;
        Sequence _sequenceBush;
        Sequence _sequenceFlash;

        #region MonoBehaviour

        void Start()
        {
            InitSequenceBush();
            InitSequenceFlash();

            Messenger.AddListener<bool>(GameEvent.Player_Sneak, GameEvent_PlayerSneak);
            Messenger.AddListener<bool>(GameEvent.Player_Bush, GameEvent_PlayerBush);
            Messenger.AddListener(GameEvent.Item_Ice, GameEvent_ItemIce);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<bool>(GameEvent.Player_Sneak, GameEvent_PlayerSneak);
            Messenger.RemoveListener<bool>(GameEvent.Player_Bush, GameEvent_PlayerBush);
            Messenger.RemoveListener(GameEvent.Item_Ice, GameEvent_ItemIce);

            _tweenSneak?.Kill();
            _sequenceBush?.Kill();
        }

        #endregion

        void InitSequenceBush()
        {
            _sequenceBush = DOTween.Sequence();

            for (int i = 0; i < _rtBushes.Count; i++)
            {
                _sequenceBush.Join(_rtBushes[i].DOAnchorPos(Vector2.zero, _bushDuration)
                    .SetEase(_bushEase));
            }

            _sequenceBush.SetAutoKill(false);
            _sequenceBush.Restart();
            _sequenceBush.Pause();
        }

        void InitSequenceFlash()
        {
            _sequenceFlash = DOTween.Sequence();

            _imgFlash.SetAlpha(0f);

            _sequenceFlash.Append(_imgFlash.DOFade(_flashAlpha, _flashInDuration)
                .SetEase(_flashInEase)
                .ChangeStartValue(_imgFlash.color));

            _sequenceFlash.AppendInterval(_flashDuration);

            _imgFlash.SetAlpha(_flashAlpha);

            _sequenceFlash.Append(_imgFlash.DOFade(0f, _flashOutDuration)
                .SetEase(_flashOutEase)
                .ChangeStartValue(_imgFlash.color));

            _sequenceFlash.SetAutoKill(false);
            _sequenceFlash.Restart();
            _sequenceFlash.Pause();
        }

        void GameEvent_ItemIce()
        {
            _sequenceFlash.Restart();
            _sequenceFlash.Play();
        }

        void GameEvent_PlayerBush(bool isHide)
        {
            if (isHide)
            {
                _sequenceBush.PlayForward();
            }
            else
            {
                _sequenceBush.PlayBackwards();
            }
        }

        void GameEvent_PlayerSneak(bool sneak)
        {
            if (sneak)
                PlaySneakIn();
            else
                PlaySneakOut();
        }

        void PlaySneakIn()
        {
            _tweenSneak?.Kill();
            _tweenSneak = _imgSneak.DOFade(_sneakAlpha, _sneakInDuration);
        }

        void PlaySneakOut()
        {
            _tweenSneak?.Kill();
            _tweenSneak = _imgSneak.DOFade(0f, _sneakOutDuration);
        }
    }
}