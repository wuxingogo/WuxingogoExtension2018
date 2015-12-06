using UnityEngine;
using UnityEditor;
using System.Collections;

public class XResources {

	private XResources(){
		string resPath = EditorPrefs.GetString("XLogo", "Assets/WuxingogoExtension/wuxingogo.psd");
		LogoTexture =  AssetDatabase.LoadAssetAtPath<Texture>(resPath);
		
		IconNames = Resources.Load<TextAsset>("icon").text.Split("\n"[0]);
	}
	public Texture LogoTexture = null;
	public string[] IconNames = null;

    public void SaveAll()
    {
        EditorPrefs.SetString("XLogo", AssetDatabase.GetAssetPath(LogoTexture));
    }
    
    private static XResources _instance = null;
    
    public static XResources GetInstance(){
    	if(_instance == null){
    		_instance = new XResources();
    	}
    	return _instance;
    }
}
