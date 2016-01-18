using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

public class XBaseEditor : Editor {

	public float CurrHeight = 0;
	public const float StartY = 45;
	public const float StartX = 10;
	public const float FieldOffset = 5;
	const int XButtonWidth = 100;
	const int XButtonHeight = 20;

	internal Vector2 _scrollPos = Vector2.zero;
    const int Xoffset = 5;

	
	static Rect _StyleRect = new Rect(StartX, 45, Screen.width - 10, EditorGUIUtility.singleLineHeight);

	public static float EditorImageWidth = 80;
	public sealed override void OnInspectorGUI()
	{
//		GUILayout.FlexibleSpace();

		GUILayout.Box(XResources.LogoTexture, GUILayout.Width(Screen.width - Xoffset), GUILayout.Height(100));
//		GUILayout.FlexibleSpace();
//		GUILayout.Label(statusText, statusStyleForErrorMode, new GUILayoutOption[]
//					{
//						GUILayout.MaxWidth((float)(Screen.width - 52))
//					});
		base.OnInspectorGUI();
		OnXGUI();
	}

	public virtual void OnXGUI(){
		
	}

	public bool CreateSpaceButton(string btnName, float width = XButtonWidth, float height = XButtonHeight){
		return GUILayout.Button(btnName, GUILayout.Height(height) );
		//		return GUILayout.Button (btnName, EditorStyles.miniButtonMid, GUILayout.Width(50f));
	}
	public void DoButton(string btnName, Action callback)
    {
        if (GUILayout.Button(btnName, GUILayout.Height(XButtonHeight)))
        {
            callback();
        }
    }
	public void DoButton(GUIContent content, Action callback, params GUILayoutOption[] options)
	{
		if (GUILayout.Button(content, options))
		{
			callback();
		}
	}
    public void DoButton<T>(string btnName, Action<T> callback, T arg)
    {
        if (GUILayout.Button(btnName, GUILayout.Height(XButtonHeight)))
        {
            callback(arg);
        }
    }
	
	public virtual void Init(){
		CurrHeight = StartY;
	}
	
	public float CreateGUIFloat(string fieldName, float value){
		
//		CurrHeight += EditorGUIUtility.singleLineHeight;
		
		return EditorGUI.FloatField(CreateRect(), fieldName, value );
	}
	
	public int CreateGUIInt(string fieldName, int value){

        return EditorGUI.IntField( CreateRect(), fieldName, value );
	}


	public bool CreateCheckBox(bool value)
    {
        return EditorGUILayout.Toggle(value);
    }

	public Rect CreateRect(float scaleOffsetY = 1, float offsetX = 10){
		CurrHeight += EditorGUIUtility.singleLineHeight * scaleOffsetY + FieldOffset;
		return new Rect(offsetX, CurrHeight, Screen.width - 2 * offsetX, EditorGUIUtility.singleLineHeight * scaleOffsetY );
	}
	
	public float CreateFloatField(float value){
		return EditorGUILayout.FloatField(value);
	}
	public float CreateFloatField(string fieldName, float value){
		return EditorGUILayout.FloatField(fieldName, value);
	}

    public Object CreateObjectField( string fieldName, Object obj, System.Type type = null )
    {
        if( null == type ) type = typeof( Object );
        return EditorGUILayout.ObjectField( fieldName, obj, type, true ) as Object;
    }
	public Object CreateObjectField( Object obj, System.Type type = null )
    {
        if( null == type ) type = typeof( Object );
        return EditorGUILayout.ObjectField( obj, type, true ) as Object;
    }

    public void CreateSpaceBox(float w, float h)
    {
//        GUILayout.Box( "", GUILayout.Width(w), GUILayout.Height(h) );
    }
    public void CreateSpaceBox(float h = 3 )
    {
        //GUILayout.Box(new GUIContent(""), Screen.width, GUILayout.Height( 3 ) );
        //GUILayout.Box( "", GUILayout.Width( Screen.width ), h );
//        CreateSpaceBox( 1, h );

    }
	
	public int CreateIntField(string fieldName, int value){
		return EditorGUILayout.IntField(fieldName, value);
	}
	public int CreateIntField(int value){
		return EditorGUILayout.IntField(value);
	}
	
	public string CreateStringField(string fieldName, string value){
		return EditorGUILayout.TextField(fieldName, value);
	}
	public string CreateStringField(string value){
		return EditorGUILayout.TextField(value);
	}
	
	public void CreateLabel(string fieldName){
		EditorGUILayout.LabelField(fieldName);
	}
	
	public void CreateMessageField(string value, MessageType type){
		EditorGUILayout.HelpBox(value,type);
		
	}
	
	public System.Enum CreateEnumSelectable(string fieldName, System.Enum value){
		return EditorGUILayout.EnumPopup(fieldName, value);
	}
	
	public int CreateSelectableFromString(int rootID, string[] array){
		return EditorGUILayout.Popup(array[rootID], rootID, array);
	}

    public void BeginHorizontal()
    {
        EditorGUILayout.BeginHorizontal();
    }
    public void EndHorizontal()
    {
        EditorGUILayout.EndHorizontal();
    }
    public void BeginVertical()
    {
        EditorGUILayout.BeginVertical();
    }
    public void EndVertical()
    {
        EditorGUILayout.EndVertical();
    }
}
