using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class XSelectionExtension {

	static List<Transform> allChilds = new List<Transform>();
	static List<Component> allComponents = new List<Component>();
	public static void Clear(){
		allChilds.Clear();
		allComponents.Clear();
	}
	public static Transform[] GetAllChild(Transform transformForSearch){
		
		
		
		for (int pos = 0; pos < transformForSearch.childCount; pos++) {
			Transform transChild = transformForSearch.GetChild(pos);
			allChilds.Add(transChild);
			
			if(transChild.childCount > 0){
				GetAllChild(transChild);
			}	
		}
		return allChilds.ToArray();
	}
	
	
	
	public static Component[] GetAllChildComponents(Transform transformForSearch, System.Type FilterComponent){
		
		for (int pos = 0; pos < transformForSearch.childCount; pos++) {
			Transform transChild = transformForSearch.GetChild(pos);
			
//			var conponent = transChild.GetComponent(FilterComponent);
			var conponent = transChild.GetComponent<ParticleSystem>();
			if(null != conponent)
				allComponents.Add(conponent);
			
			if(transChild.childCount > 0){
				GetAllChildComponents(transChild,FilterComponent);
			}	
		}
		return allComponents.ToArray();
	}
	
//	public static 
	
}
