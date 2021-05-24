using PFramework;
using UnityEngine;

namespace Game
{
    public class SpriteFactory : SingletonScriptableObject<SpriteFactory>
    {
        [Header("Emote")]
        [SerializeField] Sprite _emoteSuspect;
        [SerializeField] Sprite _emoteDetected;

        public static Sprite EmoteSuspect => Instance._emoteSuspect;
        public static Sprite EmoteDetected => Instance._emoteDetected;
    }
}