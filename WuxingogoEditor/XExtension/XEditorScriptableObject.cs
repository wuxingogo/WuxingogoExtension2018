using UnityEngine;
using UnityEditor;
using wuxingogo.Runtime;

namespace wuxingogo.Editor
{
	public class XEditorScriptableObject : ScriptableObject
	{
		[X]
		public void Dirty()
		{
			EditorUtility.SetDirty (this);
		}
	}
}


