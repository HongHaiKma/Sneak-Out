using PFramework;
using UnityEngine;

namespace Game
{
    public class EndGameDetector : MonoBehaviour
    {
        bool _gameEnded = false;

        #region MonoBehaviour

        void Start()
        {
            Messenger.AddListener(GameEvent.Player_GoalReached, GameEvent_PlayerGoalReached);
            Messenger.AddListener(GameEvent.Player_Captured, GameEvent_PlayerCaptured);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.Player_GoalReached, GameEvent_PlayerGoalReached);
            Messenger.RemoveListener(GameEvent.Player_Captured, GameEvent_PlayerCaptured);
        }

        #endregion

        void GameEvent_PlayerGoalReached()
        {
            EndGame(true);
        }

        void GameEvent_PlayerCaptured()
        {
            EndGame(false);
        }

        void EndGame(bool isWin)
        {
            if (_gameEnded)
                return;

            _gameEnded = true;

            Messenger.Broadcast(GameEvent.Game_End, isWin);
        }
    }
}