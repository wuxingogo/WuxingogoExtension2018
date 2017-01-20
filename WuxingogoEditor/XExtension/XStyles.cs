using UnityEngine;
using System.Collections.Generic;
using wuxingogo.Runtime;
using UnityEditor;
using wuxingogo.Editor;

public class XStyles : XEditorScriptableObject
{
	[MenuItem( "Assets/Create/Wuxingogo/GUI Style", false, 100 )]
	public static void CreateStyleAsset()
	{
		string path = EditorUtility.SaveFilePanel( "Create GUI Style", XEditorSetting.PluginPath, "Default.asset", "asset" );
		if( path == "" )
			return;

		path = FileUtil.GetProjectRelativePath( path );

		XStyles xstyle = CreateInstance<XStyles> ();
		GUISkin data = Instantiate (Selection.objects [0]) as GUISkin;
		xstyle.skin = data;
		AssetDatabase.CreateAsset( xstyle, path );
		AssetDatabase.AddObjectToAsset (data, path);
		AssetDatabase.SaveAssets();
	}

	private static XStyles styles = null;
	public GUISkin skin = null;

	public GUISkin game;
	public GUISkin inspector;
	public GUISkin scene;
	public List<GUISkin> customSkin = new List<GUISkin>();
	public static XStyles GetInstance()
	{
		if( styles == null )
			styles = AssetDatabase.LoadAssetAtPath<XStyles>( FileUtil.GetProjectRelativePath( XEditorSetting.PluginPath + "/Templates/XGUIStyle.asset" ) );
		return styles;
	}

	[X]
	public void InitBuildinStyle()
	{
		game = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Game);
		inspector = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Inspector);
		scene = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Scene);
	}
	[X]
	public void RemoveCustomSkin(string name)
	{
		for (int i = 0; i < customSkin.Count; i++) {
			if (customSkin [i].name == name) {
				DestroyImmediate (customSkin [i]);
				customSkin.RemoveAt (i);
				break;
			}
		}
	}

	public GUISkin GetCustomSkin(string name)
	{
		for (int i = 0; i < customSkin.Count; i++) {
			if (customSkin [i].name == name) {
				return customSkin [i];
			}
		}
		return null;
	}
	[X]
	public void AddGameSkinStyle()
	{
		var source = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Game);
		var skin = Instantiate (source);
		skin.name = source.name;
		AssetDatabase.AddObjectToAsset (skin, this);

		customSkin.Add (skin);
	}
	[X]
	public void AddInspectorSkinStyle()
	{
		var source = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Inspector);
		var skin = Instantiate (source);
		skin.name = source.name;
		AssetDatabase.AddObjectToAsset (skin, this);
		customSkin.Add (skin);
	}
	[X]
	public void AddSceneSkinStyle()
	{
		var source = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Scene);
		var skin = Instantiate (source);
		skin.name = source.name;
		AssetDatabase.AddObjectToAsset (skin, this);
		customSkin.Add (skin);
	}

	public GUIStyle FindStyle(string Name)
	{
		return skin.FindStyle( Name );
	}
}
