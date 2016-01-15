using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.IO;
using UnityEngine.UI;
#if TINYTIME
using TMPro;
#endif
public class ExprotUIToCocos : XBaseWindow 
{

	
	Transform[] transWithImages = null;
	Transform[] transAll = null;
	
	Animator _transAnimator = null;
	Animation _transAnimation = null;
	
	static XmlElement rootDoc;
	static XmlDocument xmlDoc;
	
	[MenuItem ("Wuxingogo/Wuxingogo ExprotUIToCocos")]
	static void init () {
		ExprotUIToCocos window = (ExprotUIToCocos)EditorWindow.GetWindow (typeof (ExprotUIToCocos ) );
	}

	public override void OnXGUI(){
		
		//TODO List
		if(CreateSpaceButton("Export")){
			if(Selection.transforms.Length > 0){
				xmlDoc = new XmlDocument();
				rootDoc = GetElementFromUI(Selection.transforms[0]);
				
				string filepath = Application.dataPath + @"/StreamingAssets/UI/" + Selection.transforms[0].name + ".xml";
				if (!File.Exists(filepath)) {
					File.Delete(filepath);
				}
				
				
				xmlDoc.AppendChild (rootDoc);
				xmlDoc.Save (filepath);
				AssetDatabase.Refresh();
			}
		}
		if( null != _transAnimator ){
			if(CreateSpaceButton("Export Animator", 130)){
				// XAnimatorExtension.GetAnimation( _transAnimator );
//				Animation anim = 
			}
		}
		
		if(Selection.transforms.Length > 0 && null != transAll ){
			CreateObjectField(Selection.transforms[0].name, Selection.transforms[0]);
			foreach (var item in transAll) {
				CreateObjectField(item.name, item);
			}
		}
		
	}
	
	XmlElement GetElementFromUI(Transform element){
		XmlElement xmlTrans = xmlDoc.CreateElement ("prefab");
		xmlTrans.SetAttribute("name", element.name);
		
		bool visible = element.gameObject.activeInHierarchy;
		int intVisble = 0;
		if(visible)intVisble = 1;
		else intVisble = 0;
		
		xmlTrans.SetAttribute("visible", intVisble.ToString());
		RectTransform rect = element.GetComponent<RectTransform>();
		if(rect != null){
			xmlTrans.SetAttribute("anchorMin", rect.anchorMin.ToString());
			xmlTrans.SetAttribute("anchorMax", rect.anchorMax.ToString());
			xmlTrans.SetAttribute("localScale", rect.localScale.ToString());
			//					xmlTrans.SetAttribute("anchorMinX", rect.anchorMin.x.ToString());
			//					xmlTrans.SetAttribute("anchorMinY", rect.anchorMin.y.ToString());
			//					xmlTrans.SetAttribute("anchorMaxX", rect.anchorMax.x.ToString());
			//					xmlTrans.SetAttribute("anchorMaxY", rect.anchorMax.y.ToString());
			//					xmlTrans.SetAttribute("anchorPositionX", rect.anchoredPosition.x.ToString());
			//					xmlTrans.SetAttribute("anchorPositionY", rect.anchoredPosition.y.ToString());
			//					xmlTrans.SetAttribute("anchorPositionY", rect.anchorMin.y.ToString());
			//					xmlTrans.SetAttribute("pivotX", rect.pivot.x.ToString());
			xmlTrans.SetAttribute("pivot", rect.pivot.ToString());
			//					xmlTrans.SetAttribute("localPositionX", rect.localPosition.x.ToString());
			xmlTrans.SetAttribute("size", rect.sizeDelta.ToString());
			xmlTrans.SetAttribute("rotation", rect.rotation.ToString());
			xmlTrans.SetAttribute("localposition", rect.localPosition.ToString());
			
			//					xmlTrans.SetAttribute("localScaleX", rect.localScale.x.ToString());
			//					xmlTrans.SetAttribute("localScaleY", rect.localScale.y.ToString());	
		}
		Image image = element.GetComponent<Image>();
		if( null != image){
			xmlTrans.SetAttribute("mainTexture", image.mainTexture.name);
			xmlTrans.SetAttribute("imageColor", image.color.ToString());					
		}
		Text tex = element.GetComponent<Text>();
		if( null != tex ){
			xmlTrans.SetAttribute("text", tex.text);	
			xmlTrans.SetAttribute("textColor", tex.color.ToString());	
			xmlTrans.SetAttribute("textSize", tex.fontSize.ToString());	
		}
#if TINYTIME
		TextMeshProUGUI tmpText = element.GetComponent<TextMeshProUGUI>();
		if( null != tmpText ){
			xmlTrans.SetAttribute("text", tmpText.text);	
			xmlTrans.SetAttribute("textColor", tmpText.color.ToString());	
			xmlTrans.SetAttribute("textSize", tmpText.fontSize.ToString());	
		}
#endif
		if(element.childCount > 0){
			for(int pos = 0; pos < element.childCount; pos++){
			
//				xmlTrans.SetAttributeNode("child" + pos, GetElementFromUI(element.GetChild(pos)));
//				xmlTrans.SetAttributeNode( GetElementFromUI(element.GetChild(pos)) );
				xmlTrans.AppendChild(GetElementFromUI(element.GetChild(pos)));
			}
		}
		
		return xmlTrans;
		
	}

	void OnSelectionChange(){
		transAll = null;
		//TODO List
		XSelectionExtension.Clear();
		if(Selection.transforms.Length > 0){
			transAll = XSelectionExtension.GetAllChild(Selection.transforms[0]);
			
			_transAnimator = Selection.transforms[0].GetComponent<Animator>();
			_transAnimation = Selection.transforms[0].GetComponent<Animation>();
		}
		Repaint();
	}
}