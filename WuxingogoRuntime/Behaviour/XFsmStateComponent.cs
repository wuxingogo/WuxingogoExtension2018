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


namespace wuxingogo.Fsm
{
	[Serializable]
	public class XFsmStateComponent : XScriptableObject, IFsmState
	{
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


		#region IFsmState implementation
		public void Init()
		{
			
		}
		public void OnEnter()
		{
			
		}
		public void OnExit()
		{
			
		}
		public void OnUpdate()
		{
			
		}
		public void OnLateUpdate()
		{
			
		}
		public void Reset()
		{
			
		}

		private List<IFsmAction> actions = new List<IFsmAction>();

		#endregion

		public virtual void RegistAction(XFsmActionComponent component){
			actions.Add(component);
			component.OwnerState = this;
		}

		public virtual void UngistAction(XFsmActionComponent component){
			actions.Remove(component);
			component.OwnerState = null;
		}

	}
}

