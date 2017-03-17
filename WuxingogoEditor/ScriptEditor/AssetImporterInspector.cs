//
// AssetImporterInspector.cs
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
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

namespace wuxingogo.Editor
{

	public abstract class AssetImporterInspector : UnityEditor.Editor
	{
		private ulong m_AssetTimeStamp;

		private bool m_MightHaveModified;

		//		internal override string targetTitle
		//		{
		//			get
		//			{
		//				return  " Import Settings";
		//			}
		//		}

		//		internal override int referenceTargetIndex
		//		{
		//			get
		//			{
		//				return base.referenceTargetIndex;
		//			}
		//			set
		//			{
		//				base.referenceTargetIndex = value;
		//				this.assetEditor.referenceTargetIndex = value;
		//			}
		//		}
		//
		//		internal override IPreviewable preview
		//		{
		//			get
		//			{
		//				if (this.useAssetDrawPreview && this.assetEditor != null)
		//				{
		//					return this.assetEditor;
		//				}
		//				return base.preview;
		//			}
		//		}

		protected virtual bool useAssetDrawPreview
		{
			get
			{
				return true;
			}
		}

		internal virtual bool showImportedObject
		{
			get
			{
				return true;
			}
		}

		//		internal override void OnHeaderIconGUI(Rect iconRect)
		//		{
		//			this.assetEditor.OnHeaderIconGUI(iconRect);
		//		}

		//		internal override SerializedObject GetSerializedObjectInternal()
		//		{
		//			if (this.m_SerializedObject == null)
		//			{
		//				this.m_SerializedObject = SerializedObject.LoadFromCache(base.GetInstanceID());
		//			}
		//			if (this.m_SerializedObject == null)
		//			{
		//				this.m_SerializedObject = new SerializedObject(base.targets);
		//			}
		//			return this.m_SerializedObject;
		//		}

		public virtual void OnDisable()
		{
			AssetImporter assetImporter = this.target as AssetImporter;
			if (Unsupported.IsDestroyScriptableObject(this) && this.m_MightHaveModified && assetImporter != null && !InternalEditorUtility.ignoreInspectorChanges && this.HasModified() && !this.AssetWasUpdated())
			{
				string message = "Unapplied import settings for '" + assetImporter.assetPath + "'";
				if (base.targets.Length > 1)
				{
					message = "Unapplied import settings for '" + base.targets.Length + "' files";
				}
				if (EditorUtility.DisplayDialog("Unapplied import settings", message, "Apply", "Revert"))
				{
					this.Apply();
					this.m_MightHaveModified = false;
					AssetImporterInspector.ImportAssets(this.GetAssetPaths());
				}
			}
			/*
			if (this.serializedObject != null && this.serializedObject.hasModifiedProperties)
			{
				this.serializedObject.Cache(base.GetInstanceID());
				this.serializedObject = null;
			}
			*/
		}

		internal virtual void Awake()
		{
			this.ResetTimeStamp();
			this.ResetValues();
		}

		private string[] GetAssetPaths()
		{
			UnityEngine.Object[] targets = base.targets;
			string[] array = new string[targets.Length];
			for (int i = 0; i < targets.Length; i++)
			{
				AssetImporter assetImporter = targets[i] as AssetImporter;
				array[i] = assetImporter.assetPath;
			}
			return array;
		}

		internal virtual void ResetValues()
		{
			base.serializedObject.SetIsDifferentCacheDirty();
			base.serializedObject.Update();
		}

		internal virtual bool HasModified()
		{
			//			return base.serializedObject.hasModifiedProperties;
			return true;
		}

		internal virtual void Apply()
		{
			base.serializedObject.ApplyModifiedPropertiesWithoutUndo();
		}

		internal bool AssetWasUpdated()
		{
			AssetImporter assetImporter = this.target as AssetImporter;
			if (this.m_AssetTimeStamp == 0uL)
			{
				this.ResetTimeStamp();
			}
			return assetImporter != null && this.m_AssetTimeStamp != assetImporter.assetTimeStamp;
		}

		internal void ResetTimeStamp()
		{

			AssetImporter assetImporter = this.target as AssetImporter;
			if (assetImporter != null)
			{
				this.m_AssetTimeStamp = assetImporter.assetTimeStamp;
			}
		}

		internal void ApplyAndImport()
		{
			this.Apply();
			this.m_MightHaveModified = false;
			AssetImporterInspector.ImportAssets(this.GetAssetPaths());
			this.ResetValues();
		}

		private static void ImportAssets(string[] paths)
		{
			for (int i = 0; i < paths.Length; i++)
			{
				string path = paths[i];
				AssetDatabase.WriteImportSettingsIfDirty(path);
			}
			try
			{
				AssetDatabase.StartAssetEditing();
				for (int j = 0; j < paths.Length; j++)
				{
					string path2 = paths[j];
					AssetDatabase.ImportAsset(path2);
				}
			}
			finally
			{
				AssetDatabase.StopAssetEditing();
			}
		}

		protected void RevertButton()
		{
			this.RevertButton("Revert");
		}

		protected void RevertButton(string buttonText)
		{
			if (GUILayout.Button(buttonText, new GUILayoutOption[0]))
			{
				this.m_MightHaveModified = false;
				this.ResetTimeStamp();
				this.ResetValues();
				if (this.HasModified())
				{
					Debug.LogError("Importer reports modified values after reset.");
				}
			}
		}

		protected bool ApplyButton()
		{
			return this.ApplyButton("Apply");
		}

		protected bool ApplyButton(string buttonText)
		{
			if (GUILayout.Button(buttonText, new GUILayoutOption[0]))
			{
				this.ApplyAndImport();
				return true;
			}
			return false;
		}

		protected virtual bool ApplyRevertGUIButtons()
		{
			EditorGUI.BeginDisabledGroup(!this.HasModified());
			this.RevertButton();
			bool result = this.ApplyButton();
			EditorGUI.EndDisabledGroup();
			return result;
		}

		protected void ApplyRevertGUI()
		{
			this.m_MightHaveModified = true;
			EditorGUILayout.Space();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			bool flag = this.ApplyRevertGUIButtons();
			if (this.AssetWasUpdated() && Event.current.type != EventType.Layout)
			{
				//				IPreviewable preview = this.preview;
				//				if (preview != null)
				//				{
				//					preview.ReloadPreviewInstances();
				//				}
				//				this.ResetTimeStamp();
				//				this.ResetValues();
				//				base.Repaint();
			}
			GUILayout.EndHorizontal();
			if (flag)
			{
				GUIUtility.ExitGUI();
			}
		}
	}
}

