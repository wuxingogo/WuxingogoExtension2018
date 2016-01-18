using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.IO;

public class XSerializableExtension : XBaseWindow 
{

	static XmlDocument xmlDoc = null;
	// This window to quick set serializable property.
	[MenuItem ("Wuxingogo/Reflection/Wuxingogo XSerializableExtension ")]
	static void init () {
		InitWindow<XSerializableExtension>();
	}

	Component targetComponent = null;
	bool isDirty = true;
	List<FieldInfo> mProperties = new List<FieldInfo>();
	FieldInfo targetProperty = null;
	
	public override void OnXGUI(){
		
		targetComponent = (Component)CreateObjectField("target", targetComponent, typeof(Component));
		if( null == targetComponent){
			CreateMessageField("Drag a Component.", MessageType.None);
		}
		else{
			
			if(CreateSpaceButton("Clean")){
				isDirty = true;
			}
			if(isDirty){
				isDirty = false;
				mProperties.Clear();
				targetProperty = null;
				// targetComponent.GetType().
				foreach( var item in targetComponent.GetType().GetFields() ){
					if( targetComponent.GetType() == item.DeclaringType && !item.IsNotSerialized ) {
						mProperties.Add(item);
					}
				}
			}

			for(int pos = 0; pos < mProperties.Count; pos++ ){
				if(CreateSpaceButton(mProperties[pos].Name)){
					targetProperty = mProperties[pos];
					mProperties.Clear();
				}
			}
			
			if( null != targetProperty ){
				if(CreateSpaceButton(targetProperty.Name)){
					isDirty = true;
				}
#if TINYTIME
				if(targetProperty.FieldType == typeof(System.Collections.Generic.List<TutorialModel>) ){
					List<TutorialModel>list = (List<TutorialModel>)targetProperty.GetValue(targetComponent);
					for( int pos = 0; pos < list.Count; pos++ ){
						BeginHorizontal();
						CreateStringField(pos.ToString(), list[pos].title);
						if(CreateSpaceButton("Insert")){
							list.Insert(pos,new TutorialModel());
						}
						if(CreateSpaceButton("Delete")){
							list.RemoveAt(pos);
						}
						EndHorizontal();
					}
					
					if( 0 < list.Count && CreateSpaceButton("Export")){
						string path = XFileExtension.CreateFileWithFormat("File","xml");
						GetElements(list, path);
					}
				}

				if(targetProperty.FieldType == typeof(System.Collections.Generic.List<TextModelItem>) && null != targetComponent){
					List<TextModelItem>list = (List<TextModelItem>)targetProperty.GetValue(targetComponent);
					if(Selection.gameObjects.Length > 0){
						GameObject[] arrays = Selection.gameObjects;
						List<TextModel> model = new List<TextModel>();
						for( int pos = 0; pos < arrays.Length; pos++ ){
							TextModel item = arrays[pos].transform.GetComponent<TextModel>();
							if( null != item ){
								model.Add(item);
							}
						}
						if(model.Count > 0){
							if(CreateSpaceButton("Add TextModel")){

								for(int pos = 0; pos < model.Count; pos++){
									bool isExist = false;
									for(int listLoop = 0; listLoop < list.Count; listLoop++ ){
										if(list[listLoop].modelText.Equals(model[pos].GetComponent<TMPro.TextMeshProUGUI>())){
											isExist = true;
											list[listLoop].xmlCmd = model[pos].xmlCmd;
										}
									}
									if(!isExist){
										TextModelItem modelitem = new TextModelItem();
										modelitem.modelText = model[pos].GetComponent<TMPro.TextMeshProUGUI>();
										modelitem.xmlCmd = model[pos].xmlCmd;
										list.Add(modelitem);
									}
									Undo.DestroyObjectImmediate(model[pos]);
									GameObject.DestroyImmediate(model[pos]);
								}
							}
						}else{
							CreateMessageField("Selected TextMeshProUGUI Components", MessageType.None);
						}
					}
				}
#endif
            }
		}
	}
#if TINYTIME	
	void GetElements(List<TutorialModel> list, string filepath){
		xmlDoc = new XmlDocument();
	
		XmlElement xmlRoot = xmlDoc.CreateElement ("root");
	
		for( int pos = 0; pos < list.Count; pos++ ){
			xmlRoot.AppendChild(GetElement(list[pos]));
		}
		
		xmlDoc.AppendChild(xmlRoot);
		xmlDoc.Save(filepath);
		
		AssetDatabase.SaveAssets();	
		AssetDatabase.Refresh();
		
		
	}
	
	XmlElement GetElement(TutorialModel model){
		XmlElement xmlElement = xmlDoc.CreateElement ("TutorialModel");
//		xmlElement.SetAttribute("title", model.title);
//		xmlElement.SetAttribute("
		FieldInfo[] fields = model.GetType().GetFields();
		for( int pos = 0; pos < fields.Length; pos++ ){
			FieldInfo fileInfo = fields[pos];
			if(fileInfo.FieldType == typeof(GameObject)){
				GameObject go = fileInfo.GetValue(model) as GameObject;
				if( null != go )
				xmlElement.SetAttribute(fileInfo.Name, GetRelativePath(go.transform));
			}
			else if(fileInfo.FieldType.BaseType != typeof(System.Array) )
				xmlElement.SetAttribute(fileInfo.Name, fileInfo.GetValue(model).ToString());
			else{
//				xmlElement.SetAttribute(fileInfo.Name, fileInfo.GetValue(model).ToString());
				System.Array array = fileInfo.GetValue(model) as System.Array;
				XmlElement child = CreateElementFromArray(fileInfo.GetValue(model) as System.Array, fileInfo.Name);
				xmlElement.AppendChild(child);
			}
		}
		return xmlElement;
	}
#endif
	XmlElement CreateElementFromArray(System.Array array, string fieldname){
		XmlElement xmlElement = xmlDoc.CreateElement(fieldname);
		if(array.GetType() == typeof(System.Single[])){ 
			System.Single[] floats = array as float[];
			for( int i = 0; i < floats.Length; i++ ){
				
				xmlElement.SetAttribute("array" + i.ToString(), floats[i].ToString());
			}
			
			
		}
		else if(array.GetType() == typeof(System.String[])){ 
			System.String[] strings = array as System.String[];
			for( int i = 0; i < strings.Length; i++ ){
				
				xmlElement.SetAttribute("array" + i.ToString(), strings[i]);
			}
		}
		return xmlElement;
	}
	
	void OnSelectionChange(){
		//TODO List
		Repaint();
	}
	
	public string GetRelativePath(Transform targetGO){
		string str = "";
		
		Transform sParent = targetGO.transform;
		while(true){
			if(sParent.parent != null){
				str = "/" + sParent.name + str;
				sParent = sParent.parent;
			}else{
				str =  sParent.name + str;
				break;
			}
		}
		return str;
	}

	
}