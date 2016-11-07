//
// DelegateUtils.cs
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
using System;
using System.Collections.Generic;

public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);

public class DelegateUtils
{

	public static void Clear()
	{
		eventTable.Clear();
	}
	static private Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    static public bool ContainDelegate( string eventKey )
    {
        return eventTable.ContainsKey( eventKey );
    }

    static public bool IsEventHandlerRegistered( string eventKey, Delegate prospectiveHandler )
    {
        if( !ContainDelegate( eventKey ) )
        {
            return false;
        }
        var InvocationList = eventTable[eventKey].GetInvocationList();
        foreach( Delegate existingHandler in InvocationList )
        {
            if( existingHandler == prospectiveHandler )
            {
                return true;
            }
        }
        
        return false;
    }

	public static void addListener(string eventType, Callback handler)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}
		eventTable[eventType] = (Callback)eventTable[eventType] + handler;
	}

	public static void addListener<T>(string eventType, Callback<T> handler)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}
		eventTable[eventType] = (Callback<T>)eventTable[eventType] + handler;
	}

	public static void addListener<T, U>(string eventType, Callback<T, U> handler)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}
		eventTable[eventType] = (Callback<T, U>)eventTable[eventType] + handler;
	}

	public static void addListener<T, U, V>(string eventType, Callback<T, U, V> handler)
	{
		if (!eventTable.ContainsKey(eventType))
		{
			eventTable.Add(eventType, null);
		}
		eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] + handler;
	}

	public static void removeListener(string eventType, Callback handler)
	{
		eventTable[eventType] = (Callback)eventTable[eventType] - handler;
		if (eventTable[eventType] == null)
		{
			eventTable.Remove(eventType);
		}
	}

	public static void removeListener<T>(string eventType, Callback<T> handler)
	{
		eventTable[eventType] = (Callback<T>)eventTable[eventType] - handler;
		if (eventTable[eventType] == null)
		{
			eventTable.Remove(eventType);
		}
	}

	public static void removeListener<T, U>(string eventType, Callback<T, U> handler)
	{
		eventTable[eventType] = (Callback<T, U>)eventTable[eventType] - handler;
		if (eventTable[eventType] == null)
		{
			eventTable.Remove(eventType);
		}
	}

	public static void removeListener<T, U, V>(string eventType, Callback<T, U, V> handler)
	{
		eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] - handler;
		if (eventTable[eventType] == null)
		{
			eventTable.Remove(eventType);
		}
	}

	static public void broadcast(string eventType)
	{
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d))
		{
			Callback callback = d as Callback;

			if (callback != null)
			{
				callback();
			}
		}
	}

	static public void broadcast<T>(string eventType, T args)
	{
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d))
		{
			Callback<T> callback = d as Callback<T>;

			if (callback != null)
			{
				callback(args);
			}
		}
	}

	static public void broadcast<T, U>(string eventType, T args, U args1)
	{
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d))
		{
			Callback<T, U> callback = d as Callback<T, U>;

			if (callback != null)
			{
				callback(args, args1);
			}
		}
	}

	static public void broadcast<T, U, V>(string eventType, T args, U args1, V args2)
	{
		Delegate d;
		if (eventTable.TryGetValue(eventType, out d))
		{
			Callback<T, U, V> callback = d as Callback<T, U, V>;

			if (callback != null)
			{
				callback(args, args1, args2);
			}
		}
	}
}





