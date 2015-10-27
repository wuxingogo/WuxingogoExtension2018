using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


public class XStyles : ScriptableObject
{
    public List<Texture2D> icons = new List<Texture2D>();
    public List<GUIStyle> styles = new List<GUIStyle>();

    [MenuItem( "Assets/Create/Wuxingogo/GUI Style", false, 100 )]
    public static void CreateStyleAsset()
    {
        string path = EditorUtility.SaveFilePanel( "Create A XData", Application.dataPath + @"/WuxingogoExtension", "Default.asset", "asset" );
        if( path == "" )
            return;

        path = FileUtil.GetProjectRelativePath( path );

        XStyles data = CreateInstance<XStyles>();
        AssetDatabase.CreateAsset( data, path );
        AssetDatabase.SaveAssets();
    }
}

