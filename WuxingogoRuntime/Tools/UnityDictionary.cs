using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class UnityDictionaryIntString : UnityDictionary<int,string> {}
[Serializable]
public class UnityDictionaryIntObject : UnityDictionary<int,UnityEngine.Object> {}

[Serializable]
public class UnityDictionary<TKey,TValue>
{
	[SerializeField]
	private	List<TKey> _keys = new List<TKey>();
	
	[SerializeField]
	private	List<TValue> _values = new List<TValue>();

	private Dictionary<TKey,TValue> _cache;

	public void Add(TKey key, TValue value)
	{
		if (_cache == null)
			BuildCache();

		_cache.Add(key,value);
		_keys.Add(key);
		_values.Add(value);
	}

	public TValue this[TKey key]
	{
		get {
			if (_cache == null)
				BuildCache();
			
			return _cache[key];
		}
	}
	public void Remove(TKey key){
		_cache.Remove(key);
	}
//	public bool ContainsKey(int key){
//		return _cache.ContainsKey(key);
//	}
	public bool ContainsKey(TKey key){
		if(_cache == null){
			BuildCache();
		}
		return _cache.ContainsKey(key);
	}

	void BuildCache()
	{
		_cache = new Dictionary<TKey,TValue>();
		for (int i=0; i!=_keys.Count; i++)
		{
			_cache.Add(_keys[i],_values[i]);
		}
	}
}
