using UnityEngine;
using UnityEditor;
using System.Collections;
public class XShaderExtension : XBaseWindow 
{
	GameObject[] selectedObjects;
	private string ShaderName{
		set;
		get;
	}
	[MenuItem ("Wuxingogo/Wuxingogo XShaderExtension ")]
	static void init () {
		XShaderExtension window = (XShaderExtension)EditorWindow.GetWindow (typeof (XShaderExtension ) );
	}

	public override void OnXGUI(){
		//TODO List
		selectedObjects = Selection.gameObjects;
		
		BeginHorizontal();
		ShaderName = CreateStringField("ShaderName", ShaderName);
		if( CreateSpaceButton("Mulit Material Instend Shader") && ShaderName != null ){
			Undo.RecordObjects(selectedObjects, "Mulit Material Instend Shader");
			for( int pos = 0; pos < selectedObjects.Length; pos++ ){
				Renderer renderer = selectedObjects[pos].GetComponent<Renderer>();
				if( null != renderer ){
					Material[] materials = renderer.sharedMaterials;
					for( int idx = 0; idx < materials.Length; idx++ ){
						materials[idx].shader = Shader.Find(ShaderName) ?? Shader.Find("Diffuse");
					}
				}
			}
		}
		
		
		EndHorizontal();
		
	}

	void OnSelectionChange(){
		//TODO List

	}
}