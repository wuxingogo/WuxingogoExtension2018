//
// NetworkBehaviourEditor.cs
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
using UnityEngine;
using System.Collections;



namespace wuxingogo.Editor
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;
	using wuxingogo.Runtime;
	using System.Reflection;
	using System.Collections.Generic;
	using System;
	using UnityEngine.Networking;
	using Object = UnityEngine.Object;
	[CustomEditor(typeof(NetworkBehaviour), true )]
	public class XNetworkBehaviourEditor : NetworkBehaviourInspector
	{
		public override void OnInspectorGUI()
		{
			XBaseEditor.DrawLogo();
			base.OnInspectorGUI();
			XMonoBehaviourEditor.ShowXAttributeMember(target);

			var networkBehaviour = target as NetworkBehaviour;

			XBaseWindow.CreateCheckBox ("isServer", networkBehaviour.isServer);
			XBaseWindow.CreateCheckBox ("isClient", networkBehaviour.isClient);
			XBaseWindow.CreateCheckBox ("isLocalPlayer", networkBehaviour.isLocalPlayer);
			XBaseWindow.CreateIntField ("netId", (int)networkBehaviour.netId.Value);
			XBaseWindow.CreateIntField ("playerControllerId", (int)networkBehaviour.playerControllerId);

		}
	}

}