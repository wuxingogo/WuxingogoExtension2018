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

    [SerializeField]
	private	List<TKey> totalKey = new List<TKey>();
	
	[SerializeField]
	private	List<TValue> totalValue = new List<TValue>();

    private Dictionary<TKey, TValue> totalDict = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
        get
        {
            for( int i = 0; i < totalKey.Count; i++ )
            {
                if( totalKey[i].Equals( key ))
                {
                    return totalValue[i];
                }
            }
            return default( TValue );
        }
    }

    [X]
    public UnityDictionary<TKey, TValue> Add( TKey key, TValue value )
    {
        totalKey.Add( key );
        totalValue.Add( value );
        totalDict.Add( key, value );
        OnChangeEvent();
        return this;
    }
    [X]
    public UnityDictionary<TKey, TValue> Remove( TKey key, TValue value )
    {
        totalKey.Remove( key );
        totalValue.Remove( value );
        totalDict.Remove( key );
        OnChangeEvent();
        return this;
    }

    public void Clear()
    {
        totalKey.Clear();
        totalValue.Clear();
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
            return totalKey.Count;
        }
    }
}
