
using UnityEngine;
using DG.Tweening;

public sealed class AnimationPushButton : PushButton
{
    [SerializeField] RectTransform rectTransform;

    float onDownScale = 0.95f;
    float onExitShakeStrength = 45f;
    float onClickScale = 1.15f;

    float duration = 0.05f;
    Ease easing = default;

    Tween scaleTween;
    Sequence outSequence;

    protected override void Awake()
    {
        base.Awake();

        scaleTween.OnKill(() => rectTransform.localScale = new Vector3(1, 1, 1));
    }

    void OnDestroy()
    {
        if(scaleTween != null
            && scaleTween.IsActive())
        {
            scaleTween.Kill();
        }

        if (outSequence != null
            && outSequence.IsActive())
        {
            outSequence.Kill();
        }
    }

    protected override void OnTriggerDown()
    {
        if (isScrollElement)
        {
            OnPushDown?.Invoke();
        }
        else
        {
            scaleTween = rectTransform.DOScale(onDownScale, duration)
            .SetEase(easing)
            .OnComplete(() => OnPushDown?.Invoke());
        }
    }

    protected override void OnTriggerExit()
    {
        outSequence = DOTween.Sequence();

        outSequence.Append(rectTransform.DOShakeRotation(duration, onExitShakeStrength, 10, 0)
            .SetEase(easing)
            .OnStart(() => rectTransform.DOScale(1, duration)));

        outSequence.AppendCallback(() => OnPushExit?.Invoke());

        outSequence.Play();
    }

    protected override void OnTriggerClick()
    {
        outSequence = DOTween.Sequence();

        outSequence.Append(rectTransform.DOScale(onClickScale, duration).SetEase(easing));
        outSequence.Append(rectTransform.DOScale(1f, duration).SetEase(easing));

        outSequence.AppendCallback(() => OnPushUp?.Invoke());

        outSequence.Play();
    }

    void Reset()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
