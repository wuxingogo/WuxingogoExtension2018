//
//  XFsmEvent.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using wuxingogo.Runtime;


namespace wuxingogo.Fsm
{
	public class XFsmEvent : IFsmLifecycle
	{
		public string name = "";
		public XFsmEvent()
		{
			Init();
		}

		public XFsmStateComponent NextState {
			get;
			set;
		}

		public XFsmStateComponent OwnerState {
			get;
			set;
		}

		#region IFsmLifecycle implementation

		public virtual void Init()
		{
			
		}

		public virtual void OnEnter()
		{
			
		}

		public virtual void OnExit()
		{
			
		}

		public virtual void OnUpdate()
		{
			
		}

		public virtual void OnLateUpdate()
		{
			
		}

		public virtual void Reset()
		{
			
		}

		public virtual bool IsInit {
			get{
				return true;
			}
			set
			{

			}
		}

		#endregion
	}
}

