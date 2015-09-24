using UnityEngine;
using System.Collections;
using UnityEditor;

/**
 * [XBaseWindow 基础类]
 * @type {[◑▂◑]}
 */
public class XBaseWindow : EditorWindow {

	private Vector2 _scrollPos = Vector2.zero;
	const int Xoffset = 5;
	const int XButtonWidth = 100;
	const int XButtonHeight = 20;
	
	// [MenuItem ("Wuxingogo/Wuxingogo Window %#2")]
	// static void init () {
	// 	XBaseWindow window = (XBaseWindow)EditorWindow.GetWindow (typeof (XBaseWindow));	
	// }
	
	public void OnGUI(){
		GUILayout.Box(XResources.LogoTexture,GUILayout.Width(this.position.width - Xoffset), GUILayout.Height(100));
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos); 

		OnXGUI();

		EditorGUILayout.EndScrollView();
	}

	public virtual void OnXGUI(){
		
	}
	
	public void CreateSpaceBox(){
		GUILayout.Box("",GUILayout.Width(this.position.width - Xoffset), GUILayout.Height(3));
	}
	
	public bool CreateSpaceButton(string btnName, float width = XButtonWidth, float height = XButtonHeight){
		return GUILayout.Button(btnName,  GUILayout.ExpandWidth(true), GUILayout.Height(height) );
//		return GUILayout.Button (btnName, EditorStyles.miniButtonMid, GUILayout.Width(50f));
	}
	
	public Object CreateObjectField(string fieldName, Object obj, System.Type type = null ){
		if(null == type) type = typeof(Object);
		return EditorGUILayout.ObjectField(fieldName, obj, type, true ) as Object;
	}
	
	public bool CreateCheckBox(string fieldName, bool value){
		return EditorGUILayout.Toggle(fieldName, value);
	}
	public bool CreateCheckBox(bool value){
		return EditorGUILayout.Toggle(value);
	}
	
	public float CreateFloatField(string fieldName, float value){
		return EditorGUILayout.FloatField(fieldName,value);
	}
	public float CreateFloatField(float value){
		return EditorGUILayout.FloatField(value);
	}
	
	public int CreateIntField(string fieldName, int value){
		return EditorGUILayout.IntField(fieldName, value);
	}
	
	public string CreateStringField(string fieldName, string value){
		return EditorGUILayout.TextField(fieldName, value);
	}
	public string CreateStringField(string value){
		return EditorGUILayout.TextField(value);
	}
	
	public void CreateLabel(string fieldName){
		EditorGUILayout.LabelField(fieldName);
	}
	
	public void CreateMessageField(string value, MessageType type){
		EditorGUILayout.HelpBox(value,type);
		
	}
	
	public System.Enum CreateEnumSelectable(string fieldName, System.Enum value){
		return EditorGUILayout.EnumPopup(fieldName, value);
	}

	public int CreateSelectableFromString(int rootID, string[] array){
		return EditorGUILayout.Popup(array[rootID], rootID, array);
	}
	
	public void BeginHorizontal(){
		EditorGUILayout.BeginHorizontal();
	}
	public void EndHorizontal(){
		EditorGUILayout.EndHorizontal();
	}
	
	public void BeginVertical(){
		EditorGUILayout.BeginVertical();
	}
	public void EndVertical(){
		EditorGUILayout.EndVertical();
	}
	
	public void CreateNotification(string message){
		ShowNotification(new GUIContent(message));
	}
	
	
	
	
}

public enum XEditorEnum{
	cehua,
	programmer,
	art
}
