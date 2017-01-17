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
		public void Draw(XBaseWindow window){
			XBaseWindow.DoButton("Type", ()=> {
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
}

