using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class CharacterEmote : CacheMonoBehaviour
    {
        static readonly Vector3 Euler = new Vector3(0f, -45f, 0f);

        [Header("Referecne")]
        [SerializeField] SpriteRenderer _sprEmote;

        AutoSequence _autoSeqeuence;

        #region MonoBehaviour

        void Awake()
        {
            _autoSeqeuence = GetComponent<AutoSequence>();
            _autoSeqeuence.Sequence.Restart();
            _autoSeqeuence.Sequence.Pause();

            CacheGameObject.SetActive(false);
        }

        void Update()
        {
            CacheTransform.eulerAngles = Euler;
        }

        #endregion

        #region Public

        public void PlaySuspect()
        {
            _sprEmote.sprite = SpriteFactory.EmoteSuspect;
            _sprEmote.color = Color.yellow;

            Play();

            AudioManager.Play(AudioFactory.SfxSuspect);
        }

        public void PlayDetected()
        {
            _sprEmote.sprite = SpriteFactory.EmoteDetected;
            _sprEmote.color = Color.red;

            Play();

            AudioManager.Play(AudioFactory.SfxDetected);
        }

        #endregion

        void Play()
        {
            CacheGameObject.SetActive(true);

            _autoSeqeuence.Sequence.Restart();
            _autoSeqeuence.Sequence.Play();
        }
    }
}