using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using wuxingogo.Editor;

/**
 * [XBaseWindow 基础类]
 * @type {[◑▂◑]}
 */


public class XBaseWindow : EditorWindow, IHasCustomMenu
{

	protected Vector2 _scrollPos = Vector2.zero;
	const int Xoffset = 5;
	const int XButtonWidth = 100;
	const int XButtonHeight = 20;

	public static T InitWindow<T>(params object[] args) where T : XBaseWindow
	{
		string cmdPrefs = typeof( T ).ToString() + "_isPrefix";
		bool isPrefix = EditorPrefs.GetBool( cmdPrefs, false );
		T window = EditorWindow.GetWindow<T>( isPrefix, typeof( T ).Name );
		window.OnInitialization(args);
		return window;
	}

	public virtual void OnInitialization(params object[] args)
	{
	}

	public static GUISkin Skin{
		get{
			return EditorGUIUtility.isProSkin ? XStyles.GetInstance().scene : XStyles.GetInstance ().inspector;
		}
	}

	public static void DrawLogo(GUILayoutOption widthLayout)
	{
		GUILayout.Box( XResources.LogoTexture, widthLayout, GUILayout.Height( 100 ) );
	}


	public void OnGUI()
	{
		DrawLogo( GUILayout.ExpandWidth( true ) );
		if( GUI.Button( GUILayoutUtility.GetLastRect(), XResources.LogoTexture ) ) {
			this.Close();
			string cmdPrefs = GetType().ToString() + "_isPrefix";
			bool isPrefix = EditorPrefs.GetBool( cmdPrefs, false );
			EditorPrefs.SetBool( cmdPrefs, !isPrefix );
			XBaseWindow window = EditorWindow.GetWindow( GetType(), !isPrefix, GetType().Name, true ) as XBaseWindow;
			window.OnInitialization( closeRecordArgs );
			return;

		}
		if( IsAutoScroll )
			_scrollPos = EditorGUILayout.BeginScrollView( _scrollPos );

		OnXGUI();

		if( IsAutoScroll )
			EditorGUILayout.EndScrollView();
	}

	public virtual bool IsAutoScroll {
		get {
			return true;
		}
	}

	public virtual object[] closeRecordArgs {
		get;
		set;
	}

	public virtual void OnXGUI()
	{
    
	}

	public void CreateSpaceBox()
	{
		GUILayout.Box( "", GUILayout.Width( this.position.width - Xoffset ), GUILayout.Height( 3 ) );
	}

	public static bool CreateSpaceButton(string btnName, params GUILayoutOption[] option)
	{
		return GUILayout.Button( btnName, Skin.button, option);
	}

	public static void DoButton(string btnName, Action callback, params GUILayoutOption[] options )
	{
		if (GUILayout.Button (btnName, Skin.button, options))
			callback ();
	}

	public static void DoButton(string btnName, Action callback, GUIStyle style, params GUILayoutOption[] options )
	{
		if( GUILayout.Button( btnName, style, options ) ) {
			callback();
		}
	}

	public static void DoButton(GUIContent content, Action callback, params GUILayoutOption[] options)
	{
		if( GUILayout.Button( content, Skin.button, options ) ) {
			callback();
		}
	}

	public static void DoButton<T>(string btnName, Action<T> callback, T arg)
	{
		if( GUILayout.Button( btnName, Skin.button, GUILayout.ExpandWidth( true ) ) ) {
			callback( arg );
		}
	}

	public static void DoButton<T, T1>(string btnName, Action<T, T1> callback, T arg, T1 arg1)
	{
		if( GUILayout.Button( btnName, Skin.button, GUILayout.ExpandWidth( true ) ) ) {
			callback( arg, arg1 );
		}
	}

