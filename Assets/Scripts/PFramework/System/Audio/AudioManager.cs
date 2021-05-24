namespace PFramework
{
    public class AudioManager : HardSingleton<AudioManager>
    {
        static readonly int PoolInitCount = 5;

        AudioScriptPool _pool;

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            _pool = new AudioScriptPool(null, transform, PoolInitCount);
        }

        #endregion

        #region Public

        public static AudioScript Play(AudioConfig config, bool loop = false)
        {
            AudioScript audio = SafeInstance._pool.GetItem();
            audio.Play(config, loop);

            return audio;
        }

        #endregion
    }
}