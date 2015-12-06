using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using wuxingogo.Runtime;
using UnityEditor.Callbacks;

namespace XBehaviorEditor{
	public class XBehaviorEditor : XBaseWindow {
		
		
		[MenuItem( "Wuxingogo/Wuxingogo XBehaviorEditor" )]
		static void Init()
		{
			XBaseWindow.Init<XBehaviorEditor>();
		}
		
		private XBehaviorFSM FSM{
			get{
				return fsm;
			}
			set{
//				if(fsm != value){
					
				if(fsm != value)
					isDirty = true;
				   fsm = value;
					
//				}
				
			}
		}
		private XBehaviorFSM fsm = null;
		
		private static bool isDirty = true;
		private List<XBehaviorStateNode> allStateNode = new List<XBehaviorStateNode>();
		private Vector2 mousePosition = Vector2.zero;

		private XBehaviorStateNode mouseInNode = null;
		private XBehaviorStateNode selectedNode = null;
		private XBehaviorEvent selectedEvent = null;

		int SelectedIndex = -1;

		bool IsClickNode = false;

		bool IsTransition = false;
		
		public static XBehaviorEditor GetInstance(){
			if( null == _instance ){
				_instance = EditorWindow.GetWindow(typeof(XBehaviorEditor)) as XBehaviorEditor;
			}
			return _instance;
		}
		private static XBehaviorEditor _instance = null;
		
		
		public override void OnXGUI()
		{
			
			FSM = (XBehaviorFSM)CreateObjectField("BehaviorNode", fsm, typeof(XBehaviorFSM));
			
			Event e = Event.current;
			mousePosition = e.mousePosition;
			
			ChooseNode (e.button);
			
			bool isFixUpConfig = false;
			if (e.button == 1 && e.type == EventType.MouseUp) {
				XBehaviorMenu m = new XBehaviorMenu (mouseInNode);
				e.Use ();
				m = null;
			} 
			
			if(fsm){
				
			
				if(isDirty){
					allStateNode.Clear();
					for( int pos = 0; pos < fsm.allState.Count; pos++ ) {
						//  TODO loop in fsm.allState.Count
						allStateNode.Add(new XBehaviorStateNode(fsm.allState[pos]));
					}
					isDirty = false;
				}
	
				BeginWindows();
				
				for (int pos = 0; pos < allStateNode.Count; pos++) {
					//  TODO loop in allStateNode.Count
					allStateNode[pos].Draw(pos);
					
					List<XBehaviorEvent> events = allStateNode[pos].state.events;
					
					for (int idx = 0; idx < events.Count; idx++) {
						//  TODO loop in allStateNode[pos].state.events.Count
						if(events[idx].nextState != null){
 							DrawCurve(allStateNode[pos].GetJointPos(idx),
							          ChooseNextState(events[idx].nextState).GraphRect.center);
						}
					}
					
				}
				EndWindows();
				
			}
			
			
			
			if(IsTransition)
			{
				DrawCurve(selectedNode.GetJointPos(selectedNode.eventID),
					  new Vector3(mousePosition.x, mousePosition.y, 0));
					  
				Repaint();
			}
			
			
		}
		
		XBehaviorStateNode ChooseNextState(XBehaviorState state){
			for( int pos = 0; pos < allStateNode.Count; pos++ ) {
				//  TODO loop in allStateNode.Count
				if(allStateNode[pos].state.Equals(state)){
					return allStateNode[pos];
				}
			}
			return null;
			
		}
		
		
		
		void ChooseNode(int finger)
		{
			if(Event.current.type == EventType.MouseDown){
				if(!IsTransition){
					for (int i = 0; i < allStateNode.Count; i++) {
						//if( nodes[i] != null ) {
						if (allStateNode[i].GraphRect.Contains(mousePosition)) {
							mouseInNode = allStateNode[i];
							IsClickNode = true;
							SelectedIndex = i;
							return;
						}
						//}
					}
					mouseInNode = null;
					IsClickNode = false;
					SelectedIndex = -1;
				}
				else{
					IsTransition = false;
					for (int i = 0; i < allStateNode.Count; i++) {
						//if( nodes[i] != null ) {
						if (allStateNode[i].GraphRect.Contains(mousePosition)) {
							selectedEvent.nextState = allStateNode[i].state;
							return;
						}
						//}
					}
					mouseInNode = null;
					IsClickNode = false;
					SelectedIndex = -1;
				}
			
			}
			
			
		}
		
		
		
		public void AddState(){
			XBehaviorState state = new XBehaviorState("State1");
			allStateNode.Add(new XBehaviorStateNode(state));
			fsm.allState.Add(state);
		}
		
		public static void DrawCurve (Vector3 startPos, Vector3 endPos)
		{
			Vector3 topX = new Vector3(startPos.x, endPos.y, 0);
			
			Handles.DrawLine(startPos, topX);
			Handles.DrawLine(topX, endPos);
		}
		
		public void SetBehaviorEvent(XBehaviorEvent selectedEvent){
			this.selectedEvent = selectedEvent;
			selectedNode = this.mouseInNode;
			IsTransition = true;
		}
		
		public void ClearTransition(){
			IsTransition = false;
			
		}
		
		
	}
}

