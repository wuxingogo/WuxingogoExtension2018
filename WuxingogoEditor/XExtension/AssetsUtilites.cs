using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AssetsUtilites
{
    public static T[] FindAssets<T>() where T : Object
    {

        var totalAssetGuid = AssetDatabase.FindAssets( string.Format( "t:{0}", typeof( T ) ) );
        T[] totalAssets = new T[totalAssetGuid.Length];
        for( int i = 0; i < totalAssetGuid.Length; i++ )
        {
            var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
            totalAssets[i] = AssetDatabase.LoadAssetAtPath<T>( path );
        }
        return totalAssets;
    }
}

