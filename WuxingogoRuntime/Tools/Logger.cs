using UnityEngine;
public class Logger
{
	static public bool EnableLog = true;
    static public void Log( object message )
    {
        if( EnableLog )
        {
            Debug.Log( message );
        }
    }
    static public void Log( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.Log( message, context );
        }
    }
    static public void LogFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogFormat( message, args );
        }
    }
    static public void LogError( object message )
    {
        if( EnableLog )
        {
            Debug.LogError( message );
        }
    }
    static public void LogError( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.LogError( message, context );
        }
    }
    static public void LogWarning( object message )
    {
        if( EnableLog )
        {
            Debug.LogWarning( message );
        }
    }
    static public void LogWarning( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.LogWarning( message, context );
        }
    }

    static public void LogWarningFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogWarningFormat( message, args );
        }
    }

}

