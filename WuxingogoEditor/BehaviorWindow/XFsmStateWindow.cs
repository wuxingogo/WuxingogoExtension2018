//
//  XFsmStateWindow.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using wuxingogo.Fsm;


namespace FsmEditor
{

	public class XFsmStateWindow : XBaseWindow
	{
		XFsmStateComponent component = null;
		public XFsmStateWindow(XFsmStateComponent component)
		{
			this.component = component;
		}

		public void Draw(){
			// TODO LIST 2016 01 06....

		}
	}
}

