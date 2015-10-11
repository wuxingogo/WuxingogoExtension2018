
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementNode : BaseNode {

	public ElementNode InputNode{
		get;
		set;
	}
	Rect InputNodeRect{
		get;
		set;
	}
	public ElementNode(){
		GraphTitle = "Element Node";
		GraphType = NodeType.Element;
	}
	public Rect JointRect{
		get;
		set;
	}
	
	UnityEngine.Object m_Object = null;
	string strType = "None";
	
	
	public override void DrawGraph(int id){
		base.DrawGraph(id);
		
		Event e = Event.current;
		
		
		
		m_Object = EditorGUILayout.ObjectField(m_Object, typeof(UnityEngine.Object));
		if(e.type == EventType.Repaint)
		{
			JointRect = GUILayoutUtility.GetLastRect(); 
		}	
	}
	
	
	
	
	
	public override void DrawCurves(){
		if(InputNode){
			Rect rect = GraphRect;
			rect.x += JointRect.x;
			rect.y += JointRect.y + JointRect.height / 2;
			rect.width = 1;
			rect.height = 1;
			
			Rect rect2 = InputNode.GraphRect;
			rect2.x += InputNode.JointRect.x;
			rect2.y += InputNode.JointRect.y + InputNode.JointRect.height / 2;
			rect2.width = 1;
			rect2.height = 1;
			
			GraphWindow.DrawNodeCurve(rect2, rect);
			
		}
	}
	
	public override BaseNode GetInputNode(Vector2 pos)
	{
		BaseNode retVal = null;

		pos.x -= GraphRect.x;
		pos.y -= GraphRect.y;

		if(InputNodeRect.Contains(pos))
		{
			retVal = InputNode;
			InputNode = null;
		}

		return retVal;
	}
	
	public override void SetInputNode(BaseNode input, Vector2 clickPos)
	{
//		clickPos.x -= GraphRect.x;
//		clickPos.y -= GraphRect.y;

		if(GraphRect.Contains(clickPos))
		{
			InputNode = (ElementNode)input;
		}
	}
	
}
