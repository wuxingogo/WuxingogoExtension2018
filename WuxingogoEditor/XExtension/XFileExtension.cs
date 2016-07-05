using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

namespace wuxingogo.tools
{
	public class XFileExtension : XBaseWindow
	{
		[MenuItem("Wuxingogo/Wuxingogo XFileExtension")]
		static void Init()
		{
			InitWindow<XFileExtension>();
		}

		private string url = "";
		private string lastUrl = "";
		private string content = "";
		private string directoryUrl = "";
		private Object target = null;

		public override void OnXGUI()
		{
			base.OnXGUI();

			url = CreateStringField("File Path", url);
			if (File.Exists(url))
			{
				if (lastUrl != url)
				{
					content = File.ReadAllText(url);
					content.Substring(0, Mathf.Min(content.Length, 30));
				}
				CreateLabel(content);
			}

			directoryUrl = CreateStringField("Directory Path", directoryUrl);

			if (Directory.Exists(directoryUrl))
			{
				var directories = Directory.GetDirectories(directoryUrl);
				foreach (var item in directories)
				{
					CreateLabel(item);
				}
			}

			target = CreateObjectField("Target", target);

			if (target != null)
			{
				string path = AssetDatabase.GetAssetPath(target);
				CreateLabel("Target Relative Path is " + Application.dataPath + path, true);
				CreateLabel("Target Path is " + path, true);
				CreateLabel("Target Meta is " + AssetDatabase.GetTextMetaFilePathFromAssetPath(path), true);

			}

		}

		void OnSelectionChange()
		{
			if (Selection.objects != null && Selection.objects.Length > 0)
				target = Selection.objects[0];
			Repaint();
		}

		public static List<T> GetSpecifyFolderAssets<T>(ref string path, string title = "Default Folder") where T : Object
		{
			path = EditorUtility.OpenFolderPanel(title, XEditorSetting.ProjectPath, "");
			List<T> result = new List<T>();
			if (string.IsNullOrEmpty(path))
			{
				path = FileUtil.GetProjectRelativePath(path);
				result = GetAtReleativePath<T>(path);
			}
			return result;
		}

		public static List<T> GetAtReleativePath<T>(string path) where T : Object
		{

			List<T> result = new List<T>();
			string[] fileEntries = Directory.GetFiles(path);

			foreach (string fileName in fileEntries)
			{

				T t = AssetDatabase.LoadAssetAtPath<T>(fileName);

				if (t != null)
					result.Add(t);
			}
			return result;
		}
	}
}
