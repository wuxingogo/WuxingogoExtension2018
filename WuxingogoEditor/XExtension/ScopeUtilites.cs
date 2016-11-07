using UnityEditor;
using UnityEngine;
public class HandleColor : GUI.Scope
{
    Color lastColor = Color.white;
    public HandleColor( Color c )
    {
        lastColor = Handles.color;
        Handles.color = c;
    }

    protected override void CloseScope()
    {
        Handles.color = lastColor;
    }
}

