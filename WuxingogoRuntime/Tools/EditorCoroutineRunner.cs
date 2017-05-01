//
// EditorCoroutineRunner.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2017 ly-user
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

namespace wuxingogo.tools
{
	// Unity Editor Coroutine
	// Author: Jingxuan Wang
	// Created: 2014/10/20

	using UnityEngine;

	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;

	public static class EditorCoroutineRunner
	{
		private class EditorCoroutine : IEnumerator
		{
			private Stack<IEnumerator> executionStack;

			public EditorCoroutine(IEnumerator iterator)
			{
				this.executionStack = new Stack<IEnumerator>();
				this.executionStack.Push(iterator);
			}

			public bool MoveNext()
			{
				IEnumerator i = this.executionStack.Peek();

				if (i.MoveNext())
				{
					object result = i.Current;
					if (result != null && result is IEnumerator)
					{
						this.executionStack.Push((IEnumerator)result);
					}

					return true;
				}
				else
				{
					if (this.executionStack.Count > 1)
					{
						this.executionStack.Pop();
						return true;
					}
				}

				return false;
			}

			public void Reset()
			{
				throw new System.NotSupportedException("This Operation Is Not Supported.");
			}

			public object Current
			{
				get { return this.executionStack.Peek().Current; }
			}

			public bool Find(IEnumerator iterator)
			{
				return this.executionStack.Contains(iterator);
			}
		}

		private static List<EditorCoroutine> editorCoroutineList;
		private static List<IEnumerator> buffer;

		public static IEnumerator StartEditorCoroutine(IEnumerator iterator)
		{
			if (editorCoroutineList == null)
			{
				editorCoroutineList = new List<EditorCoroutine>();
			}
			if (buffer == null)
			{
				buffer = new List<IEnumerator>();
			}

			if (editorCoroutineList.Count == 0)
			{
				System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.tools.XEditorUtilies" );
				if (type != null) {
					var method = type.GetMethod ("AddEditorUpdate");
					Action d =()=> Update();
					method.Invoke (null, new object[] { d });
				}
			}

			// add iterator to buffer first
			buffer.Add(iterator);

			return iterator;
		}

		private static bool Find(IEnumerator iterator)
		{
			// If this iterator is already added
			// Then ignore it this time
			foreach (EditorCoroutine editorCoroutine in editorCoroutineList)
			{
				if (editorCoroutine.Find(iterator))
				{
					return true;
				}
			}

			return false;
		}

		private static void Update()
		{
			// EditorCoroutine execution may append new iterators to buffer
			// Therefore we should run EditorCoroutine first
			editorCoroutineList.RemoveAll
			(
				coroutine => { return coroutine.MoveNext() == false; }
			);

			// If we have iterators in buffer
			if (buffer.Count > 0)
			{
				foreach (IEnumerator iterator in buffer)
				{
					// If this iterators not exists
					if (!Find(iterator))
					{
						// Added this as new EditorCoroutine
						editorCoroutineList.Add(new EditorCoroutine(iterator));
					}
				}

				// Clear buffer
				buffer.Clear();
			}

			// If we have no running EditorCoroutine
			// Stop calling update anymore
			if (editorCoroutineList.Count == 0)
			{
				System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.tools.XEditorUtilies" );
				if (type != null) {
					var method = type.GetMethod ("RemoveEditorUpdate");
					Action d =()=> Update();
					method.Invoke (null, new object[] { d });
				}
			}
		}
	}
}

