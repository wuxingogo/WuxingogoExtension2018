//
// SingletonClass.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2016 ly-user
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
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using wuxingogo.Runtime;

namespace wuxingogo.tools
{
	/// <summary>
	/// Singleton Component.
	/// </summary>
	public class SingletonC<T> : XMonoBehaviour where T : Component
    {
	    static T mInstance = null;

	    public static T Inst { 
		    get {
			    if (mInstance == null) {
				    mInstance = FindObjectOfType<T> ();
				    if (mInstance == null) {
					    GameObject obj = new GameObject (typeof(T).Name);
					    mInstance = obj.AddComponent<T>();
				    }
			    }
			    return mInstance;
		    }
		    set { mInstance = value; }
	    }

	    protected virtual void OnDestroy()
	    {
		    if(mInstance ==  this)
		    	mInstance = null;
	    }

	    protected virtual void OnAwake()
	    {
		    
	    }

	    protected virtual HideFlags gameObjectFlags
	    {
		    get { return HideFlags.DontSave; }
	    }

	    protected virtual bool isDontDestroy
	    {
		    get { return false; }
	    }
	    protected virtual void Awake()
	    {
		    if( mInstance != null )
		    {
			    XLogger.Log( "Found Singleton : " + mInstance.name );
			    Destroy( gameObject );
			    return;
		    }
		    else
		    {
			    mInstance = this as T;
			    if(isDontDestroy)
				    DontDestroyOnLoad( gameObject );
		    
			    gameObject.hideFlags = gameObjectFlags;

			    OnAwake();
		    }
		    
	    }
    }
}
