//
//  XCodeMethod.cs
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
	public class XCodeMethod : XCodeBase, ICodeMember
	{
		bool isShowAll = false;

		public XCodeMethod()
		{
			codeType = XCodeType.Method;
			name = "DefalutMethod";
		}


		#region implemented abstract members of XCodeBase
		public override void DrawSelf(XBaseWindow window)
		{
			window.BeginHorizontal();
			window.DoButton( name, () => isShowAll = !isShowAll );
			if( isShowAll ) {
				name = window.CreateStringField(name);
				window.CreateEnumSelectable( codeType );
				TypeID = window.CreateSelectableString(TypeID, StrTypes );
//				window.DoButton("Add Attribute", ()=> attributes.Add("XAttribute"));
				window.DoButton("Add Comment", ()=> comments.Add("TODO LIST"));
			}
			window.EndHorizontal();
				
			if(isShowAll){
				DrawComments(window);
				DrawAttribute(window);
			}
		}
		#endregion

		#region implemented members of ICodeMember
		public System.CodeDom.CodeTypeMember Compile()
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = name;
			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				method.Comments.Add(new CodeCommentStatement(comments[pos]));
			}
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in attributes
				method.CustomAttributes.Add(attributes[pos].Compile());
			}
			method.ReturnType = new CodeTypeReference(objectType);
			return method;
		}

		#endregion
	}
}

