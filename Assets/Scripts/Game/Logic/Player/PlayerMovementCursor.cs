using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerMovementCursor : CacheMonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Transform _tfCursor;

        [Header("Config")]
        [SerializeField] Vector2 _radiusRange;
        [SerializeField] Gradient _colorRange;

        #region MonoBehaviour

        void Awake()
        {
            GetComponentInParent<CharacterMovement>().OnSpeedChanged += CharacterMovement_OnSpeedChanged;

            CharacterMovement_OnSpeedChanged(0f);
        }

        #endregion

        void CharacterMovement_OnSpeedChanged(float speed)
        {
            _tfCursor.gameObject.SetActive(speed > 0f);

            float radius = Mathf.Lerp(_radiusRange.x, _radiusRange.y, speed);
            _tfCursor.SetLocalZ(radius);
        }
    }
}