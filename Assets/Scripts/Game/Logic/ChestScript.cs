using PFramework;
using UnityEngine;

namespace Game
{
    public class ChestScript : MonoBehaviour, IPlayerCollidable
    {
        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
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