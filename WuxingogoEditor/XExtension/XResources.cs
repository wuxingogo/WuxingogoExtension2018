using UnityEngine;
using UnityEditor;
using System.Collections;

public static class XResources {

	static XResources(){
		string resPath = "Assets/Plugins/WuxingogoExtension/wuxingogo.psd";
		LogoTexture =  AssetDatabase.LoadAssetAtPath<Texture>(resPath);

	}
	public static Texture LogoTexture = null;

    public static void SaveAll()
    {
        EditorPrefs.SetString("XLogo", AssetDatabase.GetAssetPath(LogoTexture));
    }
    
	
}
