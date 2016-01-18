using UnityEngine;
using UnityEditor;
using System.Collections;

public class XQucickSetPrefs : XBaseWindow {

	string prefsCMD = null;
	int IntValue = 0;
	float FloatValue = 0.0f;
	string StringValue = "";
	
	[MenuItem ("Wuxingogo/Wuxingogo Quick Set Prefs ")]
	static void init () {
		
		InitWindow<XQucickSetPrefs>();
		
		
	}
	
	public override void OnXGUI(){
		
		
		CreateSpaceBox();
		
		prefsCMD = CreateStringField( "prefsCMD", prefsCMD );
		
		EditorGUILayout.BeginHorizontal();
		IntValue = CreateIntField( "Int Value", IntValue);
		if(CreateSpaceButton("Apply Int Value")){
			PlayerPrefs.SetInt(prefsCMD, IntValue);
		}
		if(CreateSpaceButton("Get Int Value")){
			IntValue = PlayerPrefs.GetInt(prefsCMD, 0);
		}
		EditorGUILayout.EndHorizontal();
		
		
		EditorGUILayout.BeginHorizontal();
		FloatValue = CreateFloatField( "Float Value", FloatValue );
		if(CreateSpaceButton("Apply Float Value") ){
			PlayerPrefs.SetFloat(prefsCMD, FloatValue);
		}
		if(CreateSpaceButton("Get Float Value")){
			FloatValue = PlayerPrefs.GetFloat(prefsCMD, 0.0f);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		StringValue = CreateStringField( "String Value", StringValue );
		if(CreateSpaceButton("Apply String Value")){
			PlayerPrefs.SetString(prefsCMD, StringValue);
		}
		if(CreateSpaceButton("Get String Value")){
			StringValue = PlayerPrefs.GetString(prefsCMD, "");
		}
		EditorGUILayout.EndHorizontal();
		
		CreateSpaceBox();
		
		if(CreateSpaceButton("Clean All Prefs")){
			PlayerPrefs.DeleteAll();
		}
		
		CreateSpaceBox();
		
		CreateMessageField("This is Plugin for Unity to quick set up playerPrefs.",MessageType.Info);
		CreateMessageField("The \'prefs CDM\' is the key in the prefs file.", MessageType.Info);
		CreateMessageField("Keep mind the Delete All prefs", MessageType.Info);
		CreateMessageField("Author by wuxingogo.", MessageType.None);
	}
}
