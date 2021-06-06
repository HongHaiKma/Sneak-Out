// using UnityEngine;
// using PFramework;
// using DG.Tweening;

// namespace Game
// {
//     public static class AdsManager
//     {
//         static readonly string ID = GameConfig.AdsID;

//         static bool _videoRewarded = false;

//         static Callback<bool> _onVideoRewarded;
//         static Callback _onInterstitialClosed;

//         public static event Callback OnBannerLoaded;
//         public static event Callback OnVideoRewarded;

//         static Tween _tween;

//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//         static void Init()
//         {
// #if ADS

//             // IronSource.Agent.validateIntegration();
//             // IronSource.Agent.init(ID);

//             // IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
//             // IronSource.Agent.loadInterstitial();

//             // IronSourceEvents.onRewardedVideoAdRewardedEvent += IronSourceEvents_onRewardedVideoAdRewardedEvent;
//             // IronSourceEvents.onRewardedVideoAdClosedEvent += IronSourceEvents_onRewardedVideoAdClosedEvent;
//             // IronSourceEvents.onRewardedVideoAdShowFailedEvent += IronSourceEvents_onRewardedVideoAdShowFailedEvent;

//             // IronSourceEvents.onInterstitialAdClosedEvent += IronSourceEvents_onInterstitialAdClosedEvent;
//             // IronSourceEvents.onInterstitialAdShowFailedEvent += IronSourceEvents_onInterstitialAdShowFailedEvent;

//             // IronSourceEvents.onBannerAdLoadFailedEvent += IronSourceEvents_onBannerAdLoadFailedEvent;
//             // IronSourceEvents.onBannerAdLoadedEvent += IronSourceEvents_onBannerAdLoadedEvent;

//             PGameMaster.OnGamePaused += PGameMaster_OnGamePaused;

// #endif
//         }

//         #region Events

// #if ADS

//         static void PGameMaster_OnGamePaused(bool paused)
//         {
//             // IronSource.Agent.onApplicationPause(paused);
//         }

//         // static void IronSourceEvents_onRewardedVideoAdShowFailedEvent(IronSourceError obj)
//         // {
//         //     _onVideoRewarded?.Invoke(_videoRewarded);
//         // }

//         static void IronSourceEvents_onInterstitialAdClosedEvent()
//         {
//             _onInterstitialClosed?.Invoke();

//             IronSource.Agent.loadInterstitial();
//         }

//         static void IronSourceEvents_onInterstitialAdShowFailedEvent(IronSourceError obj)
//         {
//             _onInterstitialClosed?.Invoke();
//         }

//         static void IronSourceEvents_onRewardedVideoAdRewardedEvent(IronSourcePlacement obj)
//         {
//             _videoRewarded = true;

//             OnVideoRewarded?.Invoke();
//         }

//         static void IronSourceEvents_onRewardedVideoAdClosedEvent()
//         {
//             _onVideoRewarded?.Invoke(_videoRewarded);
//         }

//         static void IronSourceEvents_onBannerAdLoadedEvent()
//         {
//             OnBannerLoaded?.Invoke();
//         }

//         static void IronSourceEvents_onBannerAdLoadFailedEvent(IronSourceError obj)
//         {
//             _tween?.Kill();
//             _tween = DOVirtual.DelayedCall(1f, () =>
//             {
//                 if (!IronSource.Agent.isInterstitialReady())
//                     IronSource.Agent.loadInterstitial();
//             });
//         }

// #endif

//         #endregion

//         #region Public Static

//         /*

//         public static Vector2 GetBannerSizePixel()
//         {
// #if UNITY_IOS

//             return (Vector2)GetBannerSize();

// #elif UNITY_ANDROID && !UNITY_EDITOR

//             Vector2 bannerSize = GetBannerSize();

//             return new Vector2(bannerSize.x * (DisplayMetricsAndroid.DPI / 160f), bannerSize.y * (DisplayMetricsAndroid.DPI / 160f));

// #else

//             return Vector2.zero;

// #endif
//         }
//         */

//         public static void ShowInterstitial(Callback onClosed = null)
//         {
// #if ADS
//             _onInterstitialClosed = onClosed;

//             if (!IronSource.Agent.isInterstitialReady())
//             {
//                 IronSource.Agent.loadInterstitial();
//                 onClosed?.Invoke();
//             }
//             else
//             {
//                 IronSource.Agent.showInterstitial();
//             }
// #else
//             onClosed?.Invoke();
// #endif
//         }

//         public static bool IsVideoReady()
//         {
// #if ADS
//             return IronSource.Agent.isRewardedVideoAvailable();
// #else
//             return false;
// #endif
//         }

//         public static bool IsInterstitialReady()
//         {
// #if ADS
//             return IronSource.Agent.isInterstitialReady();
// #else
//             return false;
// #endif
//         }

//         public static void ShowVideo(Callback<bool> onReward = null)
//         {
// #if ADS && !UNITY_EDITOR
//             _videoRewarded = false;

//             _onVideoRewarded = onReward;

//             IronSource.Agent.showRewardedVideo();
// #else
//             onReward?.Invoke(true);
// #endif
//         }

//         public static void ShowBanner()
//         {
// #if ADS

//             IronSource.Agent.displayBanner();

// #endif
//         }

//         public static void HideBanner()
//         {
// #if ADS

//             IronSource.Agent.hideBanner();

// #endif
//         }

//         #endregion

//         /*
//         static Vector2Int GetBannerSize()
//         {
// #if UNITY_IOS

//             if (UnityEngine.iOS.Device.generation.ToString().IndexOf("iPad") > -1)
//                 return new Vector2Int(728, 90);
//             else
//                 return new Vector2Int(320, 50);

// #elif UNITY_ANDROID && !UNITY_EDITOR

//             float actualHeight = Screen.height / DisplayMetricsAndroid.DPI;
//             float dp = actualHeight * 160f;

//             if (dp > 720f)
//                 return new Vector2Int(728, 90);
//             else
//                 return new Vector2Int(320, 50);

// #else

//             return Vector2Int.zero;

// #endif
//         }
//         */
//     }
// }
