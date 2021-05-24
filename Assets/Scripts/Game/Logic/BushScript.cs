using DG.Tweening;
using PFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public class BushScript : MonoBehaviour, IPlayerCollidable
    {
        AutoSequence _autoSequence;
        bool _isIn = false;

        void Awake()
        {
            _autoSequence = GetComponentInChildren<AutoSequence>();
            _autoSequence.Sequence.onStepComplete += AutoSequence_OnStepComplete;
            _autoSequence.Sequence.Restart();
            _autoSequence.Sequence.Pause();
        }

        #region IPlayerCollidable

        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            _isIn = true;

            _autoSequence.Sequence.Play();

            PrefabFactory.FxBushEnter.CreateRelative(player.Position);

            AudioManager.Play(AudioFactory.SfxBush);

            player.IsHiding = true;

            Messenger.Broadcast(GameEvent.Player_Bush, true);
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
            _isIn = false;

            player.IsHiding = false;

            Messenger.Broadcast(GameEvent.Player_Bush, false);
        }

        #endregion

        void AutoSequence_OnStepComplete()
        {
            if (!_isIn && _autoSequence.Sequence.IsPlaying() && _autoSequence.Sequence.CompletedLoops() % 2 == 0)
            {
                _autoSequence.Sequence.Restart();
                _autoSequence.Sequence.Pause();
            }
        }
    }
}