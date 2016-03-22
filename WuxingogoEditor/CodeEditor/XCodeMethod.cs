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
	public class XCodeMethod : XCodeMember, ICodeMember
	{
		bool isShowAll = false;
		public List<XCodeParameter> parameters = new List<XCodeParameter>();
		public List<ICodeStatement> snippet = new List<ICodeStatement>();
		public List<CodeStatement> statements = new List<CodeStatement>();
		public XCodeMethod()
		{
			name = "DefalutMethod";
		}


		#region implemented abstract members of XCodeBase
		public override void DrawSelf(XBaseWindow window)
		{
			
			XBaseWindow.DoButton( name, () => isShowAll = !isShowAll );
			if( isShowAll ) {
				XBaseWindow.BeginHorizontal();
				name = XBaseWindow.CreateStringField(name);
				XBaseWindow.EndHorizontal();

				XBaseWindow.BeginHorizontal();
				DrawType(window);
				XBaseWindow.DoButton("Add Parameter", ()=> parameters.Add(new XCodeParameter()));
				XBaseWindow.DoButton("Add Attribute", ()=> attributes.Add(new XCodeCustomAttribute()));
				XBaseWindow.DoButton("Add Comment", ()=> comments.Add("TODO LIST"));
				XBaseWindow.EndHorizontal();
			}

				
			if(isShowAll){
				DrawComments(window);
				DrawCustomeAttribute(window);
				DrawParameters(window);
			}
		}
		#endregion

		void DrawParameters(XBaseWindow window){

			XBaseWindow.CreateLabel( "Parameters" );
			for( int pos = 0; pos < parameters.Count; pos++ ) {
				//  TODO loop in comments.Count
				XBaseWindow.BeginHorizontal();
//				parameters[pos].type = window.CreateStringField( parameters[pos].type );
				parameters[pos].Draw( window );
				XBaseWindow.DoButton( "Delete", () => parameters.RemoveAt( pos ) );
				XBaseWindow.EndHorizontal();
			}
		}

		#region implemented members of ICodeMember
		public System.CodeDom.CodeTypeMember Compile()
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = name;
			method.Attributes = memberAttribute;

			for( int pos = 0; pos < snippet.Count; pos++ ) {
				//  TODO loop in snippet.Count
				method.Statements.Add( snippet[pos].Compile() );
			}
				
			if(statements.Count > 0)
				method.Statements.AddRange(statements.ToArray());

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
				method.Parameters.Add(parameters[pos].Compile() as CodeParameterDeclarationExpression);
			}
			method.ReturnType = new CodeTypeReference(type.Target);
			if(type.Target.ContainsGenericParameters && type.Target.IsGenericType)
				method.TypeParameters.Add(new CodeTypeParameter (type.Target.DeclaringType.ToString()));
			return method;
		}

		#endregion


	}
}

