using PFramework;
using UnityEngine;

namespace Game
{
    public class DataMain : PDataBlock<DataMain>
    {
        [SerializeField] int _levelIndex;
        [SerializeField] int _levelMax;

        [SerializeField] bool _rateGame;

        public static int LevelIndex { get { return Instance._levelIndex; } set { Instance._levelIndex = value; } }
        public static int LevelMax { get { return Instance._levelMax; } set { Instance._levelMax = value; } }

        public static bool RateGame { get { return Instance._rateGame; } set { Instance._rateGame = value; } }
    }
}