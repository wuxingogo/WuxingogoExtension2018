using UnityEditor;
using UnityEngine;

 
public class HandleColorScope : GUI.Scope
{
    Color lastColor = Color.white;
    public HandleColorScope( Color c )
    {
        lastColor = Handles.color;
        Handles.color = c;
    }

    protected override void CloseScope()
    {
        Handles.color = lastColor;
    }
}

