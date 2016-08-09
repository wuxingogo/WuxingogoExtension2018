using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace wuxingogo.Runtime
{

	public class XBehaviorFSM : XMonoBehaviour {
		
		public XBehaviorState currentState;
		
		private bool isDirty = false;
		
		public XBehaviorEvent startEvent;
		
		public List<XBehaviorState> allState = new List<XBehaviorState>();
		
		
		public void FinishEvent(){
			isDirty = true;
			currentState.OnAwake();
			currentState.OnStart();
			currentState.OnEnter();
			
		}
		
		void Awake(){
//			allState = new List<XBehaviorState>();
			
//			startEvent = new XBehaviorEvent("Start");
//			startEvent.IsGlobal = true;
//			startEvent.CreateState("StartState");
			startEvent.Finish.AddListener(new UnityAction( FinishEvent));
//			
//			currentState = startEvent.nextState;
//			
//			allState.Add(startEvent.nextState);
			
			startEvent.DoState();
		}
		
		void Destory(){
			Logger.Log("Destory");
		}
	}
	
}
