using PFramework;

namespace Game
{
    public class LevelScript : Singleton<LevelScript>
    {
#if UNITY_EDITOR

        [Sirenix.OdinInspector.Button("Refresh Level")]
        void UpdatePlatformRenderer()
        {
            PlatformScript[] platforms = GetComponentsInChildren<PlatformScript>();

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].FixRenderer();
            }

        }

        [Sirenix.OdinInspector.Button("Clear Nav Mesh Data")]
        void ClearNavMeshData()
        {
            GetComponent<UnityEngine.AI.NavMeshSurface>().navMeshData = null;
        }

#endif
    }
}