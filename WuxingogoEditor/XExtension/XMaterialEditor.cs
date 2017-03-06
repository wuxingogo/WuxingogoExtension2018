//
// XMaterialEditor.cs
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
using UnityEngine;
using System.Collections;
using UnityEditor;
namespace wuxingogo.Editor
{
	[CustomEditor(typeof(Material))]
	public class XMaterialEditor : MaterialEditor {
		Material material = null;
		public override void OnEnable()
		{
			base.OnEnable ();
			material = target as Material;

		}
		public override void OnDisable ()
		{
			base.OnDisable ();
			material = null;
		}
		bool toggleKeywords = false;
		public override void OnInspectorGUI ()
		{
			if (isVisible) {
				XBaseEditor.DrawLogo ();
				base.OnInspectorGUI ();
				material.renderQueue = XBaseWindow.CreateIntField ("renderQueue", material.renderQueue);
				toggleKeywords = XBaseWindow.CreateCheckBox ("shaderKeywords : " + material.shaderKeywords.Length, toggleKeywords);
				if (toggleKeywords) {
					for (int i = 0; i < material.shaderKeywords.Length; i++) {
						material.shaderKeywords [i] = XBaseWindow.CreateStringField (material.shaderKeywords [i]);
					}
				}
				if(material.HasProperty ("_Color"))
					material.color = XBaseWindow.CreateColorField ("_Color", material.color);
			}
		}
	}

}

