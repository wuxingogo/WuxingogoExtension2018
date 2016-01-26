using UnityEngine;
using System.Collections;
using UnityEditor;

/**
 * [XBaseWindow 基础类]
 * @type {[◑▂◑]}
 */
using System;
using Object = UnityEngine.Object;
using System.IO;

public class XBaseWindow : EditorWindow, IHasCustomMenu
{

    internal Vector2 _scrollPos = Vector2.zero;
    const int Xoffset = 5;
    const int XButtonWidth = 100;
    const int XButtonHeight = 20;

	public static T InitWindow<T>() where T : XBaseWindow
    {
        string cmdPrefs = typeof(T).ToString() + "_isPrefix";
        bool isPrefix = EditorPrefs.GetBool(cmdPrefs, false);
		T window = EditorWindow.GetWindow<T>(isPrefix, typeof(T).Name);
		window.OnInitialization();
		return window;
    }

    public virtual void OnInitialization(params object[] args){}

    public void OnGUI()
    {
		GUILayout.Box(XResources.LogoTexture, GUILayout.Width(this.position.width - Xoffset), GUILayout.Height(100));
		if (GUI.Button(GUILayoutUtility.GetLastRect(), XResources.LogoTexture))
        {
        	this.Close();
            string cmdPrefs = GetType().ToString() + "_isPrefix";
            bool isPrefix = EditorPrefs.GetBool(cmdPrefs, false);
            EditorPrefs.SetBool(cmdPrefs, !isPrefix);
			XBaseWindow window = EditorWindow.GetWindow(GetType(), !isPrefix, GetType().Name, true) as XBaseWindow;
			window.OnInitialization(closeRecordArgs);
			return;

        }
		if(IsAutoScroll)
        	_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        OnXGUI();

		if(IsAutoScroll)
        	EditorGUILayout.EndScrollView();
    }

    public virtual bool IsAutoScroll {
		get{
			return true;
		}
    }

    public virtual object[] closeRecordArgs{
    	get;
    	set;
    }

    public virtual void OnXGUI(){
    
    }

    public void CreateSpaceBox()
    {
        GUILayout.Box("", GUILayout.Width(this.position.width - Xoffset), GUILayout.Height(3));
    }

    public bool CreateSpaceButton(string btnName, float width = XButtonWidth)
    {
        return GUILayout.Button(btnName, GUILayout.ExpandWidth(true));
    }
    public void DoButton(string btnName, Action callback)
    {
        if (GUILayout.Button(btnName, GUILayout.ExpandWidth(true)))
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
        if (GUILayout.Button(btnName, GUILayout.ExpandWidth(true)))
        {
            callback(arg);
        }
    }

	public void DoButton<T, T1>(string btnName, Action<T, T1> callback, T arg, T1 arg1)
    {
        if (GUILayout.Button(btnName, GUILayout.ExpandWidth(true)))
        {
            callback(arg, arg1);
        }
    }

	public Object CreateObjectField(string fieldName, Object obj, System.Type type = null, params GUILayoutOption[] options)
    {
        if (null == type) type = typeof(Object);
		return EditorGUILayout.ObjectField(fieldName, obj, type, true, options) as Object;
    }
    
	public Object CreateObjectField(Object obj, System.Type type = null)
	{
		if (null == type) type = typeof(Object);
		return EditorGUILayout.ObjectField(obj, type, true) as Object;
	}

    public bool CreateCheckBox(string fieldName, bool value)
    {
        return EditorGUILayout.Toggle(fieldName, value);
    }
    public bool CreateCheckBox(bool value)
    {
        return EditorGUILayout.Toggle(value);
    }

    public float CreateFloatField(string fieldName, float value)
    {
        return EditorGUILayout.FloatField(fieldName, value);
    }
    public float CreateFloatField(float value)
    {
        return EditorGUILayout.FloatField(value);
    }
	public int CreateIntField(int value)
	{
		return EditorGUILayout.IntField(value);
	}

    public int CreateIntField(string fieldName, int value)
    {
        return EditorGUILayout.IntField(fieldName, value);
    }

    public string CreateStringField(string fieldName, string value)
    {
        return EditorGUILayout.TextField(fieldName, value);
    }
    public string CreateStringField(string value)
    {
        return EditorGUILayout.TextField(value);
    }

    public void CreateLabel(string fieldName)
    {
        EditorGUILayout.LabelField(fieldName);
    }
    public void CreateLabel(string fieldName, string value)
    {
        EditorGUILayout.LabelField(fieldName, value);
    }

    public void CreateMessageField(string value, MessageType type)
    {
        EditorGUILayout.HelpBox(value, type);

    }
	public System.Enum CreateEnumSelectable(System.Enum value)
    {
        return EditorGUILayout.EnumPopup(value);
    }
    public System.Enum CreateEnumSelectable(string fieldName, System.Enum value)
    {
        return EditorGUILayout.EnumPopup(fieldName, value);
    }
    public System.Enum CreateEnumPopup(string fieldName, System.Enum value)
    {
        return EditorGUILayout.EnumMaskField(fieldName, value);
    }
	public System.Enum CreateEnumPopup(System.Enum value)
    {
        return EditorGUILayout.EnumMaskField(value);
    }

    public int CreateSelectableFromString(int rootID, string[] array)
    {
        return EditorGUILayout.Popup(array[rootID], rootID, array);
    }
    public int CreateSelectableString(int rootID, string[] array)
    {
        return EditorGUILayout.Popup(rootID, array);
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

    public void CreateNotification(string message)
    {
        ShowNotification(new GUIContent(message));
    }

    public virtual void AddItemsToMenu(GenericMenu menu)
    {
        //menu.AddItem(new GUIContent("asdfasd"), false, NoneCallback, "aaaa");
        menu.AddItem(new GUIContent("OpenEditorScript"), false, OpenEditorScript, "FuckThisWindow");
        menu.ShowAsContext();

    }

    void OpenEditorScript(object handle)
    {
        string type = this.GetType().Name;
		string absolutelyPath = FindFile(type, "Assets");

		Object[] obj = AssetDatabase.LoadAllAssetsAtPath(absolutelyPath);
		AssetDatabase.OpenAsset(obj);

    }

    static String FindFile(String filename, String path)
    {
        if (Directory.Exists(path))
        {
            if (File.Exists(path + "/" + filename + ".cs"))
                return path + "/" + filename + ".cs";
            String[] directorys = Directory.GetDirectories(path);
            foreach (String d in directorys)
            {
                string str = d.Replace('\\', '/');
                String p = FindFile(filename, str);
                if (p != null)
                    return p;
            }
        }
        return null;
    }
}

public enum XEditorEnum{
	cehua,
	programmer,
	art
}
