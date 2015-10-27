//  XMONOBEHAVIOUR
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2014 wuxingogo
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------------
// 2014/3/3 
// ------------------------------------------------------------------------------
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class XEvent
{
    public string title = "";
    public CallAction action = CallAction.Continue;
    public enum CallAction
    {
        Break,
        Continue
    }
}
