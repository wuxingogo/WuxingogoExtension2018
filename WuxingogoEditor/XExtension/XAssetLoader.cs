
using UnityEditor;
using UnityEngine;
public class XAssetLoader
{
    public static T LoadAssetFromProject<T>( string path) where T : Object
    {
        string assetPath = FileUtil.GetProjectRelativePath( path );
        return AssetDatabase.LoadAssetAtPath<T>( assetPath );
    }

    public static string GetRelativePath( string path )
    {
        return FileUtil.GetProjectRelativePath( path );
    }

    public static string GetAssetPath( Object asset )
    {
        return AssetDatabase.GetAssetPath( asset );
    }

    public static void CreateAsset( Object obj, string path )
    {
        AssetDatabase.CreateAsset( obj, path );
    }

    public static void SetDirty( Object asset )
    {
        EditorUtility.SetDirty( asset );
    }


}

