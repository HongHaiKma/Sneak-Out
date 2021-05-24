using DG.Tweening;
using UnityEngine;

namespace PFramework
{
    public abstract class PopupTransition : MonoBehaviour
    {
        public abstract Tween ConstructTransition(PopupBehaviour popup);
    }
}