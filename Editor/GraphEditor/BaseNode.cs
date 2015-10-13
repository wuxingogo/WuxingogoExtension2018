using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class BaseNode : ScriptableObject {

	public BaseNode(){
		GraphType = NodeType.Base;
	}
	public Rect GraphRect;

    public BaseNode InputNode
    {
        get;
        set;
    }
    public Rect JointRect{
        get;
        set;
    }
	
	public NodeType GraphType {
		get;
		set;
	}
	public string GraphTitle = "BaseNode";

    public List<Rect> jointSet = new List<Rect>();
    public List<Rect> inputSet = new List<Rect>();
	
	public virtual void DrawGraph(int id){
		
		//  GUI.DragWindow();
		GraphTitle = EditorGUILayout.TextField(GraphTitle);
	}
	public virtual void Draw(int id){
		
        //switch(GraphType){
        //    case NodeType.Behaviour:
        //    GUI.color = Color.green;
        //    break;
        //    case NodeType.Element:
        //    GUI.color = Color.red;
        //    break;
        //    case NodeType.Base:
        //    GUI.color = Color.white;
        //    break;
        //    case NodeType.Model:
        //    break;
			
        //}
		
		GraphRect = GUI.Window(id, GraphRect, drag, GraphTitle);
	}
	
	public void drag(int id){
		DrawGraph(id);
		GUI.DragWindow();
		
	}
	public virtual void DrawCurves(){
        if( InputNode )
        {
            Rect rect1 = GetJointRect();
            Rect rect2 = InputNode.GetJointRect();

            GraphWindow.DrawNodeCurve( rect2, rect1 );

        }
	}
	
	public virtual void SetInputNode(BaseNode input, Vector2 clickPos)
	{
		
	}
	
	public virtual BaseNode GetInputNode(Vector2 pos)
	{
		return null;
	}

    public Rect GetJointRect()
    {
        Rect rect = GraphRect;
        rect.x += JointRect.x;
        rect.y += JointRect.y + JointRect.height / 2;
        rect.width = 1;
        rect.height = 1;
        return rect;
    }
	
	
	public enum NodeType{
		Model,
		Behaviour,
		Element,
        Condition,
		Base
	}
}


