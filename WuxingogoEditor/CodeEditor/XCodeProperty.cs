//
//  XCodeProperty.cs
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
	public class XCodeProperty : XCodeMember, ICodeMember
	{
		bool isShowAll = false;
		public XCodeProperty()
		{
//			codeType = XCodeTypeEnum.Property;
			name = "DefalutProperty";
		}

		#region implemented abstract members of XCodeBase

		public override void DrawSelf(XBaseWindow window)
		{
			if(isShowAll)
				DrawComments(window);
		}

		#endregion

		#region implemented abstract members of XCodeBase

		public System.CodeDom.CodeTypeMember Compile()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

