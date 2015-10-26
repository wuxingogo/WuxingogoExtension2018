using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using UnityEditor;

public class XFileExtension : Editor
{
	static Object sourceTex = Selection.activeObject;
	public static string CreateFileWithFormat(string name, string format){
		return EditorUtility.SaveFilePanel("Create A " + name, AssetDatabase.GetAssetPath(sourceTex), name + "." + format , format);
	}
	
	
}