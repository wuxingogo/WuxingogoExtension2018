using UnityEngine;
using UnityEditor;
using System.Collections;

// Analysis disable once CheckNamespace
public static class XResources {

	static XResources(){
//		string resPath = EditorPrefs.GetString("XLogo", "Assets/WuxingogoExtension/wuxingogo.psd");
		string resPath = "Assets/WuxingogoExtension/wuxingogo.psd";
		LogoTexture =  AssetDatabase.LoadAssetAtPath<Texture>(resPath);

	}
	public static Texture LogoTexture = null;

    public static void SaveAll()
    {
        EditorPrefs.SetString("XLogo", AssetDatabase.GetAssetPath(LogoTexture));
    }
    
	
}
