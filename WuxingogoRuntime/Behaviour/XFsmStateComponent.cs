//
//  XFsmStateComponent.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using wuxingogo.Runtime;
using UnityEngine.Events;


namespace wuxingogo.Fsm
{
	
	public class XFsmStateComponent : XScriptableObject, IFsmState
	{
	#if UNITY_EDITOR
		public Vector2 position;
	#endif
		public IFsmAction CurrAction {
			get {
				return currAction;
			}
			set {
				currAction = value as XFsmActionComponent;
			}
		}

		private XFsmActionComponent currAction = null;

		public bool IsInit {
			get;
			set;
		}

		public IBehaviourFsm OnwerFsm {
			get;
			set;
		}

		public IList FsmActions<T>() where T : IFsmAction
		{
			return actions;
		}

		public UnityAction OnFinish = null;


		#region IFsmState implementation
		public void Init()
		{
			
		}
		public void OnEnter()
		{
			if( CurrAction == null )
				CurrAction = actions[currIndex];
			CurrAction.OnEnter();
		}
		public virtual void OnExit()
		{
			
		}
		public virtual void OnUpdate()
		{
			CurrAction.OnUpdate();
		}
		public virtual void OnLateUpdate()
		{
			currAction.OnLateUpdate();
		}
		public void Reset()
		{
			
		}

		private List<IFsmAction> actions = new List<IFsmAction>();

		#endregion

		public virtual void RegistAction(XFsmActionComponent component){
			actions.Add(component);
			component.OwnerState = this;
			component.OnFinish += NextAction;
		}

		public virtual void UngistAction(XFsmActionComponent component){
			actions.Remove(component);
			component.OwnerState = null;
			component.OnFinish -= NextAction;
		}


		public List<XFsmEvent> FsmEvents {
			get {
				return fsmEvents;
			}
			private set {
				fsmEvents = value;
			}
		}
		public XFsmEvent FinishEvent {
			get;
			set;
		}

		private List<XFsmEvent> fsmEvents = new List<XFsmEvent>();

		public virtual void RegistEvent(XFsmEvent component){
			fsmEvents.Add(component);
			component.OwnerState = this;
		}

		public virtual void UngistEvent(XFsmEvent component){
			fsmEvents.Remove(component);
			component.OwnerState = null;
		}

		public virtual void RegistFinishEvent(){
			FinishEvent = new XFsmEvent();
			RegistEvent( FinishEvent );
		}

		private void NextAction(){
			if(currIndex < actions.Count - 1){
				currIndex++;
				CurrAction.OnExit();
				CurrAction = actions[currIndex];
				CurrAction.OnEnter();
			}else{
				OnFinish();
			}
		}

		public int currIndex = 0;


	}
}

