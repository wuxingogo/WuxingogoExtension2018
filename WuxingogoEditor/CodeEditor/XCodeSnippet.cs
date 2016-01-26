//
//  XCodeSnippet.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.CodeDom;

namespace wuxingogo.Code
{

	public class XCodeSnippet : ICodeStatement
	{
		public string snippet = "";
		public XCodeSnippet(string snippet)
		{
			this.snippet = snippet;
		}

		#region ICodeMember implementation

		public System.CodeDom.CodeStatement Compile()
		{
			return new CodeSnippetStatement( snippet );
		}

		#endregion
	}
}

