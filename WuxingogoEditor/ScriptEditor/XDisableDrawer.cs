//
// DisableDrawer.cs
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
namespace wuxingogo.Attribute
{
    using UnityEngine;
    using UnityEditor;
    using wuxingogo.Runtime;

    [CustomPropertyDrawer(typeof(DisableAttribute), true)]
    public class DisableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var disableAttr = base.attribute as DisableAttribute;
//			if (property.isArray) {
//				ArrayGUI (property);
//			}
			if (disableAttr.IsEditInEditor && !EditorApplication.isPlaying) {
				EditorGUI.PropertyField (position, property, label);
			} 
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(position, property, label);
                EditorGUI.EndDisabledGroup();
            }
        }

		void ArrayGUI (SerializedProperty property) {
			SerializedProperty arraySizeProp = property.FindPropertyRelative("Array.size");
			EditorGUI.BeginDisabledGroup(true);

			EditorGUILayout.PropertyField(arraySizeProp);

			EditorGUI.EndDisabledGroup();

			EditorGUI.indentLevel ++;

			for (int i = 0; i < arraySizeProp.intValue; i++) {
				EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i));
			}

			EditorGUI.indentLevel --;
		}

    }
}
