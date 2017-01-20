using System;
using wuxingogo.Editor;

namespace wuxingogo.Editor
{
	using UnityEngine;
	using System.Collections;
	using UnityEditor;
	using wuxingogo.Runtime;
	using System.Reflection;
	using System.Collections.Generic;
	using System;
	using Object = UnityEngine.Object;
	[CustomEditor(typeof(XEditorScriptableObject), true )]
	[CanEditMultipleObjects]
	public class EditorScriptableObjectWindow : XMonoBehaviourEditor
	{
	}
}


