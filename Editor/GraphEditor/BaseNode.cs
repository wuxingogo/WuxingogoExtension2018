using UnityEngine;
using System.Collections;
using UnityEditor;

public class BaseNode : ScriptableObject {

	public BaseNode(){
		GraphType = NodeType.Base;
	}
	public Rect GraphRect;
	
	public NodeType GraphType {
		get;
		set;
	}
	public string GraphTitle = "BaseNode";
	
	public virtual void DrawGraph(int id){
		
		//  GUI.DragWindow();
		GraphTitle = EditorGUILayout.TextField(GraphTitle);
	}
	public virtual void Draw(int id){
		
		switch(GraphType){
			case NodeType.Behaviour:
			GUI.color = Color.green;
			break;
			case NodeType.Element:
			GUI.color = Color.red;
			break;
			case NodeType.Base:
			GUI.color = Color.white;
			break;
			case NodeType.Model:
			break;
			
		}
		
		GraphRect = GUI.Window(id, GraphRect, drag, GraphTitle);
	}
	
	public void drag(int id){
		DrawGraph(id);
		GUI.DragWindow();
		
	}
	public virtual void DrawCurves(){
		
	}
	
	public virtual void SetInputNode(BaseNode input, Vector2 clickPos)
	{
		
	}
	
	public virtual BaseNode GetInputNode(Vector2 pos)
	{
		return null;
	}
	
	public enum NodeType{
		Model,
		Behaviour,
		Element,
		Base
	}
}


