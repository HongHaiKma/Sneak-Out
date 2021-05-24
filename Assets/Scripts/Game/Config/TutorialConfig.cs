using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class TutorialConfig
    {
        [SerializeField] int _levelIndex;
        [SerializeField] Sprite _sprMain;

        public int LevelIndex { get { return _levelIndex; } }
        public Sprite SprMain { get { return _sprMain; } }
    }
}