using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class CreateXData : XBaseEditor 
{

	const int m_btnHeight = 40;
//	List<XDataModel<object>> m_list = new List<XDataModel<object>>();
	[MenuItem("Assets/Create/Wuxingogo/XData", false, 100)]
	public static void Init()
	{
		Object target = Selection.activeObject;
		
		// Make sure the selection is a texture.
		if (target == null )
			return;
		
		Object sourceTex = target;
		
		string path = EditorUtility.SaveFilePanel("Create A XData", AssetDatabase.GetAssetPath(sourceTex), "Default.asset", "asset");
		if (path == "")
			return;
		
		path = FileUtil.GetProjectRelativePath(path);
		
		XData data = CreateInstance<XData>();
		AssetDatabase.CreateAsset(data, path);
		AssetDatabase.SaveAssets();
	}
	
	

	
	
	
}