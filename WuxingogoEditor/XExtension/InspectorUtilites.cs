using System.Collections.Generic;
using UnityEditor;
using wuxingogo.Reflection;
using System;

namespace wuxingogo.Editor
{
	public class InspectorUtilites
	{

		public static List<EditorWindow> GetAllInspector()
		{
			
			var type = XReflectionUtils.GetUnityEditor("UnityEditor.InspectorWindow");
			var returnValue = type.TryInvokeGlobalMethod ("GetInspectors");
			return (List<EditorWindow>)returnValue;
		}
		public static EditorWindow GetInspectorWindow()
		{
			
			var type = XReflectionUtils.GetUnityEditor ("UnityEditor.InspectorWindow");
			var window = EditorWindow.GetWindow (type);
			return window;
		}
//		public static EditorWindow GetTargetWindow(Editor editor)
//		{
//			var allWindow = GetAllInspector ();
//
//		}
	}
}

