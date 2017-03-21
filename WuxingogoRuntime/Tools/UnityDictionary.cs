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
}
