using PFramework;
using UnityEngine;

namespace Game
{
    public class ChestScript : MonoBehaviour, IPlayerCollidable
    {
        public GameObject g_Cage;
        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            g_Cage.SetActive(false);

            // ProfileManager.AddGold(50);

            // Helper.DebugLog("Win LEVEL: " + DataMain.LevelIndex);

            Messenger.Broadcast(GameEvent.Player_GoalReached);

            player.GoalReached();

            GetComponentInChildren<Animator>().Play(HashDictionary.Open);

            PrefabFactory.FxOpenChest.Create(transform.position);
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
        }
    }
}