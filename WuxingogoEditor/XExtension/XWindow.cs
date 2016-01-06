using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Xml;
#if TMP
using TMPro;
#endif

enum SeachType{
	_Name,
	_Component,
	_Behaviour
}

public class XWindow : XBaseWindow {

	
    //XDebugExtension customObject;
	Object targetObject;
	GameObject targetGO = null;

	const int offsetRihgt = 5;
	
	[MenuItem ("Wuxingogo/My Window %#6")]
	static void Init () {
		
		Init<XWindow>();
		
	}
	bool isNeedSeachObj = false;
	Object _component = null;
	string filterName = "seach";
	
	bool isNeedShowData = false;
	SerializedObject m_Object;

	bool isNeedChangeComponent = false;
	static Dictionary<string, GameObject> DChangeArray = new Dictionary<string , GameObject>();

	public override void OnXGUI () {
//		targetObject = Selection.gameObjects[0];
		
		if(GUILayout.Button ("ApplyMessage", EditorStyles.toolbarButton)){
			
			//Debug.Log(customObject._noticeTarget);
			Debug.Log("target object name is " + targetObject.name);
//			targetObject.SendMessage(customObject._message);
		}
		
		EditorGUI.BeginChangeCheck();
		isNeedSeachObj = EditorGUILayout.Foldout(isNeedSeachObj, "Filter GameObject");
		if(EditorGUI.EndChangeCheck()){	}
		
		if(isNeedSeachObj)
			paintSeachObject();
		
		EditorGUI.BeginChangeCheck();
		isNeedShowData = EditorGUILayout.Foldout(isNeedShowData, "Show Object Data");
		EditorGUI.EndChangeCheck();

		if(isNeedShowData)
			paintShowData();
			
		EditorGUI.BeginChangeCheck();
		isNeedChangeComponent = EditorGUILayout.Foldout(isNeedChangeComponent, "ChangeComponent");
		EditorGUI.EndChangeCheck();

		if(isNeedChangeComponent)
			paintChangeComponent();

//		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
//		myString = EditorGUILayout.TextField ("Text Field", myString);
//		
//		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
//		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
//		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
//		
//		EditorGUILayout.EndToggleGroup ();
	}
	
	
	void paintSeachObject(){
		// by name
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
		GUILayout.Label ("Filter GameObjects by Children 's name", EditorStyles.boldLabel);
		
		filterName = EditorGUILayout.TextField ("seach", filterName);
		if(GUILayout.Button ("Seach", EditorStyles.miniButtonMid, GUILayout.Width(50f)) && null != targetObject){
			List<GameObject> selection = new List<GameObject>();
			selection.Clear();
			GetAllChildsFilterName(targetObject as GameObject, filterName, selection);
			Selection.objects = selection.ToArray();
		}
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
		// by component
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
		GUILayout.Label ("Filter GameObjects by Children 's Component", EditorStyles.boldLabel);
		
		_component = EditorGUILayout.ObjectField(_component,typeof(Object), true) as Object;
		if(GUILayout.Button ("Seach", EditorStyles.miniButtonMid, GUILayout.Width(50f)) && null != targetObject){
			List<GameObject> selection = new List<GameObject>();
			selection.Clear();
			GetComponentFilter(targetObject as GameObject, _component, selection);
			Selection.objects = selection.ToArray();
		}
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
	}
	
	private void GetAllChildsFilterName(GameObject transformForSearch, string filter,List<GameObject> selection) {
		foreach (Transform trans in transformForSearch.transform) {
			if(trans.gameObject.name.Contains(filter))
				selection.Add(trans.gameObject);
			if(trans.childCount > 0){
				GetAllChildsFilterName(trans.gameObject, filter, selection);
			}	
		}
	}
	
	private void GetComponentFilter(GameObject transformForSearch, Object filter, List<GameObject> selection){
		
		foreach (Transform trans in transformForSearch.transform) {
			if(trans.gameObject.GetComponent(filter.GetType()) != null)
				selection.Add(trans.gameObject);
			if(trans.childCount > 0){
				GetComponentFilter(trans.gameObject, filter, selection);
			}	
		}
	}
	
	
	void paintShowData(){
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
		GUILayout.Label ("Show Object Data", EditorStyles.boldLabel);
		// if( null != m_Object )
//			Debug.Log( "m_Object is "+ m_Object.GetType().ToString() );

		targetGO = (GameObject)EditorGUILayout.ObjectField(targetGO,typeof(GameObject));
		if(GUILayout.Button ("Print Parent Message") && null != targetGO){
			
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
			Debug.Log(str);		
		}
		
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
	}
	
