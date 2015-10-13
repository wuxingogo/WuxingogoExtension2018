using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public class GraphWindow : XBaseWindow 
{
	public List<BaseNode> nodes = new List<BaseNode> ();
	Vector2 mousePosition = Vector2.zero;
	public bool IsTransition {
		get;
		private set;
	}
	
	public bool IsClickNode {
		get;
		private set;
	}
	
	public int SelectedIndex {
		get;
		private set;
	}
	
	public BaseNode SelectedNode {
		get;
		private set;
	}
	
	public BaseNode InputNode {
		get;
		private set;
	}
	private static GraphWindow _instance = null;
	public static GraphWindow GetInstance ()
	{
		if (null == _instance) {
			_instance = (GraphWindow)EditorWindow.GetWindow (typeof(GraphWindow));
		}
		return _instance;
	}
	
	[MenuItem ("Wuxingogo/Wuxingogo GraphWindow ")]
	static void init ()
	{
		_instance = (GraphWindow)EditorWindow.GetWindow (typeof(GraphWindow));
		_instance.nodes.Clear ();
	}

    

	public override void OnXGUI ()
	{
		//TODO List
		
		Event e = Event.current;
		mousePosition = e.mousePosition;
		
		if (e.button == 0 || e.button == 1) {
			ChooseNode ();
		}
		bool isFixUpConfig = false;
		if (e.button == 1 && e.type == EventType.MouseUp) {
			GraphMenu m = new GraphMenu (SelectedIndex);
			e.Use ();
		} else if (e.button == 0 && e.type == EventType.MouseUp && IsTransition) {
			if (IsClickNode && !SelectedNode.Equals (InputNode)) {
				SelectedNode.SetInputNode (InputNode, mousePosition);
			}
            
			isFixUpConfig = true;
		}
		BeginWindows ();

		for (int i = 0; i< nodes.Count; i++) {
			//  nodes[i].GraphRect = GUI.Window(i, nodes[i].GraphRect, DrawNodeGraph, nodes[i].GraphTitle);
			nodes [i].Draw (i);
			
		}
		if (isFixUpConfig) {
			IsTransition = false;    
		}

		EndWindows ();

		if (IsTransition) {
			Rect mouseRect = new Rect (e.mousePosition.x, e.mousePosition.y, 10, 10);
			DrawNodeCurve (InputNode.GetJointRect (), mouseRect);
			Repaint ();
		}
		for (int i = 0; i < nodes.Count; i++) {
			nodes [i].DrawCurves ();
		}
	}


	public void SetTransition (BaseNode selected)
	{
		if (!IsTransition) {
			InputNode = selected;
			IsTransition = true;
		}
	}

	public void SetCurrentTransition ()
	{
		SetTransition (SelectedNode);
	}
	
	void ChooseNode ()
	{
		for (int i = 0; i < nodes.Count; i++) {
			//if( nodes[i] != null ) {
			if (nodes [i].GraphRect.Contains (mousePosition)) {
				SelectedNode = nodes [i];
				IsClickNode = true;
				SelectedIndex = i;
				return;
			}
			//}
		}
		SelectedNode = null;
		IsClickNode = false;
		SelectedIndex = -1;
		
	}
	
	void DrawNodeGraph (int id)
	{
		nodes [id].DrawGraph (id);
		GUI.DragWindow ();
		
	}

	public void AddNode (BaseNode node)
	{
		node.SetGraphPosition (mousePosition.x, mousePosition.y);
		nodes.Add (node);

		if (IsTransition && !node.Equals (InputNode)) {
			node.SetInputNode (InputNode, mousePosition);
			IsTransition = false;
		}
	}

	public void DeletedNode ()
	{
		nodes.RemoveAt (SelectedIndex);
	}

    
	
	public static void DrawNodeCurve (Rect start, Rect end)
	{
		Vector3 startPos = new Vector3 (start.x + start.width / 2, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3 (end.x + end.width / 2, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 150;
		Vector3 endTan = endPos + Vector3.left * 150;
		Color shadowCol = new Color (0, 0, 0, .06f);

		for (int i = 0; i < 3; i++) {
			Handles.DrawBezier (startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		}

		Handles.DrawBezier (startPos, endPos, startTan, endTan, Color.black, null, 1);
		
	}
	

	
}