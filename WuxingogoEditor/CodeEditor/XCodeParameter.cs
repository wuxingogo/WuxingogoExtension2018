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
		public string name = "arg";
		public XCodeType type = XCodeTypeTemplate.GetInstance().GetTemplate(typeof(int));
		public XCodeParameter()
		{
		}

		public object GetValue(){
//			switch(type.Target.Name){
//				case typeof(Object).Name:
//				break;
//				case typeof(int).Name:
//				return int.Parse(value);
//				break;
//				case typeof(float).Name:
//				return float.Parse(value);
//				break;
//				case typeof(string).Name:
//				return value;
//				break;
//				case typeof(UnityEngine.GameObject).Name:
//				break;
//				case "enum":
//				break;
//			}
			return null;
		}

		public void Draw(XBaseWindow window){
			window.DoButton("Type", ()=> {
				XCodeTypeTemplate.SelectType(x => type = x);
			});
		}

		public System.CodeDom.CodeExpression Compile()
		{
			
			CodeParameterDeclarationExpression expression = null;
			expression = new CodeParameterDeclarationExpression(type.Target, name);
			return expression;
		}

		
	}

	internal class GenericaType<T> 
	{
		public T Value;
	}
}

