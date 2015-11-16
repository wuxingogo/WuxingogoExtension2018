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

	[MenuItem( "Wuxingogo/Wuxingogo XReflectionWindow " )]
	static void Init()
	{
		Init<XReflectionWindow>();
	}
	
	
	private Object target = null;
	private object targetObject = null;
	private readonly string[] reflecteType = {
		"Field",
		"Property",
		"Method",
		"Member"
	};
	private string type = string.Empty;
	
	public Dictionary<string, List<string>> content = null;
	public BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

	public override void OnXGUI()
	{
		base.OnXGUI();
		
		target = CreateObjectField("target", target);
		
		if( null != target ){
			CreateLabel("Type", target.GetType().ToString());
			for (int pos = 0; pos < reflecteType.Length; pos++) {
				//  TODO loop in reflecteType.Length
				AddButton<string>(reflecteType[pos], CallBack, reflecteType[pos]);
			}
			
			
		}
		if( null != targetObject ){
			CreateLabel(targetObject.GetType().ToString(), targetObject.ToString());
			
		}
		
		if( null != content){
			bindingAttr = (BindingFlags)CreateEnumPopup("BindingFlags", bindingAttr);
//			for (int pos = 0; pos < content.Count; pos++) {
//				//  TODO loop in content.Count
//				AddButton<string>(content[pos], GetValue, content[pos]);
//			}
			foreach (var item in content) {
				List<string> list = item.Value;
				for (int pos = 0; pos < list.Count; pos++) {
					//  TODO loop in item.Value.Count
//					AddButton<string>("||" + list[pos] + "||", GetValue, list[pos]);
					if(CreateSpaceButton("||" + list[pos] + "||")){
						GetValue(item.Key);
						return;
					}
				}
				list = null;
//				AddButton<string>(item.Key, GetValue, item.Key);
			}
		}
		
	}
	
	void CallBack(string type){
		content = new Dictionary<string, List<string>>();
		this.type = type;
		switch(type){
			case "Field":
				GetStringFromArray(target.GetType().GetFields(bindingAttr));
				break;
			case "Property":
				GetStringFromArray(target.GetType().GetProperties(bindingAttr));
				break;
			case "Method":
				GetStringFromArray(target.GetType().GetMethods(bindingAttr));
				break;;
			case "Member":
				GetStringFromArray(target.GetType().GetMembers(bindingAttr));
				break;
		}
		targetObject = target;
	}
	
	void GetValue(string std){
		
		switch(type){
			case "Field":
				targetObject = targetObject.GetType().GetField(std).GetValue(targetObject);
				GetStringFromInstance(target.GetType().GetField(std));
				break;
			case "Property":
				targetObject = targetObject.GetType().GetProperty(std).GetValue(targetObject, null);
				GetStringFromInstance(target.GetType().GetProperty(std));
				break;
			case "Method":
//				targetObject = targetObject.GetType().GetMethod(std).GetValue(targetObject, null);
				GetStringFromInstance(target.GetType().GetMethod(std));
				break;;
			case "Member":
				targetObject = targetObject.GetType().GetMember(std).GetValue(0);
				GetStringFromArray(target.GetType().GetMember(std));
				break;
		}
		this.Repaint();
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
	
	void GetTypeFromObject(){
		
	}

}