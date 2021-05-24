#if UNITY_EDITOR

using UnityEngine;
using PFramework;

namespace Game
{
    public class QuickAction : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                DataMain.LevelIndex++;
                SceneTransitionManager.ReloadScene();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                DataMain.LevelIndex--;

                if (DataMain.LevelIndex < 0)
                    DataMain.LevelIndex = GameHelper.TotalLevel - 1;

                SceneTransitionManager.ReloadScene();
            }
        }
    }
}

#endif