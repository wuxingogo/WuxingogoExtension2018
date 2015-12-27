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

	[Serializable]
	public class XFsmComponent : wuxingogo.Runtime.XMonoBehaviour, IBehaviourFsm
	{
		public IFsmState CurrState {
			get{
				return currState;
			}
			set{
				currState = value as XFsmStateComponent;
			}
		}
		private XFsmStateComponent currState = null;

		#region IBehaviourFsm implementation
		public bool IsInit {
			get;
			set;
		}

		void Awake(){
			Init();
		}
		void OnEnable(){
			OnEnter();
		}
		void OnDisable(){
			OnExit();
		}
		void Update(){
			OnUpdate();
		}
		void LateUpdate()
		{
		    OnLateUpdate();
		}



		[XAttribute( "" )]
		public virtual void Init()
		{
			if(IsInit){
				IsInit = false;

				for( int pos = 0; pos < states.Count; pos++ ) {
					//  TODO loop in states.Count
					states[pos].Init();
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
			if( null == CurrState ) {
				
			}
		}

		public virtual void OnLateUpdate()
		{
			
		}

		public virtual void Reset()
		{
			
		}

		public virtual IList FsmStates<T>() where T : IFsmState
		{
			return states;
		}

		[SerializeField] List<XFsmStateComponent> states = new List<XFsmStateComponent>();

		public void RegistState(XFsmStateComponent state)
		{
			state.OnwerFsm = this;
			states.Add( state as XFsmStateComponent );
		}

		public void UngistState(XFsmStateComponent state)
		{
			state.OnwerFsm = null;
			states.Remove( state );
		}

		#endregion
		
		
	}
}

