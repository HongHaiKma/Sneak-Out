using PFramework;
using UnityEngine;

namespace Game
{
    public class LevelConstructor : MonoBehaviour
    {
        #region MonoBehaviour

        void Start()
        {
            PrefabFactory.ObjLevels.GetLoop(DataMain.LevelIndex)
                .Create();
        }

        #endregion
    }
}