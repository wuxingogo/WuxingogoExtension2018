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
using System.Collections.Generic;
using wuxingogo.Runtime;


namespace wuxingogo.Code
{
	[Serializable]
	public class XCodeMethod : XCodeBase, ICodeMember
	{
		bool isShowAll = false;
		public List<XCodeParameter> parameters = new List<XCodeParameter>();

		public XCodeMethod()
		{
			codeType = XCodeType.Method;
			name = "DefalutMethod";
		}


		#region implemented abstract members of XCodeBase
		public override void DrawSelf(XBaseWindow window)
		{
			
			window.DoButton( name, () => isShowAll = !isShowAll );
			if( isShowAll ) {
				window.BeginHorizontal();
				name = window.CreateStringField(name);
				window.CreateEnumSelectable( codeType );
				TypeID = window.CreateSelectableString(TypeID, StrTypes );
				window.EndHorizontal();

				window.BeginHorizontal();
				window.DoButton("Add Parameter", ()=> parameters.Add(new XCodeParameter()));
				window.DoButton("Add Attribute", ()=> attributes.Add(new XCodeCustomAttribute()));
				window.DoButton("Add Comment", ()=> comments.Add("TODO LIST"));
				window.EndHorizontal();
			}

				
			if(isShowAll){
				DrawComments(window);
				DrawAttribute(window);
				DrawParameters(window);
			}
		}
		#endregion

		void DrawParameters(XBaseWindow window){

			window.CreateLabel( "Parameters" );
			for( int pos = 0; pos < parameters.Count; pos++ ) {
				//  TODO loop in comments.Count
				window.BeginHorizontal();
				parameters[pos].type = window.CreateStringField( parameters[pos].type );
				window.DoButton( "Delete", () => parameters.RemoveAt( pos ) );
				window.EndHorizontal();
			}
		}

		#region implemented members of ICodeMember
		public System.CodeDom.CodeTypeMember Compile()
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = name;
//			method.Statements.Add(new CodeStatement());

			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				method.Comments.Add(new CodeCommentStatement(comments[pos]));
			}
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in attributes
				method.CustomAttributes.Add(attributes[pos].Compile());
			}

			for( int pos = 0; pos < parameters.Count; pos++ ) {
				//  TODO loop in parameters.Count
				method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(XBehaviorFSM), "aaa"));
//				new code new XCodeParameter().Compile());
			}
			method.ReturnType = new CodeTypeReference(objectType);
			return method;
		}

		#endregion


	}
}

