using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(XData)), CanEditMultipleObjects]
public class XDataEditor : XBaseEditor 
{

	XData data;
	int Arraylength = 0;

	public override void OnInspectorGUI()
	{
		Init();
//		base.OnInspectorGUI();
		
		GUI.changed = false;
		if (Event.current.type == EventType.Layout)
		{
			return;
		}
		
		data = target as XData;
		
//		CurrHeight = EditorGUI.FloatField(new Rect(OffsetX, 45, Screen.width - 10, EditorGUIUtility.singleLineHeight), "CurrHeight", CurrHeight);
		
//		CurrHeight = 0;
		
//		Arraylength = CreateIntField("ArrayLength", Arraylength);
		if(data.Array != null)
			Arraylength = data.Array.Length;
		Arraylength = CreateGUIInt("New Array Length",Arraylength);
		
//		if(CreateGUIButton("asdf")){
//			
//		}

		for( int pos = 0; pos < data.Array.Length; pos++ ){
			
			if(CreateGUIButton("AAA")){
				Debug.Log("pos is " + pos );
			}
		}
		
		
		if( GUI.changed ){
//			Debug.Log(data.m_list.Count);
//			AssetDatabase.StartAssetEditing();
			EditorUtility.SetDirty(data);
//			AssetDatabase.StopAssetEditing();
			AssetDatabase.SaveAssets();
		}
//		Debug.Log(data.xxx.aaa);
		
	}
	
	public void ChangeArray(){
		data.Array = new XDataModel[Arraylength];
	}
	

}