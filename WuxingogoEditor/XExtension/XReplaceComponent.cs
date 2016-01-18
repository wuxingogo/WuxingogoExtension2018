using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.Reflection;
/**
 * [XReplaceComponent 替换代码中的Component]
 * @author wuxingogo
 */

public class XReplaceComponent : XBaseWindow 
{
	MonoBehaviour monobeaviour;
	Component originalObj;
	Component replaceObj;

	public string SavePath{
		get;
		private set;
	}

	[MenuItem ("Wuxingogo/Wuxingogo XReplaceComponent ")]
	static void init () {
		InitWindow<XReplaceComponent>();
	}

	public override void OnXGUI(){

		monobeaviour = (MonoBehaviour)CreateObjectField("MonoBehaviour", monobeaviour, typeof(MonoBehaviour));
		
		BeginHorizontal();
		originalObj = (Component)CreateObjectField("Original", originalObj, typeof(Component));
		replaceObj = (Component)CreateObjectField("Replace", replaceObj, typeof(Component));
		EndHorizontal();

		if(CreateSpaceButton("Export XML")){
			ExprotXML();
		}
		if(CreateSpaceButton("Import XML")){
			ImportXML();
		}
		CreateMessageField("1.Drag a monobeahaviour and Drag the Original Component to Object Box.", MessageType.None);
		CreateMessageField("2.Click Button \"Export\", Choose XML Save Path.", MessageType.None);
		CreateMessageField("3.Change your <origin component> to <replace component>.", MessageType.None);
		CreateMessageField(" You can use Plugin \"My window\" to batching replace.", MessageType.None);
		CreateMessageField("4.Drag the replaceObj to Object Box", MessageType.None);
		CreateMessageField("5.Change ur code at Now.(Reference Component)", MessageType.None);
		CreateMessageField("6.Click \"Import\", Select the XML.", MessageType.None);
		CreateMessageField("Sorry My English.", MessageType.None);
		CreateMessageField("(⇀‸↼‵‵)", MessageType.None);
		
	}

	public void ExprotXML(){
		if( null == monobeaviour || null == originalObj ){
			CreateNotification( "(⇀‸↼‵‵) Null");
			return;
		}

		SavePath = EditorUtility.SaveFolderPanel("Save XML", SavePath ?? Application.streamingAssetsPath, "" );

		if(null == SavePath){
			return;
		}
		SavePath += "/";

		XmlDocument xmlDoc = new XmlDocument();
		XmlElement root = xmlDoc.CreateElement(monobeaviour.name);

		System.Type targetType = monobeaviour.GetType();
		FieldInfo[] properties = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance);
		
		for(int pos = 0; pos < properties.Length; pos++)
		{
			if(properties[pos].FieldType.Equals(originalObj.GetType())){	
				Component __target = properties[pos].GetValue(monobeaviour) as Component;
				if( __target != null ){
					XmlElement item = xmlDoc.CreateElement(properties[pos].Name);
					item.SetAttribute("object", __target.gameObject.GetInstanceID().ToString() );
					root.AppendChild(item);
				}	
			}			
		}
		xmlDoc.AppendChild(root);
		xmlDoc.Save(SavePath + monobeaviour.name + ".xml");
	}

	public void ImportXML(){
		if( null == monobeaviour || null == replaceObj ){
			CreateNotification( "(⇀‸↼‵‵) Null");
			return;
		}

		string filePath = EditorUtility.OpenFilePanel("Open XML", SavePath ?? Application.streamingAssetsPath, "" );
		if( null == filePath ){
			Debug.Log("error by wuxingogo");
			return;
		}

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(filePath);
		XmlElement root = (XmlElement)xmlDoc.SelectSingleNode(monobeaviour.name);
		
		System.Type targetType = monobeaviour.GetType();
		FieldInfo[] properties = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance);

		for(int pos = 0; pos < properties.Length; pos++)
		{
			
			if(properties[pos].FieldType.Equals(replaceObj.GetType())){
				
				XmlElement item = (XmlElement)root.SelectSingleNode(properties[pos].Name);
				
				if( item != null){
					int __instanceID = int.Parse( item.GetAttribute("object") );
					GameObject __target = EditorUtility.InstanceIDToObject(__instanceID) as GameObject;
					Component __component = __target.GetComponent(replaceObj.GetType());
					properties[pos].SetValue(monobeaviour, __component);
				}
			}
		}



	}

	



}