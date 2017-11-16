//
// MonoScriptImporterInspector.cs
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
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace wuxingogo.Editor
{
	[CustomEditor(typeof(MonoImporter))]
	internal class MonoScriptImporterInspector : wuxingogo.Editor.AssetImporterInspector
	{

		[MenuItem("CONTEXT/MonoImporter/Reset")]
		private static void ResetDefaultReferences(MenuCommand command)
		{
			MonoImporter monoImporter = command.context as MonoImporter;
			monoImporter.SetDefaultReferences(new string[0], new UnityEngine.Object[0]);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(monoImporter));
		}

		private static bool IsTypeCompatible(Type type)
		{
			return type != null && (type.IsSubclassOf(typeof(MonoBehaviour)) || type.IsSubclassOf(typeof(ScriptableObject)));
		}

		private void ShowFieldInfo(System.Type type, MonoImporter importer, List<string> names, List<UnityEngine.Object> objects, ref bool didModify)
		{
			if (!MonoScriptImporterInspector.IsTypeCompatible(type))
				return;
			this.ShowFieldInfo(type.BaseType, importer, names, objects, ref didModify);
			foreach (System.Reflection.FieldInfo field in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				if (!field.IsPublic)
				{
					object[] customAttributes = field.GetCustomAttributes(typeof (SerializeField), true);
					if (customAttributes == null || customAttributes.Length == 0)
						continue;
				}
				if (field.FieldType.IsSubclassOf(typeof (UnityEngine.Object)) || field.FieldType == typeof (UnityEngine.Object))
				{
					UnityEngine.Object defaultReference = importer.GetDefaultReference(field.Name);
					UnityEngine.Object @object = EditorGUILayout.ObjectField(ObjectNames.NicifyVariableName(field.Name), defaultReference, field.FieldType, false, new GUILayoutOption[0]);
					names.Add(field.Name);
					objects.Add(@object);
					if (defaultReference != @object)
						didModify = true;
				}
			}
		}
		
		public override void OnInspectorGUI()
		{
			MonoImporter target = this.target as MonoImporter;
			MonoScript script = target.GetScript();
			if (!(bool) ((UnityEngine.Object) script))
				return;
			System.Type type = script.GetClass();
			if (!MonoScriptImporterInspector.IsTypeCompatible(type))
				EditorGUILayout.HelpBox("No MonoBehaviour scripts in the file, or their names do not match the file name.", MessageType.Info);
			XMonoBehaviourEditor.ShowXAttributeType (type);
			Vector2 iconSize = EditorGUIUtility.GetIconSize();
			EditorGUIUtility.SetIconSize(new Vector2(16f, 16f));
			List<string> names = new List<string>();
			List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
			bool didModify = false;
			this.ShowFieldInfo(type, target, names, objects, ref didModify);
			EditorGUIUtility.SetIconSize(iconSize);
			if (!didModify)
				return;
			target.SetDefaultReferences(names.ToArray(), objects.ToArray());
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath((UnityEngine.Object) target));
		}
	}
}

