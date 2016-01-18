//XReflectionWindow.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 11/15/2015 20:50:27 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;


public class XReflectionWindow : XBaseWindow {

	[MenuItem( "Wuxingogo/Reflection/Wuxingogo XReflectionWindow " )]
	static void Init()
	{
		InitWindow<XReflectionWindow>();
	}
	
	
	private object _target = null;
	public object Target {
		get{
			return _target;
		}
		set{
			if(_target != value){
			
				_target = value;
				OnChangeTarget();
			}
		}
	}

	public override object[] closeRecordArgs {
		get {
			return new object[]{ Target };
		}
	}
	public override void OnInitialization(params object[] args)
	{
		if(args.Length > 0)
			Target = args[0];
	}

	private Stack<object> storeTargets = new Stack<object>();
	
	
	private Object uObject = null;
	private readonly string[] reflecteType = {
		"Field",
		"Property",
		"Method",
		"Member"
	};
	private string strType = string.Empty;
	
	public Dictionary<string, List<string>> content = new Dictionary<string, List<string>>();

	public BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

	public override void OnXGUI()
	{
		base.OnXGUI();

		if(CreateSpaceButton("Clean")){
			uObject = null;
			Target = null;
		}
		
		if( null == Target ){
			uObject = CreateObjectField("", uObject);
			Target = uObject;
		}else{
			CreateLabel(Target.GetType().ToString() + "||"  + Target.ToString());
			
			if(storeTargets.Count > 0)
				DoButton("Back To Last Object", BackToLastObject);
			
			for (int pos = 0; pos < reflecteType.Length; pos++) {
				//  TODO loop in reflecteType.Length
				BeginHorizontal();
				DoButton<string>(reflecteType[pos], OnClickType, reflecteType[pos]);
				DoButton<string>("Open " + reflecteType[pos] + " Editor", OnOpenEditor, reflecteType[pos]);
				EndHorizontal();
			}
			
			
			if( null != content){
				bindingAttr = (BindingFlags)CreateEnumPopup("BindingFlags", bindingAttr);
				foreach (var item in content) {
					List<string> list = item.Value;
					for (int pos = 0; pos < list.Count; pos++) {
						//  TODO loop in item.Value.Count
						//					AddButton<string>("||" + list[pos] + "||", GetValue, list[pos]);
						if(CreateSpaceButton("||" + list[pos] + "||")){
							OnClickMemberInfo(item.Key);
							return;
						}
					}
					list = null;
					
				}
			}
		}
	}
	
	void BackToLastObject(){
		
		Target = storeTargets.Pop();
	}
	
	public void OnChangeTarget(){
		if( Target != null)
			Debug.Log("Target is Type is " + Target.GetType().ToString());
		
		content.Clear();
		
		if(Target != null) storeTargets.Push(Target);
		
		this.Repaint();
	}
	
	void OnClickType(string strType){
		this.strType = strType;
		switch( strType ) {
			case "Field":
				GetStringFromArray(Target.GetType().GetFields(bindingAttr));
				break;
			case "Property":
				GetStringFromArray(Target.GetType().GetProperties(bindingAttr));
				break;
			case "Method":
				GetStringFromArray(Target.GetType().GetMethods(bindingAttr));
				break;;
			case "Member":
				GetStringFromArray(Target.GetType().GetMembers(bindingAttr));
				break;	
			
		}
	}
	
	void OnClickMemberInfo(string std){
		
		switch(strType){
			case "Field":
				Target = Target.GetType().GetField(std).GetValue(Target);
				break;
			case "Property":
				Target = Target.GetType().GetProperty(std).GetValue(Target, null);
				break;
			case "Method":
				Target = Target.GetType().GetMethod(std).Invoke(Target, null);
				break;;
			case "Member":
				Target = Target.GetType().GetMember(std).GetValue(0);
				break;
		}
		
		Debug.Log("Target is " + Target.ToString());
	}
	
	void OnOpenEditor(string editorName){
		switch( editorName ) {
			case "Field":
				XFieldWindow fieldWindow = InitWindow<XFieldWindow>();
				fieldWindow.Target = Target;
				break;
			case "Property":
				XPropertyWindow propertyWindow = InitWindow<XPropertyWindow>();
				propertyWindow.Target = Target;
				break;
			case "Method":
				XMethodWindow methodWindow = InitWindow<XMethodWindow>();
				methodWindow.Target = Target;
				break;;
			case "Member":
				break;	
				
		}
	}
	
	void GetStringFromArray(MemberInfo[] array){
		content.Clear();
		for( int pos = 0; pos < array.Length; pos++ ) {
			//  TODO loop in array.Length
			if(!content.ContainsKey(array[pos].Name)){
				content[array[pos].Name] = new List<string>();
			}
			content[array[pos].Name].Add(array[pos].ToString());
		} 
	}
	void GetStringFromInstance(MemberInfo info){
		content.Clear();
		if(!content.ContainsKey(info.Name)){
			content[info.Name] = new List<string>();
		}
		content[info.Name].Add(info.ToString());
	}

}