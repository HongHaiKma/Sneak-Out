using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace PFramework
{
    public class SceneTransition : MonoBehaviour
    {
        static readonly string StrFadeIn = "FadeIn";
        static readonly string StrFadeOut = "FadeOut";

        enum State
        {
            // Wait for load scene command
            Idle,
            // Playing fade in animation
            FadeIn,
            // End of fade in animation, load new scene
            Loading,
            // Playing fade out animation
            FadeOut,
        }

        [Tooltip("Must have: Animation component + 2 clips: \"FadeIn\" & \"FadeOut\"")]
        // Speed to fade in and fade out
        float _fadeInAnimSpeed = 0;
        float _fadeOutAnimSpeed = 0;

        // Index of scene will be loaded
        int _sceneIndex = 0;
        // Scene async
        AsyncOperation _sceneAsync;
        // Animator component
        Animator _animator;
        // State machine
        StateMachine<State> _stateMachine;
        // Tween
        Tween _tween;

        #region MonoBehaviour functions

        void Awake()
        {
            // Get component reference
            _animator = GetComponentInChildren<Animator>();

            // Calculate fade in/out speed
            CalculateAnimSpeed();

            // Prevent object destroyed when load scene
            DontDestroyOnLoad(gameObject);

            // Construct state machine
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.FadeIn, State_OnFadeInStart);
            _stateMachine.AddState(State.Loading, null, State_OnLoadingUpdate);
            _stateMachine.AddState(State.FadeOut, State_OnFadeOutStart);
            _stateMachine.AddState(State.Idle);

            _stateMachine.CurrentState = State.Idle;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        void Update()
        {
            _stateMachine.Update();
        }

        #endregion

        #region States

        void State_OnFadeInStart()
        {
            // Load scene async
            _sceneAsync = SceneManager.LoadSceneAsync(_sceneIndex);
            _sceneAsync.allowSceneActivation = false;

            //Play fade in animation
            _animator.SetTrigger(StrFadeIn);
            _animator.speed = _fadeInAnimSpeed;

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(PConfig.SceneTransitionFadeInDuration + PConfig.SceneTransitionLoadDuration, () =>
            {
                _stateMachine.CurrentState = State.Loading;
                _sceneAsync.allowSceneActivation = true;
            }, true);
        }

        void State_OnLoadingUpdate()
        {
            if (_sceneAsync.isDone)
            {
                _stateMachine.CurrentState = State.FadeOut;
            }
        }

        void State_OnFadeOutStart()
        {
            //Play fade out animation
            _animator.SetTrigger(StrFadeOut);
            _animator.speed = _fadeOutAnimSpeed;

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(PConfig.SceneTransitionFadeOutDuration, () =>
            {
                _stateMachine.CurrentState = State.Idle;
                gameObject.SetActive(false);
            }, true);
        }

        #endregion

        #region Public

        public void Load(int sceneIndex)
        {
            if (_stateMachine.CurrentState != State.Idle)
            {
                PDebug.Log("[{0}] A scene is loading, can't execute load scene command!", typeof(SceneTransition));
                return;
            }

            _sceneIndex = sceneIndex;
            _stateMachine.CurrentState = State.FadeIn;
        }

        public void Reload()
        {
            Load(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion

        void CalculateAnimSpeed()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == StrFadeIn)
                {
                    _fadeInAnimSpeed = clip.length / PConfig.SceneTransitionFadeInDuration;
                }
                else if (clip.name == StrFadeOut)
                {
                    _fadeOutAnimSpeed = clip.length / PConfig.SceneTransitionFadeOutDuration;
                }
            }
        }
    }

    public static class SceneTransitionManager
    {
        static SceneTransition _instance;

        public static event Callback OnLoadScene;

        static void LazyInit()
        {
            if (_instance == null)
            {
                _instance = PConfig.ObjSceneTransition.Create().GetComponent<SceneTransition>();
            }
            _instance.gameObject.SetActive(true);
        }

        #region Public

        public static void LoadScene(int sceneIndex)
        {
            LazyInit();

            _instance.Load(sceneIndex);

            OnLoadScene?.Invoke();
        }

        public static void ReloadScene()
        {
            LazyInit();

            _instance.Reload();
        }

        public static int GetSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        #endregion
    }
}