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
//			Type XMethod = GetTypeFromAllAssemblies("XMethodWindow");
//			object window = XMethod.GetMethod("Init").Invoke(null, null);
			
			Type windowType = XReflectionUtils.TryGetClass( "XMethodWindow" );
			object window = windowType.TryInvokeGlobalMethod("Init");
			windowType.TrySetProperty(window, "Target", this);
//			window.GetType().GetProperty("Target").SetValue(window, this, null);


		}


//		public static Type GetTypeFromAllAssemblies(string typeName) {
//			Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
//			foreach(Assembly assembly in assemblies) {
//				Type[] types = assembly.GetTypes();
//				foreach(Type type in types) {
//					if(type.Name.Equals(typeName, ignoreCase) || type.Name.Contains('+' + typeName)) //+ check for inline classes
//						return type;
//				}
//			}
//			return null;
//		}
		#endif
	}

}


