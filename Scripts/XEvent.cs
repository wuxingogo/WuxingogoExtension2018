using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class XEvent
{
    public string title = "";
    public CallAction action = CallAction.Continue;
    public enum CallAction
    {
        Break,
        Continue
    }
}
