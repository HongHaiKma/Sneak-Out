using DG.Tweening;
using PFramework;

namespace Game
{
    public class AdsShowHandler : HardSingleton<AdsShowHandler>
    {
        Tween _tween;

        #region MonoBehaviour

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _tween?.Kill();
        }

        #endregion

        public void Show(Callback onClosed)
        {
            if (DataMain.LevelMax < GameConfig.AdsInterstitialMinLevel)
            {
                onClosed?.Invoke();
                return;
            }

            if (_tween != null || _tween.IsActive())
            {
                onClosed?.Invoke();
                return;
            }

            if (!AdsManager.IsInterstitialReady())
            {
                onClosed?.Invoke();
                return;
            }

            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(GameConfig.AdsInterstitialDelay, () =>
            {
                _tween?.Kill();
                _tween = null;
            });

            AdsManager.ShowInterstitial(onClosed);
        }
    }
}