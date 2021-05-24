using UnityEngine;

namespace Game
{
    public class PlatformScript : MonoBehaviour
    {
#if UNITY_EDITOR

        static readonly float PlatformDistance = 5f;

        public void FixRenderer()
        {
            bool right = gameObject.scene.GetPhysicsScene().Raycast(transform.TransformPoint(Vector3.right * PlatformDistance + Vector3.up), Vector3.down, 2f, GameConfig.LayerMaskObject);
            bool left = gameObject.scene.GetPhysicsScene().Raycast(transform.TransformPoint(Vector3.left * PlatformDistance + Vector3.up), Vector3.down, 2f, GameConfig.LayerMaskObject);
            bool foward = gameObject.scene.GetPhysicsScene().Raycast(transform.TransformPoint(Vector3.forward * PlatformDistance + Vector3.up), Vector3.down, 2f, GameConfig.LayerMaskObject);
            bool back = gameObject.scene.GetPhysicsScene().Raycast(transform.TransformPoint(Vector3.back * PlatformDistance + Vector3.up), Vector3.down, 2f, GameConfig.LayerMaskObject);

            PFramework.PDebug.DrawLine(transform.position + Vector3.left * PlatformDistance + Vector3.up, transform.position + Vector3.left * PlatformDistance + Vector3.up + Vector3.down * 2f, Color.red, 1f);

            transform.GetChild(0).GetChild(0).gameObject.SetActive(!back);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(!left);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(!foward);
            transform.GetChild(0).GetChild(3).gameObject.SetActive(!right);
        }

#endif
    }
}