//
//  XFsmTemplate.cs
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
using System.Collections.Generic;


namespace wuxingogo.Fsm
{

	public class XFsmTemplate<TSource> : XScriptableObject where TSource : XScriptableObject
	{

		public static TSource Create()
		{
			TSource obj = XScriptableObject.CreateInstance<TSource>();
			return obj;
		}


		
	}
}

