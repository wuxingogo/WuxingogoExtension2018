using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


public class XStyles : Editor
{
	public GUISkin skin = null;
	[MenuItem( "Assets/Create/Wuxingogo/GUI Style", false, 100 )]
	public static void CreateStyleAsset()
	{
		string path = EditorUtility.SaveFilePanel( "Create A XData", Application.dataPath + @"/WuxingogoExtension", "Default.asset", "asset" );
		if( path == "" )
			return;

		path = FileUtil.GetProjectRelativePath( path );

		GUISkin data = CreateInstance<GUISkin>();
		AssetDatabase.CreateAsset( data, path );
		AssetDatabase.SaveAssets();
	}

	private static GUISkin style = null;

	public static GUISkin GetInstance()
	{
		if( style == null )
			style = AssetDatabase.LoadAssetAtPath<GUISkin>( FileUtil.GetProjectRelativePath( XEditorSetting.ProjectPath + "/Templates/XGUIStyle.guiskin" ) );
		return style;
	}
}

