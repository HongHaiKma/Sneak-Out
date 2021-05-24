using PFramework;
using UnityEngine;

namespace Game
{
    public class AudioFactory : SingletonScriptableObject<AudioFactory>
    {
        [Header("Music")]
        [SerializeField] AudioConfig _bgmMain;

        [Header("Sound")]
        [SerializeField] AudioConfig _sfxAlarm;
        [SerializeField] AudioConfig _sfxBush;
        [SerializeField] AudioConfig _sfxDoorOpen;
        [SerializeField] AudioConfig _sfxJump;
        [SerializeField] AudioConfig _sfxSuspect;
        [SerializeField] AudioConfig _sfxDetected;
        [SerializeField] AudioConfig _sfxFootStep;
        [SerializeField] AudioConfig _sfxSuccess;

        public static AudioConfig BgmMain => Instance._bgmMain;

        public static AudioConfig SfxAlarm => Instance._sfxAlarm;
        public static AudioConfig SfxBush => Instance._sfxBush;
        public static AudioConfig SfxDoorOpen => Instance._sfxDoorOpen;
        public static AudioConfig SfxJump => Instance._sfxJump;
        public static AudioConfig SfxSuspect => Instance._sfxSuspect;
        public static AudioConfig SfxDetected => Instance._sfxDetected;
        public static AudioConfig SfxFootStep => Instance._sfxFootStep;
        public static AudioConfig SfxSuccess => Instance._sfxSuccess;
    }
}