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
			
			var type = Type.GetType ("InspectorWindow");
			var returnValue = XReflectionUtils.TryInvokeGlobalMethod (type, "GetInspectors") as List<EditorWindow>;
			
			return returnValue;
		}
		public static EditorWindow GetInspectorWindow()
		{
			var type = XReflectionUtils.TryGetClass("InspectorWindow");
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

