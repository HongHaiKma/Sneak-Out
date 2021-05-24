#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace PFramework
{
    public class PQuickAction : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneTransitionManager.ReloadScene();
            }
        }

        [MenuItem("PFramework/Clear Data")]
        public static void ClearData()
        {
            PGameMaster.ClearData();
        }
    }
}

#endif