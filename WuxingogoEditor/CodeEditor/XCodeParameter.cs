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
using System.CodeDom;


namespace wuxingogo.Code
{
	[Serializable]
	public class XCodeParameter : ICodeExpression
	{
		public string name = "";
		public string type = "";
		public string value = "";
		public XCodeParameter()
		{
			
		}

		public object GetValue(){
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

		public void Draw(XBaseWindow window){
			
		}

		public System.CodeDom.CodeExpression Compile()
		{
			CodePrimitiveExpression expression = null;

			object value = GetValue();
			if( null != value ){
				expression = new CodePrimitiveExpression(value);
			}
//			expression.Value = GetValue();
			return expression;
		}
	}
}

