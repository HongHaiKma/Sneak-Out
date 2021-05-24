using UnityEngine;

namespace PFramework
{
    public class AudioScript : MonoBehaviour
    {
        AudioConfig _config;
        AudioSource _audioSource;

        public AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                    _audioSource = gameObject.AddComponent<AudioSource>();
                return _audioSource;
            }
        }

        #region MonoBehaviour

        void Awake()
        {
            PDataSettings.SoundEnabledData.OnDataChanged += SoundEnabled_OnDataChanged;
            PDataSettings.MusicEnabledData.OnDataChanged += MusicEnabled_OnDataChanged;
        }

        #endregion

        public AudioSource Play(AudioConfig config, bool loop = false)
        {
            _config = config;

            UpdateVolume();

            AudioSource.clip = config.Clip;
            AudioSource.loop = loop;
            AudioSource.Play();

            return AudioSource;
        }

        void SoundEnabled_OnDataChanged(bool enabled)
        {
            UpdateVolume();
        }

        void MusicEnabled_OnDataChanged(bool enabled)
        {
            UpdateVolume();
        }

        void UpdateVolume()
        {
            if (_config == null)
                return;

            AudioSource.volume = _config.VolumeScale;
            AudioSource.mute = _config.Type == AudioType.Sound ? !PDataSettings.SoundEnabled : !PDataSettings.MusicEnabled;
        }
    }
}