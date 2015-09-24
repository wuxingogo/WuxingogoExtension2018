using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(XMonoBehaviour), true)]


public class XMonoBehaviourEditor : XBaseEditor 
{
//	List<int> lInts = new List<int>();
//	bool isDirty = true;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
//		if( isDirty ){
//			isDirty = false;
//		}
//		lInts.Clear();
		
////		Types type = target.GetType();
		foreach( var info in target.GetType().GetMethods() ){
//			CreateSpaceButton(info.Name);
			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
//				Debug.Log(att.ToString());
				if(CreateSpaceButton(info.Name)){
					ParameterInfo[] paras = info.GetParameters();
					foreach( var p in paras ){
						if( p.GetType() == typeof(System.Int64) ){
//							CreateIntField()
						}
					}
					info.Invoke( target, null );
				}
			}
		}
		
//		Debug.Log("aaa");
	}
}