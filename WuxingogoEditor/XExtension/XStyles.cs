using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
		if (styles == null) {
			var path = FileUtil.GetProjectRelativePath (XEditorSetting.PluginPath + "/Templates/XGUIStyle.asset");
			XLogger.Log (path);
			
			styles = AssetDatabase.LoadAssetAtPath<XStyles> (path);
			XLogger.Log (styles == null);
		}
		return styles;
	}
	[MenuItem("Wuxingogo/Reimport Style")]
	[X]
	public static void InitBuildinStyle()
	{
		var instance = GetInstance();
		instance.game = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Game);
		instance.inspector = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Inspector);
		instance.scene = EditorGUIUtility.GetBuiltinSkin (EditorSkin.Scene);
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
		var style = scene.FindStyle( Name );
		if( style == null )
		{
			style = new GUIStyle();
			style.name = Name;
			var styles = skin.customStyles.ToList();
			styles.Add( style );
			scene.customStyles = styles.ToArray();
			EditorUtility.SetDirty( scene );
		}
		return style;
	}
}
