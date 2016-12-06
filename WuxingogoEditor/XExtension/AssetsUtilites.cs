using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class AssetsUtilites
{
    public static T[] FindAssetsByType<T>() where T : Object
    {

        var totalAssetGuid = AssetDatabase.FindAssets( string.Format( "t:{0}", typeof( T ) ) );
        List<T> totalAssets = new List<T>();
        for( int i = 0; i < totalAssetGuid.Length; i++ )
        {
            var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
            var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
            for( int j = 0; j < allAssets.Length; j++ )
            {
                if( allAssets[j] is T && !totalAssets.Contains(allAssets[j] as T))
                    totalAssets.Add( allAssets[j] as T );
            }
        }
        return totalAssets.ToArray();
    }

    public static T[] FindAssetsByTags<T>( string tag ) where T : Object
    {
        var totalAssetGuid = AssetDatabase.FindAssets( string.Format( "l:{0}", tag ) );
        List<T> totalAssets = new List<T>();
        for( int i = 0; i < totalAssetGuid.Length; i++ )
        {
            var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
            var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
            for( int j = 0; j < allAssets.Length; j++ )
            {
                if( allAssets[j] is T && !totalAssets.Contains( allAssets[j] as T ) )
                    totalAssets.Add( allAssets[j] as T );
            }
        }
        return totalAssets.ToArray();
    }

    public static T[] FindGameObjectsByComponents<T>(params string[] searchInFolders) where T : Component
    {
        var totalAssetGuid = AssetDatabase.FindAssets( "t:GameObject", searchInFolders );
        List<T> totalAssets = new List<T>();
        for( int i = 0; i < totalAssetGuid.Length; i++ )
        {
            var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
            var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
            for( int j = 0; j < allAssets.Length; j++ )
            {
                if( allAssets[j] is GameObject )
                {
                    var tGameObject = allAssets[j] as GameObject;
                    var t = tGameObject.GetComponent<T>();
                    if( t != null )
                        totalAssets.Add( t );
                }
            }
        }
        return totalAssets.ToArray();
    }
}

