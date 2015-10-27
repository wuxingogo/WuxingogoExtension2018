using UnityEngine;
using UnityEditor;
using System.Collections;
public class XHierarchy : XBaseWindow 
{
	ComponentType _componentType = ComponentType.monobehaviour;	
	[MenuItem ("Wuxingogo/Wuxingogo XHierarchy %#F1")]
	static void init () {
		XHierarchy window = (XHierarchy)EditorWindow.GetWindow (typeof (XHierarchy ) );
	}

	public override void OnXGUI(){
		//TODO List
		_componentType = (ComponentType)CreateEnumSelectable("component type", _componentType);
	}

	void OnSelectionChange(){
		//TODO List
		
	}

	internal enum ComponentType{
		image,
		textmeshpro,
		text,
		monobehaviour,
		recttransform,
		meshcomponent,
		collider
	}
}