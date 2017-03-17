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

	static GUILayoutOption widthOption = GUILayout.Width( Screen.width - 40 );
	public static EditorWindow ForcusWindow{
		get{
			if(forcusWindow == null)
				forcusWindow = InspectorUtilites.GetInspectorWindow();
			return forcusWindow;
		}
	}
	private static EditorWindow forcusWindow = null;
    public static void DrawLogo()
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

	public static bool CreateSpaceButton(string btnName)
	{
		return GUILayout.Button( btnName );
	}

	public static void DoButton(string btnName, Action callback)
	{
		if( GUILayout.Button( btnName ) ) {
			callback();
		}
	}

	public static void DoButton(GUIContent content, Action callback, params GUILayoutOption[] options)
	{
		if( GUILayout.Button( content, options ) ) {
			callback();
		}
	}

	public static void DoButton<T>(string btnName, Action<T> callback, T arg)
	{
		if( GUILayout.Button( btnName ) ) {
			callback( arg );
		}
	}

	public static void CreateSpaceBox()
	{
		GUILayout.Box( "", widthOption, GUILayout.Height( 3 ) );
	}

	public static void Space()
    {
        EditorGUILayout.Space();
    }

	public static bool CreateCheckBox(bool value)
	{
		return EditorGUILayout.Toggle( value );
	}
	public static bool CreateCheckBox( string title, bool value )
    {
        return EditorGUILayout.Toggle( title, value );
    }

	public static float CreateFloatField(float value, params GUILayoutOption[] option)
	{
		return EditorGUILayout.FloatField( value, option );
	}

	public static float CreateFloatField(string fieldName, float value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.FloatField( fieldName, value, option );
	}
	public static Vector2 CreateVector2Field(string fieldName, Vector2 value)
	{
		return EditorGUILayout.Vector2Field (fieldName, value);
	}


	public static Vector3 CreateVector3Field(string fieldName, Vector3 value)
	{
		return EditorGUILayout.Vector3Field (fieldName, value);
	}

	public static Vector4 CreateVector4Field(string fieldName, Vector4 value)
	{
		return EditorGUILayout.Vector4Field (fieldName, value);
	}

	public static Object CreateObjectField(string fieldName, Object obj, System.Type type = null)
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( fieldName, obj, type, true ) as Object;
	}

	public static Object CreateObjectField(Object obj, System.Type type = null)
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( obj, type, true ) as Object;
	}
	public static Color CreateColorField( Color value, params GUILayoutOption[] option )
    {
        return EditorGUILayout.ColorField( value, option );
    }
	public static Color CreateColorField( string label, Color value, params GUILayoutOption[] option )
    {
        return EditorGUILayout.ColorField( label, value, option );
    }

	public static int CreateIntField(string fieldName, int value,params GUILayoutOption[] option)
	{
		return EditorGUILayout.IntField( fieldName, value, option );
	}

	public static int CreateIntField(int value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.IntField( value, option );
	}

	public static long CreateLongField(string fieldName,long value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.LongField(fieldName, value, option );
	}

	public static long CreateLongField(long value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.LongField( value, option );
	}

	public static string CreateStringField(string fieldName, string value)
	{
		return EditorGUILayout.TextField( fieldName, value );
	}

	public string CreateStringField(string value, params GUILayoutOption[] option )
	{
		return EditorGUILayout.TextField( value );
	}

	public static void CreateLabel(string fieldName)
	{
		EditorGUILayout.LabelField( fieldName );
	}

	public static void CreateMessageField(string value, MessageType type)
	{
		EditorGUILayout.HelpBox( value, type );
		
	}

    public void CreatePropertyField( SerializedProperty property, params GUILayoutOption[] options )
    {
        EditorGUILayout.PropertyField( property, options );
    }

	public static Enum CreateEnumSelectable(string fieldName, Enum value)
	{
		return EditorGUILayout.EnumPopup( fieldName, value );
	}

	public static int CreateSelectableFromString(int rootID, string[] array)
	{
		return EditorGUILayout.Popup( array[rootID], rootID, array );
	}

	public static Rect CreateRectField(string label, Rect rect, params GUILayoutOption[] options )
    {
        return EditorGUILayout.RectField( label, rect, options );
    }

	public static void BeginHorizontal()
	{
		EditorGUILayout.BeginHorizontal( "AS TextArea" );
	}

	public static void EndHorizontal()
	{
		EditorGUILayout.EndHorizontal();
	}

	public static void BeginVertical()
	{
		EditorGUILayout.BeginVertical( "AS TextArea");
	}

	public static void EndVertical()
	{
		EditorGUILayout.EndVertical();
	}

    public void BeginGroup()
    {
        
    }
}
