using PFramework;
using UnityEngine;

namespace Game
{
    public class DoorJumpPair : MonoBehaviour
    {
        [Header("Config")]
        [Range(0, 2)]
        [SerializeField] int _id;

        void Awake()
        {
            DoorJump[] doorJumps = GetComponentsInChildren<DoorJump>();
            Material mat = MaterialFactory.DoorJumpMaterials.GetClamp(_id);

            for (int i = 0; i < doorJumps.Length; i++)
            {
                doorJumps[i].Construct(mat);
            }
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                GizmosHelper.DrawLine(transform.GetChild(i).position, transform.GetChild(i - 1).position, Color.blue);
            }
        }

#endif
    }
}