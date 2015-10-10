using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
public class CreateXBaseWindow : XBaseWindow 
{
	[MenuItem("Assets/Create/Wuxingogo/XBaseWindow", false, 100)]
	public static void CreateFile()
	{
		Object target = Selection.activeObject;
		
		// Make sure the selection is a texture.
		if (target == null )
			return;
		
		Object sourceTex = target;
		
		string path = EditorUtility.SaveFilePanel("Create A XBaseWindow", AssetDatabase.GetAssetPath(sourceTex), "NewWindow.cs", "cs");
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
		         + "using UnityEditor;\n"
		         + "using System.Collections;\n"
		         + "public class "+ className +" : XBaseWindow "
		         + "\n{\n"
		         + "\n\t[MenuItem (\"Wuxingogo/Wuxingogo " + className + " \")]\n"
		         + "\tstatic void init () {\n"
		         + "\t\t" + className + " window = (" + className + ")EditorWindow.GetWindow (typeof (" + className + " ) );\n"
		         + "\t}\n"
		         + "\n\tpublic override void OnXGUI(){\n"
		         + "\t\t//TODO List\n"
		         + "\t}\n"
		         + "\n\tvoid OnSelectionChange(){\n"
		         + "\t\t//TODO List\n"
		         + "\n\t}"
		         + "\n}");//追加数据             
		sw.Dispose();//释放资源,关闭文件 

		AssetDatabase.SaveAssets();	
		AssetDatabase.Refresh();
		
		
	}
	
}