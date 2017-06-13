//
// XLogger.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2015 ly-user
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using Conditional = System.Diagnostics.ConditionalAttribute;

public class XLogger
{
	public const string PLATFORM = "XLOG_ENABLE";
    static public bool EnableLog = true;
	[Conditional(PLATFORM)]
    static public void Log( object message )
    {
        if( EnableLog )
        {
            Debug.Log( message );
        }
    }
	[Conditional(PLATFORM)]
    static public void Log( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.Log( message, context );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogFormat( context, message, args );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogFormat( message, args );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogError( object message )
    {
        if( EnableLog )
        {
            Debug.LogError( message );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogErrorFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogErrorFormat( context, message, args );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogError( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.LogError( message, context );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogWarning( object message )
    {
        if( EnableLog )
        {
            Debug.LogWarning( message );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogWarning( object message, Object context )
    {
        if( EnableLog )
        {
            Debug.LogWarning( message, context );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogWarningFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogWarningFormat( message, args );
        }
    }
	[Conditional(PLATFORM)]
    static public void LogWarningFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            Debug.LogWarningFormat( context, message, args );
        }
    }
	[Conditional(PLATFORM)]
	static public void Break()
	{
		if( EnableLog )
		{
			Debug.Log( "XLogger Break ");
			Debug.Break();
		}
	}

}

