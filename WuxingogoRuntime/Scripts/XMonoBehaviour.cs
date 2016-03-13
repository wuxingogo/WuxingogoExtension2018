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
using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using System;
using System.Reflection;
using wuxingogo.Reflection;
#endif
namespace wuxingogo.Runtime {
	
	public class XMonoBehaviour : MonoBehaviour {
		#if UNITY_EDITOR
		static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;
		[ContextMenu("OpenInMethod")]
		public void OpenInMethodExten(){
			
			Type windowType = XReflectionUtils.TryGetClass( "XMethodWindow" );
//			object window = windowType.TryInvokeGlobalMethod("InitWindow");
//			windowType.TrySetPropertsy(window, "Target", this);

		}
		#endif
	}

}