	public static Object CreateObjectField(string fieldName, Object obj, System.Type type = null, params GUILayoutOption[] options)
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( fieldName, obj, type, true, options ) as Object;
	}

	public static T CreateObjectField<T>(string fieldName, Object obj, params GUILayoutOption[] options) where T : Object
	{
		return EditorGUILayout.ObjectField( fieldName, obj, typeof(T), true, options ) as T;
	}

	public static Object CreateObjectField(Object obj, System.Type type = null, params GUILayoutOption[] options )
	{
		if( null == type )
			type = typeof( Object );
		return EditorGUILayout.ObjectField( obj, type, true, options ) as Object;
	}

	public static bool CreateCheckBox(string fieldName, bool value)
	{
		return EditorGUILayout.Toggle( fieldName, value );
	}

	public static bool CreateCheckBox(bool value, params GUILayoutOption[] options )
	{
		return EditorGUILayout.Toggle( value, options );
	}

    public static float CreateSlider( float value, float leftValue, float rightValue, params GUILayoutOption[] options )
    {
        return EditorGUILayout.Slider( value, leftValue, rightValue, options );
    }

    public static float CreateFloatField(string fieldName, float value)
	{
		return EditorGUILayout.FloatField( fieldName, value );
	}


    public static float CreateFloatField( float value, params GUILayoutOption[] options )
    {
		return EditorGUILayout.FloatField( value, options );
	}

	public static int CreateIntField(int value, params GUILayoutOption[] options )
	{
		return EditorGUILayout.IntField( value, options );
	}

	public static int CreateIntField(string fieldName, int value)
	{
		return EditorGUILayout.IntField( fieldName, value );
	}

	public static string CreateStringField(string fieldName, string value)
	{
		return EditorGUILayout.TextField( fieldName, value );
	}

	public static string CreateStringField(string value, params GUILayoutOption[] options )
	{
		return EditorGUILayout.TextField( value, options);
	}
    public static string CreateTextArea( string value, params GUILayoutOption[] options )
    {
        return EditorGUILayout.TextArea( value, options );
    }

    public static void CreateLabel(string fieldName, bool canSelect = false, params GUILayoutOption[] options )
    {
		if( canSelect )
			EditorGUILayout.SelectableLabel( fieldName, options );
		else
			EditorGUILayout.LabelField( fieldName, options );
	}

	public static void CreateLabel(string fieldName, string value, bool canSelect = false)
	{
		if( canSelect )
			EditorGUILayout.SelectableLabel( fieldName, value );
		else
			EditorGUILayout.LabelField( fieldName, value );
	}

	public static void PropertyFiled(SerializedProperty property, params GUILayoutOption[] options)
	{
		EditorGUILayout.PropertyField (property, options);
	}

	public static List<SerializedProperty> GetAllProperty(SerializedObject @object)
	{
		List<SerializedProperty> properties = new List<SerializedProperty> ();
		var prop = @object.GetIterator();
		while (prop.Next(true)) {
			properties.Add(prop);
		}
		return properties;
	}

	public static void CreateMessageField(string value, MessageType type)
	{
		EditorGUILayout.HelpBox( value, type );
	}

	public static System.Enum CreateEnumSelectable(System.Enum value)
	{
		return EditorGUILayout.EnumPopup( value );
	}

	public static System.Enum CreateEnumSelectable(string fieldName, System.Enum value, params GUILayoutOption[] options )
	{
		return EditorGUILayout.EnumPopup( fieldName, value, options );
	}

	public static System.Enum CreateEnumPopup(string fieldName, System.Enum value)
	{
		return EditorGUILayout.EnumMaskField( fieldName, value );
	}

	public static System.Enum CreateEnumPopup(System.Enum value)
	{
		return EditorGUILayout.EnumMaskField( value );
	}

	public static int CreateSelectableFromString(int rootID, string[] array, params GUILayoutOption[] option )
	{
		return EditorGUILayout.Popup( array[rootID], rootID, array, option );
	}

	public static int CreateSelectableString(int rootID, string[] array, params GUILayoutOption[] option )
	{
		return EditorGUILayout.Popup( rootID, array, option );
	}

    public static T SelectableString<T>( T defaultContent, T[] array, Action<T> onChange, params GUILayoutOption[] option )
        where T : class
    {
        string[] content = new string[array.Length];
        for( int i = 0; i < array.Length; i++ )
        {
            content[i] = array[i].ToString();
        }
        int selectedIndex = -1;
        for( int i = 0; i < array.Length; i++ )
        {
            if( defaultContent == array[i] )
            {
                selectedIndex = i;
            }
        }
        if( selectedIndex == -1 )
        {
            XLogger.Log( defaultContent.ToString() );
            return default( T );
        }

        var index = EditorGUILayout.Popup( selectedIndex, content, option );

        return array[index];
    }

	public static void BeginHorizontal()
	{
		EditorGUILayout.BeginHorizontal();
	}

	public static void EndHorizontal()
	{
		EditorGUILayout.EndHorizontal();
	}

	public static void BeginVertical()
	{
		EditorGUILayout.BeginVertical();
	}

	public static void EndVertical()
	{
		EditorGUILayout.EndVertical();
	}

    public Vector2 CreateVector2Field(string fieldName, Vector2 value, params GUILayoutOption[] options )
    {
        return EditorGUILayout.Vector2Field(fieldName, value, options );
    }

    public Vector3 CreateVector3Field(string fieldName, Vector3 value, params GUILayoutOption[] options )
    {
        return EditorGUILayout.Vector3Field(fieldName, value, options );
    }

    public Vector4 CreateVector4Field(string fieldName, Vector4 value, params GUILayoutOption[] options )
    {
        return EditorGUILayout.Vector4Field(fieldName, value, options );
    }

    public long CreateLongField(long value, params GUILayoutOption[] options )
    {
        return EditorGUILayout.LongField(value, options);
    }
	public static Color CreateColorField(string fieldName, Color value, params GUILayoutOption[] options)
	{
		return EditorGUILayout.ColorField (fieldName, value, options);
	}

    public static void DisableFragment(bool isDisable, Action action)
	{
		EditorGUI.BeginDisabledGroup( true );
		action();
		EditorGUI.EndDisabledGroup();
	}

	public void CreateNotification(string message)
	{
		ShowNotification( new GUIContent( message ) );
	}

	public virtual void AddItemsToMenu(GenericMenu menu)
	{
		menu.AddItem( new GUIContent( "Open This Editor Script" ), false, OpenEditorScript, "FuckThisWindow" );
		menu.AddItem( new GUIContent( "Select This Window" ), false, OpenEditorScript, "SelectThisWindow");
		menu.ShowAsContext();
	}

	void OpenEditorScript(object handle)
	{
		string para = (string)handle;
		switch (para)
		{
			case "FuckThisWindow":
				{
                    OpenTypeScript( GetType() );

                }
				break;
			case "SelectThisWindow":
				{
					Selection.objects = new Object[1] { this };
				}
				break;
		}


	}
	[MenuItem("Wuxingogo/Select FocusedWindow")]
	static void SelectWindow()
	{
		Selection.objects = new Object[]{ InspectorUtilites.GetInspectorWindow() };
//		Selection.objects = ActiveEditorTracker.sharedTracker.activeEditors;
//		for (int i = 0; i < ActiveEditorTracker.sharedTracker.activeEditors.Length; i++) {
//			XLogger.Log (ActiveEditorTracker.sharedTracker.activeEditors [i].GetType ().Name);
//		}
		
	}
    public static void OpenTypeScript(Type type)
    {
        string fileName = type.Name;
        string absolutelyPath = OpenScript( fileName, "Assets" );

        Object[] obj = AssetDatabase.LoadAllAssetsAtPath( absolutelyPath );
        AssetDatabase.OpenAsset( obj );
    }
	public static string OpenScript(string filename, string path)
	{
		if( Directory.Exists( path ) ) {
			if( File.Exists( path + "/" + filename + ".cs" ) )
				return path + "/" + filename + ".cs";
			string[] directorys = Directory.GetDirectories( path );
			foreach( string d in directorys ) {
				string str = d.Replace( '\\', '/' );
				String p = OpenScript( filename, str );
				if( p != null )
					return p;
			}
		}
		return null;
	}
}

