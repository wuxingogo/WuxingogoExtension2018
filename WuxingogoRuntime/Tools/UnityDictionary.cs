//
// UnityDictionary.cs
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

using UnityEngine;
using System.Collections.Generic;
using System;
using wuxingogo.Runtime;
using System.Collections;
using System.Linq;

[Serializable]
public class UnityDictionary<TKey,TValue> : XScriptableObject 
{
    public delegate void OnChange();

    public event OnChange OnChangeEvent;
	//[HideInInspector]
	public List<TKey> Keys = new List<TKey>();
	//[HideInInspector]
    public List<TValue> Values = new List<TValue>();

	protected Dictionary<TKey, TValue> totalDict = null;
	[X]
	public Dictionary<TKey, TValue> TotalDict
	{
		get
		{
			if (totalDict == null)
			{
				totalDict = new Dictionary<TKey, TValue>();
				for (int i = 0; i < Keys.Count; i++)
				{
					var key = Keys[i];
					var v = Values[i];
					totalDict.Add(key, v);
				}
			}
			return totalDict;
		}
	}

    public TValue this[TKey key]
    {
        get
        {
			return TotalDict[key];
		}set
		{
			if (Keys.Contains(key)){
				var index = Keys.IndexOf(key);
				Values[index] = value;
				TotalDict[key] = value;
			}
			else
			{
				Add(key, value);
			}
		}
    }

    [X]
    public UnityDictionary<TKey, TValue> Add( TKey key, TValue value )
    {
        Keys.Add( key );
        Values.Add( value );
		TotalDict.Add( key, value );
		if(OnChangeEvent != null)
        	OnChangeEvent();
		SaveInEditor();
        return this;
    }
    [X]
    public UnityDictionary<TKey, TValue> Remove( TKey key, TValue value )
    {
        Keys.Remove( key );
        Values.Remove( value );
		TotalDict.Remove( key );
		if(OnChangeEvent != null)
        	OnChangeEvent();
		SaveInEditor();
        return this;
    }
	[X]
    public void Clear()
    {
        Keys.Clear();
        Values.Clear();
		TotalDict.Clear();
		SaveInEditor();
    }

    public IEnumerator GetEnumerator()
    {
		return TotalDict.GetEnumerator();
    }

    public int Count
    {
        get
        {
            return Keys.Count;
        }
    }

	public bool ContainKey(TKey key)
	{
		return Keys.Contains (key);
	}
	public bool ContainerValue(TValue value)
	{
		return Values.Contains (value);
	}

	public TKey GetValueKey(TValue value)
	{
		var index = Values.IndexOf(value);
		return Keys[index];
	}
}
