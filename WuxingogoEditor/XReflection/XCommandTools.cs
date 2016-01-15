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
using wuxingogo.Runtime;
using wuxingogo.Reflection;
using wuxingogo.tools;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;


public class XCommandTools : XBaseWindow
{
	//	bool isEditorAssembly = false;
	string command = "~";

	bool isDirty = false;
	List<string> searchCollection = new List<string>();

	bool isClassIntent = false;
	bool isMethodIntent = false;
	bool isFieldIntent = false;

	int count = 0;

	Type type;

	[MenuItem( "Wuxingogo/Reflection/Wuxingogo XCommandTools" )]
	static void init()
	{
		Init<XCommandTools>();
		
	}

	public override void OnXGUI()
	{
		
		command = CreateStringField( "Input ur command : ", command );
	
		if( Event.current != null && Event.current.isKey ) {
			Listen();
			count = StringUtils.RegexCharCount( command, "." );
			switch( count ) {
				case 0:
					isClassIntent = false;
					isMethodIntent = false;
					isFieldIntent = false;
					searchCollection = TryGetClass( command );
				break;
				case 1:
					if( !isClassIntent ) {	
						type = XReflectionUtils.TryGetClass( command.Substring( 0, command.IndexOf( "." ) ) );
						isClassIntent = true;
						isMethodIntent = false;
						isFieldIntent = false;
					}
					searchCollection = TryGetMember( command.Substring(command.IndexOf(".") + 1) );
				break;
				case 2:
					if( !isMethodIntent ) {
						object instance = type.TryInvokeGlobalMethod( StringUtils.CutString(command, command.IndexOf(".") + 1, command.LastIndexOf( "." ) ) );
						type = instance.GetType();
						isMethodIntent = true;
					}

//					XReflectionUtils.TryGetClass( command.Substring(0, command.IndexOf(".")) );
					searchCollection = TryGetMember( command.Substring( command.LastIndexOf( "." ) + 1 ) );
				break;
				default:
				break;
			}

//			if(!isClassIntent){
//				isClassIntent = true;
//			}else{
//				
//			}
			Repaint();
		}

		for( int pos = 0; pos < searchCollection.Count; pos++ ) {
			//  TODO loop in searchCollection.Count
			GUI.SetNextControlName(searchCollection[pos]);
			DoButton( searchCollection[pos], () =>{ 
				command = searchCollection[pos];
				GUI.FocusControl("");
				searchCollection.Clear();
				return;
			});
		}
	}

	static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;

	public static List<string> TryGetClass(string className)
	{
		List<string> result = new List<string>();
		if( string.IsNullOrEmpty( className ) )
			return result;
		
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for( int pos = 0; pos < assemblies.Length; pos++ ) {
			//  TODO loop in Length
			var types = assemblies[pos].GetTypes();
			for( int idx = 0; idx < types.Length; idx++ ) {
				//  TODO loop in types.Length
				if( types[idx].Name.Contains( className ) || className.Contains( types[idx].Name ) ) {
					string entry = types[idx].Name;
					entry.Replace("<", "")
					.Replace(">", "")
					.Replace("\t", "")
					.Replace("\n", "")
					.Replace("(", "")
					.Replace(")", "")
					.Replace("$", "");
					result.Add( types[idx].Name );
				}

			}
		}
		return result;
	}

	public List<string> TryGetMember(string memberName){
		
		List<string> result = new List<string>();
		if( type == null )
			return result;
		MemberInfo[] memberCollection = type.MemberMatch( memberName, count == 1 ? true : false );
		for( int pos = 0; pos < memberCollection.Length; pos++ ) {
			//  TODO loop in memberCollection.Length
			result.Add( memberCollection[pos].Name );
		}
		return result;
	}

	void ModifyCommand()
	{
//		command = "~" + command;
	}

	void EmptyCommand()
	{
		
		command = "";

	}

	void OnSelectionChange()
	{
		//TODO List
		
	}

	void ExcuteCommand()
	{
		object obj = XReflectionManager.GetValue( command );
		if( null != obj )
			Debug.Log( command + " is : " + obj.ToString() );
		else
			Debug.Log( "excute : " + command );
		
	}

	public bool Listen()
	{
		bool c = false;
		switch( Event.current.keyCode ) {
			case KeyCode.Return:
				if( "" != command )
					EditorPrefs.SetString( "command_string", command );
				ExcuteCommand();
				EmptyCommand();
				Repaint();
				c = true;
			break;
			case KeyCode.UpArrow:
				command = EditorPrefs.GetString( "command_string", "" );
				Repaint();
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
		isDirty = true;
		Event.current.type = EventType.Layout;
		return c;
	}
}