using UnityEngine;
using System.Collections;
using UnityEditor;

public class QuickSetComponent : XBaseWindow {

	[MenuItem ("Wuxingogo/Wuxingogo QuickSetComponent")]
	static void init () {
		InitWindow<XAnimationExtension>();
	}

	[MenuItem ("Wuxingogo/Wuxingogo CopyTransfrom &2")]
	static void CopyTransfrom(){
		var go = Selection.gameObjects;
		if(go != null && go.Length > 0){
			Transform trans = go[0].GetComponent<Transform>();
			m_position = trans.position;
			m_rotation = trans.eulerAngles;
			m_scale = trans.localScale;
		}
	}

	private static Vector3 m_position;
	private static Vector3 m_rotation;
	private static Vector3 m_scale;

	[MenuItem ("Wuxingogo/Wuxingogo ParseTransfrom &3")]
	static void ParseTransfrom(){
		var go = Selection.gameObjects;
		if(go != null && go.Length > 0){
			
			Transform trans = go[0].GetComponent<Transform>();
			trans.position = m_position;
			trans.eulerAngles = m_rotation;
			trans.localScale = m_scale;
		}
	}
}
