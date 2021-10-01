
public sealed class DefaultPushButton : PushButton
{
    protected override void OnTriggerDown()
    {
        OnPushDown?.Invoke();
    }

    protected override void OnTriggerExit()
    {
        OnPushExit?.Invoke();
    }

    protected override void OnTriggerClick()
    {
        OnPushUp?.Invoke();
    }
}
