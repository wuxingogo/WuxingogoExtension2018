using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

/**
 * [XCommandTools 模仿LLDB 命令行]
 * @type {[┏(｀ー´)┛]}
 * 其实就是字符串分割 + 反射啦
 * 本来想可以设置变量也用这个的,可以到手机上来射ᕙ(⇀‸↼‵‵)ᕗ
 */
public class XCommandTools : XBaseWindow 
{
//	bool isEditorAssembly = false;
	string command = "~";
	Object obj = null;
	System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
	bool[] isCurrBool;
	Assembly currAssembly = null;
	bool isEndStart = false;
	int recordID = 0;
	[MenuItem ("Wuxingogo/Wuxingogo XCommandTools %#2")]
	static void init () {
		XCommandTools window = (XCommandTools)EditorWindow.GetWindow (typeof (XCommandTools ) );
		
	}
	
	void Start(){
		isCurrBool = new bool[assemblies.Length];
		isCurrBool[0] = true;
	}

	public override void OnXGUI(){
		//TODO List
//		if(!isEndStart){
//			isEndStart = true;
//			Start();
//		}
//		_scrollPos = GUI.BeginScrollView(
//			new Rect(0, 105, position.width, position.height),
//			_scrollPos,
//			new Rect(0, 50, 550, position.height + 550)
//			); 
		command = CreateStringField("Input ur command : ",command);
		
//		isEditorAssembly = CreateCheckBox("Is Editor Command", isEditorAssembly);
		
		
//		for (int pos = 0; pos < assemblies.Length; pos++)
//		{
//			EditorGUILayout.BeginHorizontal();
//			isCurrBool[pos] = CreateCheckBox("curr", isCurrBool[pos]);
////			Debug.Log(A.FullName);
//			CreateLabel(assemblies[pos].FullName);
//			EditorGUILayout.EndHorizontal();
//			if(isCurrBool[pos] && pos != recordID){
//				isCurrBool[recordID] = false;
//				isCurrBool[pos] = true;
//				recordID = pos;
//				currAssembly = assemblies[recordID];
//			}
//		}
//		GUI.EndScrollView();
			
		if(Event.current!=null && Event.current.isKey )
		{
			Listen();
		}
	}
	
	void ModifyCommand(){
//		command = "~" + command;
	}
	
	void EmptyCommand(){
		
		command = "";

	}

	void OnSelectionChange(){
		//TODO List
		
	}
	
	void ExcuteCommand(){

#if UNITY_4_6
        object obj = XReflectionManager.GetValue(command);
        if( null != obj )
            Debug.Log (command + " is : " + obj.ToString());
        else
            Debug.Log ("excute : " + command);
#endif

		
	}
	
	public bool Listen(){
		bool c = false;
		switch(Event.current.keyCode){
		case KeyCode.Return:
			if( "" != command)
				EditorPrefs.SetString("command_string",command);
			ExcuteCommand();
			EmptyCommand();
			Repaint ();
			c = true;
			break;
		case KeyCode.UpArrow:
			command = EditorPrefs.GetString("command_string","");
			Repaint ();
			c = true;
			break;
		case KeyCode.CapsLock:
			break;
		case KeyCode.Tab:
			break;
		case KeyCode.None:
			break;
		case KeyCode.Backspace:
//			command.Substring(0, command.Length - 2);
//			Repaint();
			break;
		
		default:
//			command += Event.current.keyCode;
//			Repaint();
//		    c = true;
			break;
		}
		return c;
	}
}