//
//  DisableExpresionAttribute.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UnityEngine;


namespace wuxingogo.Runtime
{

	public class DisableExpresionAttribute : PropertyAttribute
	{
		public object obj = null;
		public DisableExpresionAttribute(object obj)
		{
			this.obj = obj;
		}
	}
}

