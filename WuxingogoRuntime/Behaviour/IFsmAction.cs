//
//  IFsmAction.cs
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

	public interface IFsmAction
	{
		bool isActive {
			get;
			set;
		}
		IFsmState OwnerState{
			get;
			set;
		}

		void Init();
		void OnEnter();
		void OnExit();
		void OnUpdate();
		void OnLateUpdate();
		void Reset();
	}
}
