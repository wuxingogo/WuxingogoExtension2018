using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class CreateXMonoBehaviour : CreateUnityScript {
	
	[MenuItem("Assets/Create/Wuxingogo/XMonoBehaviour", false, 100)]
	public static void CreateFile()
	{
        string path = EditorUtility.SaveFilePanel("Create A Object", XEditorSetting.ProjectPath, "NewEditor.cs", "cs");
        if (path == "")
            return;
        
        path = FileUtil.GetProjectRelativePath(path); 
        
        FileInfo file = new FileInfo(path);           
        StreamWriter sw = file.AppendText(); 
        
        string fileName = file.Name;
        string className = file.Name.Substring(0, file.Name.Length - 3);  
        
        string codeHeader = WriteHeader(file.Name);
        string codeUs = WriteUseNameSpace("UnityEngine", "System.Collections");
        string codeClass = WriteExtendClass(className, "XMonoBehaviour");
        
        sw.Write(codeHeader + codeUs + codeClass);           
        sw.Dispose();  
        
        AssetDatabase.SaveAssets(); 
        AssetDatabase.Refresh(); 
		

	}


	
	
	
}
