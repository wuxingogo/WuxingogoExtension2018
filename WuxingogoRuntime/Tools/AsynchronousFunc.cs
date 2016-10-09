//
// AsynchronousFunc.cs
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
using System.Collections;
using UnityEngine;
using wuxingogo.Runtime;

namespace wuxingogo.tools
{
	public class AsynchronousFunc
	{
		static XMonoBehaviour behaviour = null;
		[RuntimeInitializeOnLoadMethod]
		static void OnSecondRuntimeMethodLoad()
		{
			GameObject gameObject = new GameObject("Wuxingogo Kernal");
			behaviour = gameObject.AddComponent<XMonoBehaviour>();
			Object.DontDestroyOnLoad(gameObject);
		}

		public static void Delaytime(float delay, System.Action onFinish)
		{
			behaviour.StartCoroutine(DelayTime(delay, onFinish));
		}

		static IEnumerator DelayTime(float delay, System.Action onFinish)
		{
			yield return new WaitForSeconds(delay);
			onFinish();
		}

        public static void StartCoroutine( System.Collections.IEnumerator iEnumerator)
        {
            behaviour.StartCoroutine( iEnumerator );
        }
	}
}

