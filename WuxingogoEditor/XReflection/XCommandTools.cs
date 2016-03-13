using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Linq;


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

	Type type;
	object currValue = null;

	[MenuItem( "Wuxingogo/Reflection/Wuxingogo XCommandTools" )]
	static void init()
	{
		InitWindow<XCommandTools>();
		
	}

	public override void OnXGUI()
	{
		
		command = CreateStringField( "Input ur command : ", command );
	
		if( Event.current != null && Event.current.isKey ) {
			Listen();
			string[] paras = command.Split( '.' );
			

			switch( paras.Length ) {
				case 1:
					isClassIntent = false;
					isMethodIntent = false;
					isFieldIntent = false;
					searchCollection = TryGetClass( paras[0] );
				break;
				case 2:
					if( !isClassIntent ) {	
						type = XReflectionUtils.TryGetClass( paras[0] );
						isClassIntent = true;
						isMethodIntent = false;
						isFieldIntent = false;
					}
					searchCollection = TryGetMember( paras[1], true );
				break;
				case 3:
					if( !isMethodIntent ) {
//						object instance = TryInvokeGlobalFunction( paras[1] );
//						type = instance.GetType();
//						isMethodIntent = true;
					}
					searchCollection = TryGetMember( paras[2], false );
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
			GUI.SetNextControlName( searchCollection[pos] );
			DoButton( searchCollection[pos], () => { 
				command = searchCollection[pos];
				GUI.FocusControl( "" );
				searchCollection.Clear();
				return;
			} );
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
					entry.Replace( "<", "" )
					.Replace( ">", "" )
					.Replace( "\t", "" )
					.Replace( "\n", "" )
					.Replace( "(", "" )
					.Replace( ")", "" )
					.Replace( "$", "" );
					result.Add( types[idx].Name );
				}

			}
		}
		return result;
	}

	object TryInvokeGlobalFunction(string memberName, List<string> paraStr)
	{
		string[] array = paraStr.ToArray();
		object[] paras = new object[array.Length];
		for( int pos = 0; pos < array.Length; pos++ ) {
			//  TODO loop in array.Length
			var str = array[pos];
			if( str.Contains( "\"" ) ) {
				str = str.Replace( "\"", "" );
				paras[pos] = str;
			} else {
				paras[pos] = float.Parse( str );
			}
		}
		return type.TryInvokeGlobalMethod( memberName, paras );
	}

	public List<string> TryGetMember(string memberName, bool isStatic)
	{
		string[] array = memberName.Split( '(', ')' );

		List<string> result = new List<string>();
		if( type == null )
			return result;
		MemberInfo[] memberCollection = type.MemberMatch( array[0], isStatic );
		for( int pos = 0; pos < memberCollection.Length; pos++ ) {
			//  TODO loop in memberCollection.Length
			result.Add( memberCollection[pos].Name );
		}
		return result;
	}

	void EmptyCommand()
	{
		
		command = "";
		type = null;
		currValue = null;

	}

	void ExcuteCommand()
	{
//		object obj = XReflectionManager.GetValue( command );
//		if( null != obj )
//			Debug.Log( command + " is : " + obj.ToString() );
//		else
//			Debug.Log( "excute : " + command );

//		string[] paras = command.Split( '(', ')' );
//		List<string> parasList = new List<string>();
//		if( paras.Length > 1 ) {
//			parasList = paras.ToList();
//			parasList.RemoveAt( 0 );
//			parasList.ForEach( (t) => {
//				if( t == null || t == "" )
//					parasList.Remove( t );
//			} );
//		}
//
//
//		string[] cmd = paras[0].Split( '.' );
//		type = XReflectionUtils.TryGetClass( cmd[0] );
//		Debug.Log( TryInvokeGlobalFunction( cmd[1], parasList ) );
		MatchPara();
		
	}

	void MatchPara()
	{
		var functions = command.RegexCutString( "(", ")" );
		var clear = command.RegexCutStringReverse( "(", ")" );

		string[] commandPara = clear.Split( '.' );
		if( commandPara.Length > 0 && type == null )
			type = XReflectionUtils.TryGetClass( commandPara[0] );
		int funCount = functions.Length;
		for( int i = 1; i < funCount; i++ ) {
			//  TODO loop in funCount
			if( currValue == null ) {
				currValue = type.TryInvokeGlobalMethod( commandPara[i] );
			} else {
				currValue = currValue.GetType().TryInvokeMethod( currValue, commandPara[i] );
			}
		}
		Debug.Log( "currValue is : " + currValue.ToString() );
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