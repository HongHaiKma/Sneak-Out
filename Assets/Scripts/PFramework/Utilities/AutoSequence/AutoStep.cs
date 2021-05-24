using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PFramework
{
    [System.Serializable]
    public class AutoStep
    {
        public virtual void Construct(AutoSequence autoSequence, Sequence sequence)
        {
        }
    }

    [System.Serializable]
    public class AutoStepCallback : AutoStep
    {
        [SerializeField] UnityEvent _event;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            sequence.AppendCallback(() => { _event?.Invoke(); });
        }
    }

    [System.Serializable]
    public class AutoStepInterval : AutoStep
    {
        [SerializeField] float _interval;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            sequence.AppendInterval(_interval);
        }
    }

    [System.Serializable]
    public class AutoStepTween : AutoStep
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected Ease _ease = Ease.Linear;
        [SerializeField] protected bool _join;

        public override void Construct(AutoSequence autoSequence, Sequence sequence)
        {
            base.Construct(autoSequence, sequence);

            Tween tween = ConstructTween(autoSequence, sequence);

            tween.SetEase(_ease);

            if (_join)
                sequence.Join(tween);
            else
                sequence.Append(tween);
        }

        protected virtual Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            return null;
        }
    }

    [System.Serializable]
    public class AutoStepTransform : AutoStepTween
    {
        [System.Serializable]
        public enum Type
        {
            Position,
            Rotation,
            Scale,
        }

        [SerializeField] Type _type;
        [SuffixLabel("@GetSpeed()")]
        [SerializeField] Vector3 _end;
        [SerializeField] bool _persist;

        protected string GetSpeed()
        {
            if (_persist)
                return "Unknow";
            else if (_duration <= 0)
                return "Speed: 0";
            else
                return string.Format("Speed: {0}", _end.magnitude / _duration);
        }

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            Tween tween = null;
            Transform transform = autoSequence.CacheTransform;

            switch (_type)
            {
                case Type.Position:
                    {
                        Vector3 end = _persist ? _end : transform.localPosition + _end;

                        if (_duration > 0f)
                        {
                            tween = transform.DOLocalMove(end, _duration)
                               .ChangeStartValue(transform.localPosition);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localPosition = end; });
                        }

                        transform.localPosition = end;
                    }
                    break;
                case Type.Rotation:
                    {
                        Vector3 end = _persist ? _end : transform.localEulerAngles + _end;

                        if (_duration > 0f)
                        {
                            tween = transform.DOLocalRotate(end, _duration)
                                .ChangeStartValue(transform.localEulerAngles);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localEulerAngles = end; });
                        }

                        transform.localEulerAngles = end;
                    }
                    break;
                case Type.Scale:
                    {
                        Vector3 end = _persist ? _end : transform.localScale + _end;

                        if (_duration > 0)
                        {
                            tween = transform.DOScale(end, _duration)
                               .ChangeStartValue(transform.localScale);
                        }
                        else
                        {
                            sequence.AppendCallback(() => { transform.localScale = end; });
                        }

                        transform.localScale = end;
                    }
                    break;
            }

            return tween;
        }
    }

    public class AutoStepRectTransform : AutoStepTween
    {
        [SuffixLabel("@GetSpeed()")]
        [SerializeField] Vector3 _end;

        protected string GetSpeed()
        {
            if (_duration <= 0)
                return "Speed: 0";
            else
                return string.Format("Speed: {0}", _end.magnitude / _duration);
        }

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            Tween tween = null;
            RectTransform rectTransform = autoSequence.CacheRectTransform;

            Vector3 end = rectTransform.anchoredPosition3D + _end;

            if (_duration > 0f)
            {
                tween = rectTransform.DOAnchorPos3D(end, _duration)
                   .ChangeStartValue(rectTransform.anchoredPosition3D);
            }
            else
            {
                sequence.AppendCallback(() => { rectTransform.anchoredPosition3D = end; });
            }

            rectTransform.anchoredPosition3D = end;

            return tween;
        }
    }

    public class AutoStepGraphicColor : AutoStepTween
    {
        [SerializeField] Color _color;

        protected override Tween ConstructTween(AutoSequence autoSequence, Sequence sequence)
        {
            return autoSequence.CacheGraphic.DOColor(_color, _duration)
                .ChangeStartValue(autoSequence.CacheGraphic.color);
        }
    }
}
