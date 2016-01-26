//
//  XCodeBase.cs
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
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using System.Reflection;
	using System.CodeDom;
	using System;
	using Object = UnityEngine.Object;

//	[System.Serializable]
	public class XCodeMember
	{
		public string name = "Default";

		public virtual void DrawSelf(XBaseWindow window){
		}

		public List<string> comments = new List<string>();
		public List<XCodeCustomAttribute> attributes = new List<XCodeCustomAttribute>();

		public XCodeType type = XCodeTypeTemplate.GetInstance().GetTemplate(typeof(void));
		public MemberAttributes memberAttribute = MemberAttributes.Public;

		public XCodeMember()
		{
		}

		public virtual void DrawType(XBaseWindow window)
		{
			window.DoButton("Type", ()=> {
				XCodeTypeTemplate.SelectType(x => type = x);
			});

			memberAttribute = (MemberAttributes)window.CreateEnumSelectable( memberAttribute );
		
		}
//		public virtual void DrawMemberAttribute(XBaseWindow window)
//		{
//			memberAttribute = window.CreateEnumSelectable( memberAttribute ) as MemberAttributes;
//		}

		public virtual void DrawComments(XBaseWindow window)
		{
			window.CreateLabel( "Comments" );
			for( int pos = 0; pos < comments.Count; pos++ ) {
				//  TODO loop in comments.Count
				window.BeginHorizontal();
				comments[pos] = window.CreateStringField( comments[pos] );
				window.DoButton( "Delete", () => comments.RemoveAt( pos ) );
				window.EndHorizontal();
			}
		}

		public virtual void DrawCustomeAttribute(XBaseWindow window)
		{
			window.CreateLabel( "CustomAttributes" );
			for( int pos = 0; pos < attributes.Count; pos++ ) {
				//  TODO loop in comments.Count
				window.BeginHorizontal();
				attributes[pos].name = window.CreateStringField( attributes[pos].name );
				window.DoButton<XCodeParameter>( "Parameter", ParameterCreate, attributes[pos].parameter);
				window.DoButton( "Delete", () => attributes.RemoveAt( pos ) );
				window.EndHorizontal();
			}
		}

		public void ParameterCreate(XCodeParameter parameter){
			XParameterEditor.Init<XParameterEditor>(parameter);
		}
   
	}
}

