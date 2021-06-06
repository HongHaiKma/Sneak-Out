using System.Collections;
using System.Collections.Generic;
// using Firebase;
// using Firebase.Analytics;
using PFramework;
using UnityEngine;

namespace Game
{
    public class GameMaster : MonoBehaviour
    {
        #region Runtime Init

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            GameObject obj = new GameObject(typeof(GameMaster).ToString());
            obj.AddComponent<GameMaster>();
            obj.AddComponent<AdsShowHandler>();

#if UNITY_EDITOR
            obj.AddComponent<QuickAction>();
#endif
        }

        #endregion

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
#if !UNITY_EDITOR

            // InitFirebase();

#endif

            AudioManager.Play(AudioFactory.BgmMain, true);
        }

        #region Firebase

        // void InitFirebase()
        // {
        //     FirebaseApp app;

        //     FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //     {
        //         var dependencyStatus = task.Result;
        //         if (dependencyStatus == Firebase.DependencyStatus.Available)
        //         {
        //             // Create and hold a reference to your FirebaseApp,
        //             // where app is a Firebase.FirebaseApp property of your application class.
        //             app = FirebaseApp.DefaultInstance;

        //             // Set a flag here to indicate whether Firebase is ready to use by your app.
        //             // Enable analytics
        //             FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        //         }
        //         else
        //         {
        //             UnityEngine.Debug.LogError(System.String.Format(
        //               "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //             // Firebase Unity SDK is not safe to use here.
        //         }

        //     });
        // }

        #endregion
    }
}