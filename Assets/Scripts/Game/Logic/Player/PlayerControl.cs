using PFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Game
{
    public class PlayerControl : MonoBehaviour
    {
        Vector2 _dragBeginPos;

        public EnemyScript m_EnemyScript;

        public PlayerScript m_PlayerScript;
        public CharacterMovement m_CharacterMovement;

        public event Callback<Vector3, float> OnMove;
        public event Callback OnStop;

        #region MonoBehaviour

        void Start()
        {
            Messenger.AddListener<PointerEventData>(GameEvent.Control_DragBegin, GameEvent_ControlDragBegin);
            Messenger.AddListener<PointerEventData>(GameEvent.Control_Drag, GameEvent_ControlDrag);
            Messenger.AddListener<PointerEventData>(GameEvent.Control_DragEnd, GameEvent_ControlDragEnd);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<PointerEventData>(GameEvent.Control_DragBegin, GameEvent_ControlDragBegin);
            Messenger.RemoveListener<PointerEventData>(GameEvent.Control_Drag, GameEvent_ControlDrag);
            Messenger.RemoveListener<PointerEventData>(GameEvent.Control_DragEnd, GameEvent_ControlDragEnd);
        }

        #endregion

        void GameEvent_ControlDragBegin(PointerEventData pointer)
        {
            _dragBeginPos = pointer.position;
        }

        void GameEvent_ControlDrag(PointerEventData pointer)
        {
            Vector3 dir = (pointer.position - _dragBeginPos).Rotate(45f).normalized.ToXZ();
            float speedNormalized = Mathf.Clamp01((Vector2.Distance(_dragBeginPos, pointer.position) / Screen.dpi) / GameConfig.ControlMaxDragDistance);

            OnMove?.Invoke(dir, speedNormalized);

            if (m_PlayerScript.IsGoodToKillEnemy())
            {
                m_PlayerScript.KillEnemy();
            }
        }

        void GameEvent_ControlDragEnd(PointerEventData pointer)
        {
            OnStop?.Invoke();
        }
    }
}