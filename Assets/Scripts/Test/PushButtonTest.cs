
using UnityEngine;

public sealed class PushButtonTest : MonoBehaviour
{
    [SerializeField] PushButton[] pushButtons = new PushButton[0];

    void Start()
    {
        foreach(var pushButton in pushButtons)
        {
            pushButton.OnPushDown += OnPushDown;
            pushButton.OnPushExit += OnPushExit;
            pushButton.OnPushUp += OnPushUp;
        }
    }

    void OnPushDown()
    {
        Debug.Log("押された");
    }

    void OnPushExit()
    {
        Debug.Log("押された後ポインターが外れた");
    }

    void OnPushUp()
    {
        Debug.Log("押された後トリガー上でポインターが押された");
    }
}
