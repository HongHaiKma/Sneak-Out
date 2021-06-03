#define TAPTIC

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace PFramework
{
    public class PGameMaster : MonoBehaviour
    {
        public static event Callback<bool> OnGamePaused;
        public static event Callback OnGameQuit;
        public static event Callback OnClearData;
        public static event Callback OnSceneChanged;

        #region Runtime Init

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            GameObject obj = new GameObject(typeof(PGameMaster).ToString());
            obj.AddComponent<PGameMaster>();

#if UNITY_EDITOR
            obj.AddComponent<PQuickAction>();
#endif

#if PDEBUG && !UNITY_EDITOR
            PConfig.ObjReporter.Create(obj.transform);
#endif

            InitVibration();
        }

        #endregion

        #region MonoBehaviour

        void Awake()
        {
#if UNITY_IOS
            if (UnityEngine.iOS.Device.lowPowerModeEnabled)
                Application.targetFrameRate = 30;
            else
                Application.targetFrameRate = 60;
#else
            Application.targetFrameRate = 60;
#endif

            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;

            // SetJavaHome();
        }

        // [InitializeOnLoadMethod]
        // static void SetJavaHome()
        // {
        //     //Debug.Log(EditorApplication.applicationPath);

        //     Debug.Log("JAVA_HOME in editor was: " + Environment.GetEnvironmentVariable("JAVA_HOME"));

        //     string newJDKPath = EditorApplication.applicationPath.Replace("Unity.app", "PlaybackEngines/AndroidPlayer/OpenJDK");

        //     if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJDKPath)
        //     {
        //         Environment.SetEnvironmentVariable("JAVA_HOME", newJDKPath);
        //     }

        //     Debug.Log("JAVA_HOME in editor set to: " + Environment.GetEnvironmentVariable("JAVA_HOME"));
        // }

        void OnApplicationPause(bool pause)
        {
            OnGamePaused?.Invoke(pause);
        }

        void OnApplicationQuit()
        {
            OnGameQuit?.Invoke();
        }

        #endregion

        #region Init

        static void InitVibration()
        {
#if TAPTIC
            Taptic.Taptic.tapticOn = PDataSettings.VibrationEnabled;

            PDataSettings.VibrationEnabledData.OnDataChanged += (enabled) => { Taptic.Taptic.tapticOn = enabled; };
#endif
        }

        #endregion

        #region Public Static

        public static void ClearData()
        {
            if (!Application.isPlaying)
                PDebug.Log("This action only works in PLAY MODE!");

            OnClearData?.Invoke();
        }

        #endregion

        void SceneManager_ActiveSceneChanged(Scene arg0, Scene arg1)
        {
            OnSceneChanged?.Invoke();
        }
    }
}