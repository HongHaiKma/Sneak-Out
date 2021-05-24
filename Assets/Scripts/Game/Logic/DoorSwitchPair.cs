using PFramework;
using UnityEngine;

namespace Game
{
    public class DoorSwitchPair : MonoBehaviour
    {
        [Header("Config")]
        [Range(0, 2)]
        [SerializeField] int _id;

        void Awake()
        {
            DoorSwitch door = GetComponentInChildren<DoorSwitch>();
            DoorSwitchButton button = GetComponentInChildren<DoorSwitchButton>();

            door.Construct(MaterialFactory.DoorSwitchMaterials.GetClamp(_id));
            button.Construct(door, MaterialFactory.DoorSwitchButtonMaterials.GetClamp(_id));
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