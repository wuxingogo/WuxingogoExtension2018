using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;

[CustomEditor( typeof( XScriptableObject ), true )]
public class XScriptObjectEditor : XBaseEditor {

	internal Vector2 _scrollPos = Vector2.zero;
    const int Xoffset = 5;
    const int XButtonWidth = 100;
    const int XButtonHeight = 20;

	public override void OnInspectorGUI()
	{
		GUILayout.Box(XResources.LogoTexture, GUILayout.Width(Screen.width - Xoffset), GUILayout.Height(100));
		base.OnInspectorGUI();
	}
}
