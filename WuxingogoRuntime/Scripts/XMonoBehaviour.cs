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


#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#define Wuxingogo_Core
#endif

#if Wuxingogo_Core
using System;
using System.Reflection;
using wuxingogo.Reflection;
#endif

using UnityEngine;
using System.Collections;

namespace wuxingogo.Runtime {
	public class XMonoBehaviour : MonoBehaviour {
        #if Wuxingogo_Core
		[ContextMenu("OpenInMethod")]
		public void OpenInMethodExten(){

			Type windowType = XReflectionUtils.TryGetClass( "XMethodWindow" );
			object window = windowType.TryInvokeGlobalMethod("Init");
			windowType.TrySetProperty(window, "Target", this);

		}
        #endif

        public T Instanie<T>(T original ) where T : UnityEngine.Object
        {
            if( Application.isPlaying )
            {
                return Instantiate<T>( original );
            }
            return original;
        }
    }

}


