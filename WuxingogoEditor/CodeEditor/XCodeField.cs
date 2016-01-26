//
//  XCodeField.cs
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
	public class XCodeField : XCodeMember, ICodeMember
	{
		bool isShowAll = false;

		public XCodeField()
		{
			name = "DefaultField";
		}

		#region implemented abstract members of CodeBase

		public override void DrawSelf(XBaseWindow window)
		{
			window.BeginHorizontal();
			window.DoButton( name, () => isShowAll = !isShowAll );
			if( isShowAll ) {
				name = window.CreateStringField(name);
//				window.CreateEnumSelectable( codeType );
//				TypeID = window.CreateSelectableString(TypeID, StrTypes );
				DrawType(window);
				window.DoButton("Add Attribute", ()=> attributes.Add(new XCodeCustomAttribute()));
				window.DoButton("Add Comment", ()=> comments.Add("TODO LIST"));
			}
			window.EndHorizontal();

			if(isShowAll)
				DrawComments(window);
		}

		#endregion

		#region implemented members of ICodeMember

		public System.CodeDom.CodeTypeMember Compile()
		{
			CodeMemberField field = new CodeMemberField(type.Target, name);
			field.Attributes = memberAttribute;
			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				field.Comments.Add(new CodeCommentStatement(comments[pos]));
			}
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in attributes
				field.CustomAttributes.Add(new CodeAttributeDeclaration(attributes[pos].name));
			}
			return field;
		}

		#endregion
	}
}

