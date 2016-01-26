//
//  XCodeEventType.cs
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

	public class XCodeEvent : XCodeMember, ICodeMember
	{
		public XCodeEvent()
		{
			name = "DefaultEvent";
		}

		#region ICodeMember implementation
		public System.CodeDom.CodeTypeMember Compile()
		{
			CodeMemberEvent memberEvent = new CodeMemberEvent();
			memberEvent.Type = new CodeTypeReference(type.Target);
			memberEvent.Name = name;
			memberEvent.Attributes = memberAttribute;

			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				memberEvent.Comments.Add(new CodeCommentStatement(comments[pos]));
			}
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in attributes
				memberEvent.CustomAttributes.Add(new CodeAttributeDeclaration(attributes[pos].name));
			}
			return memberEvent;
		}
		#endregion
	}
}

