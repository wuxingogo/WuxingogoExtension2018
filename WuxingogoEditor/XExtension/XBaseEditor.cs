using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using wuxingogo.Editor;

public class XBaseEditor : Editor
{

	public override void OnInspectorGUI()
	{
        DrawLogo();

        base.OnInspectorGUI();

        OnXGUI();
	}

    GUILayoutOption widthOption = GUILayout.Width( Screen.width - 40 );
	EditorWindow ForcusWindow{
		get{
			if(forcusWindow == null)
				forcusWindow = InspectorUtilites.GetInspectorWindow();
			return forcusWindow;
		}
	}
	private EditorWindow forcusWindow = null;
    public void DrawLogo()
    {
//		InspectorUtilites.GetAllInspector ();

		GUILayout.Box( XResources.LogoTexture, GUILayout.Width(ForcusWindow.position.width - 40) );
//		BeginHorizontal();
//		GUILayout.Box( XResources.LogoTexture);
//		EndHorizontal ();
    }

	public virtual void OnXGUI()
	{
        
    }

	public bool CreateSpaceButton(string btnName)
	{
		return GUILayout.Button( btnName );
	}

	public void DoButton(string btnName, Action callback)
	{
		if( GUILayout.Button( btnName ) ) {
			callback();
		}
	}

	public void DoButton(GUIContent content, Action callback, params GUILayoutOption[] options)
	{
		if( GUILayout.Button( content, options ) ) {
			callback();
		}
	}

	public void DoButton<T>(string btnName, Action<T> callback, T arg)
	{
		if( GUILayout.Button( btnName ) ) {
			callback( arg );
		}
	}

	public void CreateSpaceBox()
	{
		GUILayout.Box( "", widthOption, GUILayout.Height( 3 ) );
	}

    public void Space()
    {
        EditorGUILayout.Space();
    }

	public bool CreateCheckBox(bool value)
	{
		return EditorGUILayout.Toggle( value );
	}
    public bool CreateCheckBox( string title, bool value )
    {
        return EditorGUILayout.Toggle( title, value );
    }

    public float CreateFloatField(float value, params GUILayoutOption[] option)
	{
		return EditorGUILayout.FloatField( value, option );
	}

	public float CreateFloatField(string fieldName, float value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.FloatField( fieldName, value, option );
	}
	public Vector2 CreateVector2Field(string fieldName, Vector2 value)
	{
		return EditorGUILayout.Vector2Field (fieldName, value);
	}


	public Vector3 CreateVector3Field(string fieldName, Vector3 value)
	{
		return EditorGUILayout.Vector3Field (fieldName, value);
	}

	public Vector4 CreateVector4Field(string fieldName, Vector4 value)
	{
		return EditorGUILayout.Vector4Field (fieldName, value);
	}

	public Object CreateObjectField(string fieldName, Object obj, System.Type type = null)
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( fieldName, obj, type, true ) as Object;
	}

	public Object CreateObjectField(Object obj, System.Type type = null)
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( obj, type, true ) as Object;
	}
    public Color CreateColorField( Color value, params GUILayoutOption[] option )
    {
        return EditorGUILayout.ColorField( value, option );
    }
    public Color CreateColorField( string label, Color value, params GUILayoutOption[] option )
    {
        return EditorGUILayout.ColorField( label, value, option );
    }

	public int CreateIntField(string fieldName, int value,params GUILayoutOption[] option)
	{
		return EditorGUILayout.IntField( fieldName, value, option );
	}

	public int CreateIntField(int value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.IntField( value, option );
	}

	public long CreateLongField(string fieldName,long value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.LongField(fieldName, value, option );
	}

	public long CreateLongField(long value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.LongField( value, option );
	}

	public string CreateStringField(string fieldName, string value)
	{
		return EditorGUILayout.TextField( fieldName, value );
	}

	public string CreateStringField(string value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.TextField( value );
	}

	public void CreateLabel(string fieldName)
	{
		EditorGUILayout.LabelField( fieldName );
	}

	public void CreateMessageField(string value, MessageType type)
	{
		EditorGUILayout.HelpBox( value, type );
		
	}

    public void CreatePropertyField( SerializedProperty property, params GUILayoutOption[] options )
    {
        EditorGUILayout.PropertyField( property, options );
    }

	public Enum CreateEnumSelectable(string fieldName, Enum value)
	{
		return EditorGUILayout.EnumPopup( fieldName, value );
	}

	public int CreateSelectableFromString(int rootID, string[] array)
	{
		return EditorGUILayout.Popup( array[rootID], rootID, array );
	}

    public Rect CreateRectField(string label, Rect rect, params GUILayoutOption[] options )
    {
        return EditorGUILayout.RectField( label, rect, options );
    }

    public void BeginHorizontal()
	{
		EditorGUILayout.BeginHorizontal( "AS TextArea" );
	}

	public void EndHorizontal()
	{
		EditorGUILayout.EndHorizontal();
	}

	public void BeginVertical()
	{
		EditorGUILayout.BeginVertical( "AS TextArea");
	}

	public void EndVertical()
	{
		EditorGUILayout.EndVertical();
	}

    public void BeginGroup()
    {
        
    }
}
