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

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Text;
using UnityEngine;
using wuxingogo.Runtime;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class XLogger
{
    /// <summary>
    /// enable in "Player Setting" Scripting Define Symbols
    /// </summary>
	public const string PREDEFINE = "XLOG_ENABLE";

    public const string LOG_TO_FILE = "XLOG_WRITE_FILE";
    static public bool EnableLog = true;
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void Log( object message )
    {
        if( EnableLog )
        {
            LogToFile(message);
			Debug.Log( message );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void Log( object message, Object context )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.Log( message, context );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogFormat( context, message, args );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogFormat( message, args );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogError( object message )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogError( message );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogErrorFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(string.Format(message, args));
            Debug.LogErrorFormat( context, message, args );
        }
    }
    
    [System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogErrorFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(string.Format(message, args));
            Debug.LogErrorFormat( message, args );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogError( object message, Object context )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogError( message, context );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogWarning( object message )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogWarning( message );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogWarning( object message, Object context )
    {
        if( EnableLog )
        {
            LogToFile(message);
            Debug.LogWarning( message, context );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogWarningFormat( string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(string.Format(message, args));
            Debug.LogWarningFormat( message, args );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
    static public void LogWarningFormat( Object context, string message, params object[] args )
    {
        if( EnableLog )
        {
            LogToFile(string.Format(message, args));
            Debug.LogWarningFormat( context, message, args );
        }
    }
	[System.Diagnostics.Conditional(PREDEFINE)]
	static public void Break()
	{
		if( EnableLog )
        {
            LogToFile("XLogger Break ");
			Debug.Log( "XLogger Break ");
		    Debug.Break();
		}
	}
    
    private static FileStream FileWriter;
    private static UTF8Encoding encoding;
    public static bool isLogToFile = false;
    [System.Diagnostics.Conditional(LOG_TO_FILE)]
    static public void LogToFile(object content)
    {
        //if(isLogToFile)
        //    ToFile(content);
    }
    
    public static void ToFile(object content)
    {
        
        var trace = new StackTrace(); //获取调用类信息
        var ClassName = trace.GetFrame(1).GetMethod().DeclaringType.Name;
        var WayName = trace.GetFrame(1).GetMethod().Name;
        var log = DateTime.Now + " " + "[" + ClassName + "." + WayName + "]" + " " + ":" + " " + content +
                  Environment.NewLine;

        if (FileWriter == null)
        {
            var nowTime = DateTime.Now.ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_");
            var fileInfo = new FileInfo(Application.persistentDataPath + nowTime + "_Log.txt");
            encoding = new UTF8Encoding();
            //设置Log文件输出地址
            FileWriter = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        }
        
        FileWriter.Write(encoding.GetBytes(log), 0, encoding.GetByteCount(log));
        FileWriter.Flush();
        /*
          --------------------- 
            作者：hchsen 
            来源：CSDN 
            原文：https://blog.csdn.net/u010989951/article/details/70918714 
        */
    }

}



