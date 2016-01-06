//
//  IFsmState.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.



namespace wuxingogo.Fsm
{
	using System.Collections.Generic;
	using System.Collections;


	public interface IFsmState : IFsmLifecycle
	{
		IBehaviourFsm OnwerFsm {
			get;
			set;
		}

		IList FsmActions<T>() where T : IFsmAction;

		IFsmAction CurrAction {
			get;
			set;
		}

		List<XFsmEvent> FsmEvents {
			get;
		}
	}
}