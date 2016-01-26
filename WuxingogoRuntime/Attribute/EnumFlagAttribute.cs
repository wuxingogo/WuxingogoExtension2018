//
//  EnumFlagAttribute.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using UnityEngine;

namespace wuxingogo.Runtime
{
	public class EnumFlagAttribute : PropertyAttribute
	{
		public string enumName;

		public EnumFlagAttribute() {}

		public EnumFlagAttribute(string name)
		{
			enumName = name;
		}
	}
}

