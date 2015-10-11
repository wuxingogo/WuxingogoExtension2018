using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public class GraphWindow : XBaseWindow 
{
	private List<BaseNode> nodes = new List<BaseNode>();
	Vector2 mousePosition = Vector2.zero;
	public bool IsTransition{
		get;
		private set;
	}
	
	public bool IsClickNode{
		get;
		private set;
	}
	
	public int SelectedIndex{
		get;
		private set;
	}
	
	public BaseNode SelectedNode{
		get;
		private set;
	}
	
	public BaseNode InputNode{
		get;
		private set;
	}
	
	[MenuItem ("Wuxingogo/Wuxingogo GraphWindow ")]
	static void init () {
		GraphWindow window = (GraphWindow)EditorWindow.GetWindow (typeof (GraphWindow ) );
	}

    

    public override void OnXGUI(){
		//TODO List
		
		Event e = Event.current;
		mousePosition = e.mousePosition;
		
		if(e.button == 0 || e.button == 1){
			ChooseNode();
		}
		
		if(e.button == 1 && e.type == EventType.MouseUp){
			GenericMenu menu = new GenericMenu();
			if(SelectedIndex == -1){
				menu.AddItem(new GUIContent("Add Base Node"), false, ContextCallback, "AddBaseNode");
				menu.AddItem(new GUIContent("Add Element Node"), false, ContextCallback, "AddElement");
				menu.AddItem(new GUIContent("Add Behaviour Node"), false, ContextCallback, "AddBehaviour");
				menu.AddItem(new GUIContent("Add Model Node"), false, ContextCallback, "AddModel");
	
			}else{
				menu.AddItem(new GUIContent("Make Transition"),false, ContextCallback, "MakeTransition");
				menu.AddSeparator("");
				menu.AddItem(new GUIContent("Delete Node"),false, ContextCallback, "DeleteNode");
			}
			
			menu.ShowAsContext();
			e.Use();
		}
		
		else if(e.button == 0 && e.type == EventType.MouseDown && IsTransition){
			if(IsClickNode && !SelectedNode.Equals(InputNode)){
				SelectedNode.SetInputNode(InputNode, mousePosition);
				IsTransition = false;
				
			}
		}


        if (IsTransition){
            Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
            DrawNodeCurve(InputNode.GraphRect, mouseRect);
            Repaint();
        }
		
		
		BeginWindows();

		for(int i = 0; i< nodes.Count; i++){
			//  nodes[i].GraphRect = GUI.Window(i, nodes[i].GraphRect, DrawNodeGraph, nodes[i].GraphTitle);
			nodes[i].Draw(i);
			nodes[i].DrawCurves();
		}

		EndWindows();
	}
	void ContextCallback(object obj){
		string clb = obj.ToString();
		if(clb.Equals("AddBaseNode"))
		{
			BaseNode inputNode = new BaseNode();
			inputNode.GraphRect = new Rect(mousePosition.x,mousePosition.y, 200, 150);

			nodes.Add(inputNode);
		}
		else if(clb.Equals("AddElement")){
			ElementNode element = new ElementNode();
			element.GraphRect =  new Rect(mousePosition.x,mousePosition.y, 200, 150);
			nodes.Add(element);
		}
		else if(clb.Equals("AddBehaviour")){
			BehaviourNode behaviour = new BehaviourNode();
			behaviour.GraphRect =  new Rect(mousePosition.x,mousePosition.y, 200, 150);
			nodes.Add(behaviour);
		}
		else if(clb.Equals("MakeTransition")){
			InputNode = SelectedNode;
			IsTransition = true;
		}
		else if(clb.Equals("DeleteNode")){
			nodes.RemoveAt(SelectedIndex);
		}
	}
	
	void ChooseNode(){
		for(int i = 0; i < nodes.Count; i++){
			if (nodes[i].GraphRect.Contains(mousePosition)){
				SelectedNode = nodes[i];
				IsClickNode = true;
				SelectedIndex = i;
				return;
			}
		}
		SelectedNode = null;
		IsClickNode = false;
		SelectedIndex = -1;
		
	}
	
	void DrawNodeGraph(int id){
		nodes[id].DrawGraph(id);
		GUI.DragWindow();
		
	}
	
	public static void DrawNodeCurve(Rect start, Rect end) 
	{
		Vector3 startPos = new Vector3(start.x + start.width/2 , start.y + start.height/2,0);
		Vector3 endPos = new Vector3(end.x + end.width /2 , end.y + end.height /2 , 0);
		Vector3 startTan = startPos + Vector3.right * 150;
		Vector3 endTan = endPos + Vector3.left * 150;
		Color shadowCol = new Color(0,0,0,.06f);

		for(int i = 0; i < 3; i++)
		{
			Handles.DrawBezier(startPos,endPos,startTan, endTan, shadowCol, null, (i+1) * 5);
		}

		Handles.DrawBezier(startPos,endPos, startTan, endTan, Color.black, null, 1);
		
	}
	

	
}