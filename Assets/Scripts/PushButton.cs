
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PushButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{
    /// <summary>
    /// ボタンが押された時
    /// </summary>
    public Action OnPushDown = delegate { };
    /// <summary>
    /// ボタンから外れた時
    /// </summary>
    public Action OnPushExit = delegate { };
    /// <summary>
    /// ボタン上で離された時
    /// </summary>
    public Action OnPushUp = delegate { };

    [SerializeField] EventTrigger trigger;
    // ScrollRectの中に存在するか？
    [SerializeField] protected bool isScrollElement;

    protected bool intaractable = true;

    bool isPushed;

    protected virtual void Awake()
    {
        if(!isScrollElement)
        {
            var entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;
            entryDown.callback.AddListener((data) => { PushDown(); });
            trigger.triggers.Add(entryDown);

            var entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => { PushExit(); });
            trigger.triggers.Add(entryExit);

            var entryClick = new EventTrigger.Entry();
            entryClick.eventID = EventTriggerType.PointerClick;
            entryClick.callback.AddListener((data) => { PushUp(); });
            trigger.triggers.Add(entryClick);
        }
        else
        {
            Destroy(trigger);
        }
    }

    /// <summary>
    /// 使用の可否を設定する
    /// </summary>
    /// <param name="intaractable"></param>
    public void SetIntaractable(bool intaractable)
    {
        this.intaractable = intaractable;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isScrollElement || !intaractable)
        {
            return;
        }

        isPushed = true;

        OnTriggerDown();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isScrollElement || !intaractable || !isPushed)
        {
            return;
        }

        isPushed = false;

        OnTriggerExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isScrollElement || !intaractable)
        {
            return;
        }

        isPushed = false;

        OnTriggerClick();
    }

    /// <summary>
    /// 押された時
    /// </summary>
    protected abstract void OnTriggerDown();
    /// <summary>
    /// 押された後ポインターが外れた時
    /// </summary>
    protected abstract void OnTriggerExit();
    /// <summary>
    /// 押された後トリガー上でポインターが押された時
    /// </summary>
    protected abstract void OnTriggerClick();

    void PushDown()
    {
        if (!intaractable)
        {
            return;
        }

        isPushed = true;

        OnTriggerDown();
    }

    void PushExit()
    {
        if (!intaractable
            || !isPushed)
        {
            return;
        }

        isPushed = false;

        OnTriggerExit();
    }

    void PushUp()
    {
        if (!intaractable)
        {
            return;
        }

        isPushed = false;

        OnTriggerClick();
    }

#if UNITY_EDITOR

    void Reset()
    {
        trigger = GetComponent<EventTrigger>();
    }

    /// <summary>
    /// このボタンがスクロール要素の中にあるか？
    /// </summary>
    /// <param name="isScrollElement"></param>
    public void SetIsScrollElement(bool isScrollElement)
    {
        this.isScrollElement = isScrollElement;
    }

#endif
}
