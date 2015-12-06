using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;
#if TMP
using TMPro;
#endif
public class XTextureWindow : XBaseWindow 
{

	List<Texture> m_textures = new List<Texture>();
	List<Image> m_images = new List<Image>();
	List<Component> m_components = new List<Component>();
	List<string> m_tags = new List<string>();
	
	[MenuItem ("Wuxingogo/Wuxingogo XTextureWindow %#5")]
	static void init () {
		XTextureWindow window = (XTextureWindow)EditorWindow.GetWindow (typeof (XTextureWindow ) );
	}

	public override void OnXGUI(){
		//TODO List
		
		CreateMessageField("This is a atlas Viewer. Please Select the GameObjects",MessageType.None);

		if( m_components.Count > 0 ){
			for( int i = 0; i < m_components.Count; i++ ){
				EditorGUILayout.BeginHorizontal();
				if(m_components[i].GetType() == typeof(Text)){
					Text tex = m_components[i] as Text;
					CreateObjectField("selection" + i, tex.mainTexture );
					CreateStringField("packing tag", "Text");
				}
               #if TMP
				else if(m_components[i].GetType() == typeof(TextMeshProUGUI)){
					TextMeshProUGUI tmgui = m_components[i] as TextMeshProUGUI;
					CreateObjectField("selection" + i, tmgui.fontMaterial.mainTexture );
					CreateStringField("packing tag", "TMP_UGUI");
				}
#endif
				else{
					Image img = m_components[i] as Image;
					CreateObjectField("selection" + i, img.mainTexture );
					CreateStringField("packing tag", GetPackingTag(img.sprite));
				}
//				switch(m_components[i].GetType()){
//					case typeof(Text):
//					Text tex = m_components[i] as Text;
//					CreateObjectField("selection" + i, tex.mainTexture );
//					CreateStringField("packing tag", "text");
//					break;
//					case typeof(Image):
//					Image img = m_components[i] as Image;
//					CreateObjectField("selection" + i, img.mainTexture );
//					CreateStringField("packing tag", GetPackingTag(img.sprite));
//					break;
//				}
				
				EditorGUILayout.EndHorizontal();
			}
		}
		
		CreateMessageField("Author By Wuxingogo ",MessageType.None);
	}
	
	string GetPackingTag(Sprite sp){
		if(null == sp) return "";
		string path = AssetDatabase.GetAssetPath(sp.texture); 
		
		TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
		
		if(null != textureImporter )
			return textureImporter.spritePackingTag;
		return sp.texture.ToString();
//		if (textureImporter.isReadable == false) { textureImporter.isReadable = true; AssetDatabase.ImportAsset(path); }
//		return textureImporter.spritePackingTag;
//		PropertyInfo info = sp.GetType().GetProperty("Packing Tag");
//		return info.ToString();
	} 
	
	void SortGameObjects( GameObject[] elements ){
//		Transform[] selection =  Selection
//		
		
//		int pos = 0;
//		for( int i = 0 ; i < elements.Length; i++ ){
//			pos++;
//			TransformSort s = new TransformSort();
//			if( i + 1 <= i)
//			s.Compare( elements[i], elements[i + 1] );
//			
////	          Debug.Log( "pos is " + o.name );
//		}
//		foreach ( GameObject o in elements ){
//			pos++;
//			TransformSort s = new TransformSort();
//			s.Compare(o, 
//			Debug.Log( "pos is " + o.name );
//		}
		
	}

	void OnSelectionChange(){
		//TODO List
		m_textures.Clear();
		m_images.Clear();
		m_tags.Clear();
		m_components.Clear();
		
		SortGameObjects( Selection.gameObjects );
		GameObject[] gameobjects = Selection.gameObjects;
		if( gameobjects.Length > 0 ){
			foreach( GameObject go in gameobjects ){
				Image img = go.transform.GetComponent<Image>();
      
				if( null != img ){		
					m_components.Add( img );
				}
                Text tex = go.transform.GetComponent<Text>();
				if( null != tex ){
					m_components.Add( tex );
				}
                #if TMP
				TextMeshProUGUI tmgui = go.transform.GetComponent<TextMeshProUGUI>();
				if( null != tmgui){
					m_components.Add( tmgui );
				}
                #endif
			}
		}
		
//		if( m_images.Count > 0 ){
//			for( int i = 0; i < m_images.Count; i++ ){
//				if( !m_textures.Contains(m_images[i].mainTexture) ){
//					m_textures.Add(m_images[i].mainTexture);
////					m_textures[i].
//				}
//			}
//		}
//		
//		Object[] objects = Selection.objects;
//		foreach( Object o in objects){
//			if( o.GetType() == typeof(Texture2D))
//			{
//				Texture2D tex = (Texture2D)o;
//				Debug.Log(tex.GetNativeTextureID());
//			}
//		}
		
		
		this.Repaint();
	}
}