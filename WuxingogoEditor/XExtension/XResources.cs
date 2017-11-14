using UnityEngine;
using UnityEditor;
using System.Collections;
using wuxingogo.Runtime;

public static class XResources {

	static XResources(){
		string resPath = "Assets/Plugins/WuxingogoExtension/wuxingogo.psd";
		LogoTexture =  AssetDatabase.LoadAssetAtPath<Texture>(resPath);

	}
	public static Texture LogoTexture = null;
	[X]
    public static void SaveAll()
    {
        EditorPrefs.SetString("XLogo", AssetDatabase.GetAssetPath(LogoTexture));
    }
    
	
}
