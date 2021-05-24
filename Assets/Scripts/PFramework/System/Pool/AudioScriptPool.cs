using UnityEngine;

namespace PFramework
{
    public class AudioScriptPool : BasePool<AudioScript>
    {
        public AudioScriptPool(GameObject prefab, Transform root, int initAtStart) : base(prefab, root, initAtStart)
        {
        }

        protected override void SetActive(AudioScript item, bool enabled)
        {
            // Do nothing
        }

        protected override bool IsActive(AudioScript item)
        {
            return item.AudioSource.isPlaying;
        }
    }
}