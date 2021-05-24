using System.Collections.Generic;
using PFramework;
using UnityEngine;

namespace Game
{
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        [Header("Layer Mask")]
        [SerializeField] LayerMask _layerMaskObject;

        [Header("Enemy")]
        [SerializeField] float _enemyWalkSpeed;
        [SerializeField] float _enemyRunSpeed;
        [SerializeField] float _enemyFreezeDuration;

        [Header("Control")]
        [SerializeField] float _controlMaxDragDistance = 1f;

        [Header("Ads")]
        [SerializeField] string _adsID;
        [SerializeField] float _adsInterstitialDelay;
        [SerializeField] int _adsInterstitialMinLevel;

        [Header("Tutorial")]
        [SerializeField] List<TutorialConfig> _tutorialConfigs;

        [Header("Rating")]
        [SerializeField] int _ratingStart;
        [SerializeField] int _ratingRepeatCount;

        public static LayerMask LayerMaskObject => Instance._layerMaskObject;

        public static float EnemyWalkSpeed => Instance._enemyWalkSpeed;
        public static float EnemyRunSpeed => Instance._enemyRunSpeed;
        public static float EnemyFreezeDuration => Instance._enemyFreezeDuration;

        public static float ControlMaxDragDistance => Instance._controlMaxDragDistance;

        public static string AdsID => Instance._adsID;
        public static float AdsInterstitialDelay => Instance._adsInterstitialDelay;
        public static int AdsInterstitialMinLevel => Instance._adsInterstitialMinLevel;

        public static List<TutorialConfig> TutorialConfigs => Instance._tutorialConfigs;

        public static int RatingStart => Instance._ratingStart;
        public static int RatingRepeatCount => Instance._ratingRepeatCount;
    }
}