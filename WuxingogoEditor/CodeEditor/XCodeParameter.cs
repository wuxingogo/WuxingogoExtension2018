//
//  XCodeParameter.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;


namespace wuxingogo.Code
{
	[Serializable]
	public class XCodeParameter
	{
		public string type = "";
		public string value = "";
		public XCodeParameter()
		{
			
		}

		public object Compile(){
			switch(type){
				case "void":
				break;
				case "int":
				return int.Parse(value);
				break;
				case "float":
				return float.Parse(value);
				break;
				case "string":
				return value;
				break;
				case "UnityObject":
				break;
				case "enum":
				break;
			}
			return null;
		}
	}
}

