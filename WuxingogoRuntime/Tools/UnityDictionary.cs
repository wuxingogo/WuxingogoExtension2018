using UnityEngine;
using System.Collections.Generic;
using System;
using wuxingogo.Runtime;
using System.Collections;
using System.Linq;

[Serializable]
public class UnityDictionary<TKey,TValue> : XScriptableObject 
    where TKey : new()  where TValue : new()
{
    public delegate void OnChange();

    public event OnChange OnChangeEvent;

	public	List<TKey> Keys = new List<TKey>();
	
    public List<TValue> Values = new List<TValue>();

    private Dictionary<TKey, TValue> totalDict = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
        get
        {
            for( int i = 0; i < Keys.Count; i++ )
            {
                if( Keys[i].Equals( key ))
                {
                    return Values[i];
                }
            }
            return default( TValue );
        }
    }

    [X]
    public UnityDictionary<TKey, TValue> Add( TKey key, TValue value )
    {
        Keys.Add( key );
        Values.Add( value );
        totalDict.Add( key, value );
        OnChangeEvent();
        return this;
    }
    [X]
    public UnityDictionary<TKey, TValue> Remove( TKey key, TValue value )
    {
        Keys.Remove( key );
        Values.Remove( value );
        totalDict.Remove( key );
        OnChangeEvent();
        return this;
    }

    public void Clear()
    {
        Keys.Clear();
        Values.Clear();
        totalDict.Clear();
    }

    public IEnumerator GetEnumerator()
    {
        return totalDict.GetEnumerator();
    }

    public int Count
    {
        get
        {
            return Keys.Count;
        }
    }
}
