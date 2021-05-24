using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TutorialUI : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Image _imgMain;

        void Awake()
        {
            _imgMain.sprite = GameHelper.GetCurrentTutorialConfig().SprMain;
        }
    }
}