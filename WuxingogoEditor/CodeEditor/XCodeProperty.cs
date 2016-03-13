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
using System.CodeDom;


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
			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = name;
			property.Type = new CodeTypeReference(type.Target);
			property.Attributes = memberAttribute;
			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				property.Comments.Add(new CodeCommentStatement(comments[pos]));
			}
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in attributes
				property.CustomAttributes.Add(new CodeAttributeDeclaration(attributes[pos].name));
			}
			return property;
		}

		#endregion
	}
}

