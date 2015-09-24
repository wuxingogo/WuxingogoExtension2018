using UnityEngine;
using UnityEditor;
using System.Collections;
public class XBehaviourWindows : XBaseWindow 
{

	private XBehaviourMode _currentMode;

	public string[] chooses = new string[]{
		"aaaa",
		"bbbb",
		"cccc"
	};
	public int id = 0;

	[MenuItem ("Wuxingogo/Wuxingogo XBehaviourWindows ")]
	static void init () {
		XBehaviourWindows window = (XBehaviourWindows)EditorWindow.GetWindow (typeof (XBehaviourWindows ) );
	}

	public override void OnXGUI(){
		//TODO List
		_currentMode = (XBehaviourMode)CreateEnumSelectable("Mode", _currentMode);

		id = CreateSelectableFromString(id, chooses);
		switch(_currentMode){
			case XBehaviourMode.PushData:
			// CreateMessageField("aa", MessageType.None);
			// EditorGUILayout.BoundsField(bounds);
			break;
			case XBehaviourMode.CheckData:
			break;
		}
	}


	void OnSelectionChange(){
		//TODO List

	}

	internal enum XBehaviourMode{
		PushData,
		CheckData
	}


}