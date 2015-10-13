
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementNode : BaseNode {


   
	Rect InputNodeRect{
		get;
		set;
	}
	public ElementNode() : base() 
    {
		GraphTitle = "Element Node";
		GraphType = NodeType.Element;
	}
	UnityEngine.Object m_Object = null;
	string strType = "None";
	
	
	public override void DrawGraph(int id){
		base.DrawGraph(id);
		
		Event e = Event.current;

		m_Object = EditorGUILayout.ObjectField(m_Object, typeof(UnityEngine.Object));
		if(e.type == EventType.Repaint)
		{
			//JointRect = GUILayoutUtility.GetLastRect(); 
		}	
	}


  
	
    //public override void DrawCurves(){
		
    //}
	
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
			InputNode = input;
		}
	}
	
}
