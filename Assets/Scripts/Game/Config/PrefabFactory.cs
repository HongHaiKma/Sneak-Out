using PFramework;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PrefabFactory : SingletonScriptableObject<PrefabFactory>
    {
        [Header("Common")]
        [SerializeField] List<GameObject> _objLevels;
        [SerializeField] GameObject _objDoorJumpTips;

        [Header("Effect")]
        [SerializeField] GameObject _fxSleep;
        [SerializeField] GameObject _fxOpenChest;
        [SerializeField] GameObject _fxBushEnter;
        [SerializeField] GameObject _fxRingNoise;
        [SerializeField] GameObject _fxSmoke;
        [SerializeField] GameObject _fxIce;

        [Header("Popup")]
        [SerializeField] GameObject _popupLose;
        [SerializeField] GameObject _popupWin;
        [SerializeField] GameObject _popupPause;
        [SerializeField] GameObject _popupMessage;
        [SerializeField] GameObject _popupTutorial;
        [SerializeField] GameObject _popupRating;

        public static List<GameObject> ObjLevels => Instance._objLevels;
        public static GameObject ObjDoorJumpTips => Instance._objDoorJumpTips;

        public static GameObject FxSleep => Instance._fxSleep;
        public static GameObject FxOpenChest => Instance._fxOpenChest;
        public static GameObject FxBushEnter => Instance._fxBushEnter;
        public static GameObject FxRingNoise => Instance._fxRingNoise;
        public static GameObject FxSmoke => Instance._fxSmoke;
        public static GameObject FxIce => Instance._fxIce;

        public static GameObject PopupLose => Instance._popupLose;
        public static GameObject PopupWin => Instance._popupWin;
        public static GameObject PopupPause => Instance._popupPause;
        public static GameObject PopupMessage => Instance._popupMessage;
        public static GameObject PopupTutorial => Instance._popupTutorial;
        public static GameObject PopupRating => Instance._popupRating;
    }
}