using PFramework;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MaterialFactory : SingletonScriptableObject<MaterialFactory>
    {
        [Header("Door Jump")]
        [SerializeField] List<Material> _doorJumpMaterials;

        [Header("Door Switch")]
        [SerializeField] List<Material> _doorSwitchMaterials;
        [SerializeField] List<Material> _doorSwitchButtonMaterials;

        public static List<Material> DoorJumpMaterials => Instance._doorJumpMaterials;

        public static List<Material> DoorSwitchMaterials => Instance._doorSwitchMaterials;
        public static List<Material> DoorSwitchButtonMaterials => Instance._doorSwitchButtonMaterials;
    }
}