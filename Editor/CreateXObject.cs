using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
public class CreateXObject : XBaseEditor 
{
	[MenuItem("Assets/Create/Wuxingogo/XObject", false, 100)]
	public static void CreateFile()
	{
		Object target = Selection.activeObject;
		
		// Make sure the selection is a texture.
		if (target == null )
			return;
		
		Object sourceTex = target;
		
		string path = EditorUtility.SaveFilePanel("Create A XObject", AssetDatabase.GetAssetPath(sourceTex), "NewScript.cs", "cs");
		if (path == "")
			return;
		
		path = FileUtil.GetProjectRelativePath(path);
		
//		ExplosionShape es = CreateInstance<ExplosionShape>();
//		AssetDatabase.CreateAsset(es, path);
//		AssetDatabase.SaveAssets();
		
		// Get the path to the selected texture.       
		string filePathWithName = AssetDatabase.GetAssetPath(sourceTex);
//		FileInfo file = new FileInfo(filePathWithName + @"/aaa.cs");//创建文件  
		
		FileInfo file = new FileInfo(path);           
//		Debug.Log("创建时间：" + file.CreationTime);             
//		Debug.Log("路径：" + file.DirectoryName);
		StreamWriter sw = file.AppendText();//打开追加流          
		
		string className = file.Name.Substring(0, file.Name.Length - 3); 
		   
		sw.Write("using UnityEngine;\n"
		          + "using System.Collections;\n"
		         + "public class "+ className +" : XObject "
		        + "\n{"
		        + "\n}");//追加数据             
		sw.Dispose();//释放资源,关闭文件 
		
		AssetDatabase.SaveAssets();	
		AssetDatabase.Refresh();
		

	}
}