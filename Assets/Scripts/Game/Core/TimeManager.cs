using PFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class TimeManager : HardSingleton<TimeManager>
    {
        #region Runtime Init

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            GameObject obj = new GameObject(typeof(TimeManager).ToString());
            obj.AddComponent<TimeManager>();
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
        }

        void SceneManager_ActiveSceneChanged(Scene arg0, Scene arg1)
        {
            Time.timeScale = 1f;
        }

        public static void Pause()
        {
            Time.timeScale = 0f;
        }

        public static void Resume()
        {
            Time.timeScale = 1f;
        }
    }
}