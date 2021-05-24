using Sirenix.OdinInspector;
using UnityEngine;

namespace PFramework
{
    [System.Serializable]
    public enum AudioType
    {
        Sound,
        Music,
    }

    [System.Serializable]
    public class AudioConfig
    {
        [HorizontalGroup]
        [LabelWidth(50f)]
        [SerializeField] AudioClip _clip;

        [HorizontalGroup]
        [LabelWidth(50f)]
        [SerializeField] AudioType _type;

        [HorizontalGroup]
        [Range(0f, 1f)]
        [LabelWidth(85f)]
        [SerializeField] float _volumeScale = 1f;

        public AudioClip Clip => _clip;
        public AudioType Type => _type;
        public float VolumeScale => _volumeScale;
    }
}