//
//  XCodeCustomAttribute.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.



namespace wuxingogo.Code
{
	using System;
	using System.CodeDom;


	[Serializable]
	public class XCodeCustomAttribute
	{
		public string name = "";
		public XCodeParameter parameter = null;

		public XCodeCustomAttribute()
		{
			parameter = null;
		}

		public CodeAttributeDeclaration Compile()
		{
			CodeAttributeDeclaration declaration = null;
			CodeExpression para = parameter.Compile();
			if( null != para)
				declaration = new CodeAttributeDeclaration(name, new CodeAttributeArgument(para));
			else
				declaration = new CodeAttributeDeclaration(name);

			return declaration;
		}
	}
}