	void OnSelectionChange(){
//		Debug.Log("change");
//		Debug.Log( Selection.objects[0].GetType().ToString() );
		if( Selection.objects.Length > 0){
			targetObject = Selection.objects[0];
			
			m_Object = new UnityEditor.SerializedObject(targetObject);
			// Debug.Log("targetObject.name is " + targetObject.name);
		}
		
	}


	void paintChangeComponent(){
#if TMP
		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
		
		GUILayout.Label ("ChangeCompont", EditorStyles.boldLabel);
		

		if(GUILayout.Button ("UGUI TO TMP", EditorStyles.miniButtonMid, GUILayout.Width(80f)) && 0 < Selection.gameObjects.Length ){

			foreach( var tar in Selection.gameObjects){
				UnityEngine.UI.Text sText = tar.GetComponent<UnityEngine.UI.Text>();
				if( null != sText ){
					Undo.DestroyObjectImmediate(sText);
					Debug.Log( "sText text is : " + sText.text );
					

					
					TextMeshProUGUI tmp_text = tar.AddComponent<TextMeshProUGUI>();
					
					tmp_text.text = sText.text;
					tmp_text.color = sText.color;
					tmp_text.fontSize = sText.fontSize;
					// if( sText.fontStyle == FontStyle.Bold ){
					// 	tmp_text.fontStyle = FontStyles.Bold;
					// }
					tmp_text.font = Resources.Load("Font/MSYH SDF 1") as TextMeshProFont;

					switch(sText.alignment){
						case TextAnchor.LowerCenter:
						tmp_text.alignment = TMPro.TextAlignmentOptions.Bottom;
						break;
						case TextAnchor.LowerLeft:
						tmp_text.alignment = TMPro.TextAlignmentOptions.BottomLeft;
						break;
						case TextAnchor.LowerRight:
						tmp_text.alignment = TMPro.TextAlignmentOptions.BottomRight;
						break;
						case TextAnchor.MiddleCenter:
						tmp_text.alignment = TMPro.TextAlignmentOptions.Center;
						break;
						case TextAnchor.MiddleRight:
						tmp_text.alignment = TMPro.TextAlignmentOptions.MidlineRight;
						break;
						case TextAnchor.MiddleLeft:
						tmp_text.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
						break;
						case TextAnchor.UpperLeft:
						tmp_text.alignment = TMPro.TextAlignmentOptions.TopLeft;
						break;
						case TextAnchor.UpperCenter:
						tmp_text.alignment = TMPro.TextAlignmentOptions.Top;
						break;
						case TextAnchor.UpperRight:
						tmp_text.alignment = TMPro.TextAlignmentOptions.TopRight;
						break;
						default:
						Debug.LogError("instead the instance alignment has error!" + targetObject.name);
						break;
					}
					// tmp_text.alignment = TMPro.TextAlignmentOptions.Center;
					
					switch(sText.GetGenerationSettings(new Vector2(0.5f,0.5f)).verticalOverflow){
						case VerticalWrapMode.Overflow:
						tmp_text.OverflowMode = TMPro.TextOverflowModes.Overflow;
						break;
						case VerticalWrapMode.Truncate:
						tmp_text.OverflowMode = TMPro.TextOverflowModes.Truncate;
						break;
					}
					// tmp_text.OverflowMode = TMPro.TextOverflowModes.ScrollRect;
					tmp_text.enableWordWrapping = true;
					GameObject.DestroyImmediate(sText);
					
				}
			}
		}

		if(GUILayout.Button ("TMP TO UGUI", EditorStyles.miniButtonMid, GUILayout.Width(80f)) && 0 < Selection.gameObjects.Length ){

			foreach( var tar in Selection.gameObjects){
				TextMeshProUGUI sText = tar.GetComponent<TextMeshProUGUI>();
				if( null != sText ){
					Undo.DestroyObjectImmediate(sText);
					Debug.Log( "sText text is : " + sText.text );
					

					
					UnityEngine.UI.Text tmp_text = tar.AddComponent<UnityEngine.UI.Text>();
					
					tmp_text.text = sText.text;
					tmp_text.color = sText.color;
					tmp_text.fontSize = (int)sText.fontSize;
					// if( sText.fontStyle == FontStyle.Bold ){
					// 	tmp_text.fontStyle = FontStyles.Bold;
					// }
					tmp_text.font = Resources.Load("Font/fzcyjt") as Font;

					switch(sText.alignment){
						case TMPro.TextAlignmentOptions.Bottom:
						tmp_text.alignment = TextAnchor.LowerCenter;
						break;
						case TMPro.TextAlignmentOptions.BottomLeft:
						tmp_text.alignment = TextAnchor.LowerLeft;
						break;
						case TMPro.TextAlignmentOptions.BottomRight:
						tmp_text.alignment = TextAnchor.LowerRight;
						break;
						case TMPro.TextAlignmentOptions.Center:
						tmp_text.alignment = TextAnchor.MiddleCenter;
						break;
						case TMPro.TextAlignmentOptions.MidlineRight:
						tmp_text.alignment = TextAnchor.MiddleRight;
						break;
						case TMPro.TextAlignmentOptions.MidlineLeft:
						tmp_text.alignment = TextAnchor.MiddleLeft;
						break;
						case TMPro.TextAlignmentOptions.TopLeft:
						tmp_text.alignment = TextAnchor.UpperLeft;
						break;
						case TMPro.TextAlignmentOptions.Top:
						tmp_text.alignment = TextAnchor.UpperCenter;
						break;
						case TMPro.TextAlignmentOptions.TopRight:
						tmp_text.alignment = TextAnchor.UpperRight;
						break;
						default:
						Debug.LogError("instead the instance alignment has error!" + targetObject.name);
						break;
					}
					// tmp_text.alignment = TMPro.TextAlignmentOptions.Center;
					
					switch(sText.OverflowMode){
						case TMPro.TextOverflowModes.Overflow:
//						tmp_text.GetGenerationSettings().verticalOverflow = VerticalWrapMode.Overflow;
						break;
						case TMPro.TextOverflowModes.Truncate:
//						tmp_text.GetGenerationSettings().verticalOverflow = VerticalWrapMode.Truncate;
						break;
					}
					// tmp_text.OverflowMode = TMPro.TextOverflowModes.ScrollRect;
					// tmp_text.enableWordWrapping = true;
					GameObject.DestroyImmediate(sText);
					
				}
			}
		}
		

		
		_component = EditorGUILayout.ObjectField(_component,typeof(Object), true) as Object;
		if(GUILayout.Button ("Store Text Reference", EditorStyles.miniButtonMid, GUILayout.Width(120f)) && null != _component){
			System.Type comT = _component.GetType();
//			CharacteristicCtr myinstance = _component as CharacteristicCtr;
			object instance = _component;
			Debug.Log(comT.ToString());
			
			PropertyInfo[] myFields = _component.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			// Display the values of the fields.
			
			List<GameObject> gos = new List<GameObject>();
			DChangeArray.Clear();
			for(int i = 0; i < myFields.Length; i++)
			{
				if(myFields[i].PropertyType.Equals(typeof(UnityEngine.UI.Text))){	
					if(myFields[i].GetValue(instance, null) != null)
					{
						Debug.Log("The value of " + myFields[i].Name  + " is: " + myFields[i].GetValue(instance, null));
						GameObject go = ((Component)myFields[i].GetValue(instance, null)).gameObject;
						DChangeArray.Add(myFields[i].Name + "_tmp", go);
						gos.Add(go);
					}
				}
				if(myFields[i].PropertyType.Equals(typeof(System.Array))){
					Debug.Log("has a array");
				}
				
			}

			Selection.objects = gos.ToArray();
		}
		
		if(GUILayout.Button ("Find Text Reference", EditorStyles.miniButtonMid, GUILayout.Width(120f)) && null != _component){
			System.Type comT = _component.GetType();
			object myinstance = _component;
			Debug.Log(comT.ToString());
			
			FieldInfo[] myFields = _component.GetType().GetFields( BindingFlags.Public | BindingFlags.Instance );
			// Display the values of the fields.
			
			List<GameObject> gos = new List<GameObject>();
			for(int i = 0; i < myFields.Length; i++)
			{
				if(myFields[i].FieldType.Equals(typeof(TextMeshProUGUI))){
//					if(myFields[i].GetValue(myinstance) != null)
//					{
//						Debug.Log("The value of " + myFields[i].Name  + " is: " + myFields[i].GetValue(myinstance));
//						GameObject go = ((Component)myFields[i].GetValue(myinstance)).gameObject;
//						DChangeArray.Add(myFields[i].Name, go);
//						gos.Add(go);
//					}
					if(DChangeArray.ContainsKey(myFields[i].Name)){
						TextMeshProUGUI component = DChangeArray[myFields[i].Name].GetComponent<TextMeshProUGUI>();
						myFields[i].SetValue(myinstance, component);
					}
					
				}
			}
        
			Selection.objects = gos.ToArray();
		}

		GUILayout.Box("",GUILayout.Width(this.position.width - offsetRihgt), GUILayout.Height(3));
#endif
                               }

}
