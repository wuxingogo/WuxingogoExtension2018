using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class BaseNode : ScriptableObject {

	public BaseNode(){
		GraphType = NodeType.Base;
        GraphRect = new Rect( 0, 0, 200, 150 );
        
	}
	public Rect GraphRect;

    public BaseNode InputNode
    {
        get;
        set;
    }
    public Rect JointRect{
        get
        {
            return jointRect = new Rect( GraphRect.width - 10, GraphRect.height / 2, 20, 20 );
        }
    }
    private Rect jointRect;
	
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

        if( GUI.Button( JointRect, "", EditorStyles.toolbarDropDown ) )
        {
            GraphWindow.GetInstance().SetTransition( this );
        }
        
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

    public void SetGraphPosition(float x, float y){
        GraphRect.x = x;
        GraphRect.y = y;
    }
	
	public enum NodeType{
		Model,
		Behaviour,
		Element,
        Condition,
		Base
	}
}


