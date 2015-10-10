using UnityEngine;
using UnityEditor;
using System.Collections;
public class XMuiltHandle : XBaseWindow 
{

	enum HandleType{
		Names,
		ComponentParas,
		
	}
	HandleType _currentType = HandleType.Names;
	[MenuItem ("Wuxingogo/Wuxingogo XMuiltHandle %#2")]
	static void init () {
		XMuiltHandle window = (XMuiltHandle)EditorWindow.GetWindow (typeof (XMuiltHandle ) );
	}

	public override void OnXGUI(){
		//TODO List
		_currentType = (HandleType)CreateEnumSelectable("HandleType",_currentType);
		
	}
	
	
	void OnSelectionChange(){
		//TODO List

	}
	
	
	
}