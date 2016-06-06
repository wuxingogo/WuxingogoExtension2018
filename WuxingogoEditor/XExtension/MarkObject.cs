using System.Collections.Generic;
using UnityEngine;

public class MarkObject : ScriptableObject
{
    static MarkObject _instance = null;
    public static MarkObject GetInstance()
    {
        var path = XAssetLoader.GetRelativePath( XEditorSetting.TemplatesPath + "/FavoriteObj.asset" );
        if( _instance == null )
        {
            _instance = XAssetLoader.LoadAssetFromProject<MarkObject>( path );
        }
        if( _instance == null )
        {
            _instance = ScriptableObject.CreateInstance<MarkObject>();
            Logger.Log( "Create MarkObj" );
            XAssetLoader.CreateAsset( _instance, path );
        }
        return _instance;
    }

    public List<string> pathSet = new List<string>();
    public List<Object> objSet = new List<Object>();

    public void AddEntry( Object asset )
    {

        var path = XAssetLoader.GetAssetPath( asset );
        if( !string.IsNullOrEmpty( path ) )
        {
            pathSet.Add( path );
            objSet.Add( asset );
            XAssetLoader.SetDirty( this );
        }
    }

    public void RemoveEntry( int index )
    {
        pathSet.RemoveAt( index );
        objSet.RemoveAt( index );
        XAssetLoader.SetDirty( this );
    }
}