//
//  XFsmComponent.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using wuxingogo.Runtime;



namespace wuxingogo.Fsm
{
	public class XFsmComponent : wuxingogo.Runtime.XMonoBehaviour, IBehaviourFsm
	{
		#if UNITY_EDITOR
		public XFsmComponent(){
			XFsmStateComponent startState = new XFsmStateComponent();
			startState.name = "Start";
			RegistState(startState);
		}
		#endif
		public IFsmState CurrState {
			get {
				return currState;
			}
			set {
				currState = value as XFsmStateComponent;
			}
		}

		private XFsmStateComponent currState = null;

		#region IBehaviourFsm implementation

		public bool IsInit {
			get;
			set;
		}

		void Awake()
		{
			Init();
		}

		void OnEnable()
		{
			OnEnter();
		}

		void OnDisable()
		{
			OnExit();
		}

		void Update()
		{
			OnUpdate();
		}

		void LateUpdate()
		{
			OnLateUpdate();
		}



		[XAttribute( "" )]
		public virtual void Init()
		{
			if( IsInit ) {
				IsInit = false;

				for( int pos = 0; pos < fsmState.Count; pos++ ) {
					//  TODO loop in states.Count
					fsmState[pos].Init();
				}
			}
		}

		public virtual void OnEnter()
		{

		}

		public virtual void OnExit()
		{
			
		}

		public virtual void OnUpdate()
		{
			if(CurrState != null){
				CurrState.OnUpdate();
			}else{
				Debug.LogWarning("XFsm CurrentState is NULL================");
			}
		}

		public virtual void OnExecutingEvent(XFsmEvent fsmEvent){
			fsmEvent.OnEnter();
			if( null != fsmEvent.NextState ){
				CurrState.OnExit();
				CurrState = fsmEvent.NextState;
				CurrState.OnEnter();
			}
			fsmEvent.OnExit();
		}

		public virtual void OnLateUpdate()
		{
			if( null == CurrState ) {
				Debug.LogWarning("XFsm CurrentState is NULL================");
			} else {
				CurrState.OnLateUpdate();
			}
		}

		public virtual void Reset()
		{
			
		}

		public virtual IList FsmStates<T>() where T : IFsmState
		{
			return fsmState;
		}

		[SerializeField] List<XFsmStateComponent> fsmState = new List<XFsmStateComponent>();

		public void RegistState(XFsmStateComponent state)
		{
			state.OnwerFsm = this;
			fsmState.Add( state as XFsmStateComponent );
		}

		public void UngistState(XFsmStateComponent state)
		{
			if( fsmState.Count > 0 ) {


				state.OnwerFsm = null;
				fsmState.Remove( state );
			}
		}

		#endregion
		
		
	}
}

