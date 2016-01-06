//
//  XFsmActionComponent.cs
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
using wuxingogo.Runtime;
using UnityEngine.Events;


namespace wuxingogo.Fsm
{
	[Serializable]
	public class XFsmActionComponent : XScriptableObject, IFsmAction
	{

		public UnityAction OnFinish = null;
		public bool IsInit {
			get;
			set;
		}

		#region IFsmAction implementation

		public virtual void Init(){

		}

		public virtual void OnEnter()
		{
			OnFinish();
		}

		public virtual void OnExit()
		{
			
		}

		public virtual void OnUpdate()
		{
			
		}

		public virtual void OnLateUpdate()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		public bool isActive {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public IFsmState OwnerState {
			get {
				return ownerState;
			}
			set {
				ownerState = value;
			}
		}

		private IFsmState ownerState = null;

		#endregion
	}
}

