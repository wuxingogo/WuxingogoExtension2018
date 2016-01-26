//
//  XCodeClass.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.CodeDom;
using System.CodeDom.Compiler;
using UnityEditor;


namespace wuxingogo.Code
{
	[System.Serializable]
	public class XCodeClass : ICodeMember
	{
		public string useNamespace = "";
		public List<string> importNamespace = new List<string>();
		public List<string> baseClass = new List<string>();

		public List<XCodeField> fields = new List<XCodeField>();
		public List<XCodeMethod> methods = new List<XCodeMethod>();
		public List<XCodeProperty> properties = new List<XCodeProperty>();
		public List<XCodeEvent> events = new List<XCodeEvent>();
		public string name = "";

		public string defaultLanguage = "CSharp";

		public XCodeClass()
		{
			name = "DefaultClass";

		}

		#region implemented abstract members of CodeBase

		public void DrawSelf(XBaseWindow window)
		{
			
			name = window.CreateStringField("ClassName", name);

			window.DoButton("Add New NameSpace", ()=> importNamespace.Add("UnityEngine"));
			for( int pos = 0; pos < importNamespace.Count; pos++ ) {
				//  TODO loop in importNamespace.Count
				window.BeginHorizontal();
				importNamespace[pos] = window.CreateStringField(importNamespace[pos]);
				window.DoButton("Delete", ()=> importNamespace.RemoveAt(pos));
				window.EndHorizontal();
			}

			window.DoButton("Add New BaseClass", ()=> baseClass.Add("Object"));
			for( int pos = 0; pos < baseClass.Count; pos++ ) {
				//  TODO loop in importNamespace.Count
				window.BeginHorizontal();
				baseClass[pos] = window.CreateStringField(baseClass[pos]);
				window.DoButton("Delete", ()=> baseClass.RemoveAt(pos));
				window.EndHorizontal();
			}

			window.BeginHorizontal();
			window.DoButton("Add Method", ()=> methods.Add(new XCodeMethod()));
			window.DoButton("Add Field", ()=> fields.Add(new XCodeField()));
			window.DoButton("Add Property", ()=> properties.Add(new XCodeProperty()));
			window.EndHorizontal();

			for( int pos = 0; pos < methods.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				methods[pos].DrawSelf(window);
				window.DoButton("Delete", ()=> methods.RemoveAt(pos));

			}
			for( int pos = 0; pos < fields.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				fields[pos].DrawSelf(window);
				window.DoButton("Delete", ()=> fields.RemoveAt(pos));

			}
			for( int pos = 0; pos < properties.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				properties[pos].DrawSelf(window);
				window.DoButton("Delete", ()=> properties.RemoveAt(pos));

			}

		}

		#endregion

		#region implemented abstract members of ICodeMember

		public CodeTypeMember Compile()
		{
			CodeTypeDeclaration declarationClass = new CodeTypeDeclaration( name );

			for( int pos = 0; pos < baseClass.Count; pos++ ) {
				//  TODO loop in baseClass.Count
				declarationClass.BaseTypes.Add(baseClass[pos]);
			}

			for( int pos = 0; pos < methods.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				CodeTypeMember member = methods[pos].Compile();
				declarationClass.Members.Add(member);
			}

			for( int pos = 0; pos < fields.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				CodeTypeMember member = fields[pos].Compile();
				declarationClass.Members.Add(member);
			}

			for( int pos = 0; pos < properties.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				CodeTypeMember member = properties[pos].Compile();
				declarationClass.Members.Add(member);
			}

			for( int pos = 0; pos < events.Count; pos++ ) {
				//  TODO loop in events.Count
				CodeTypeMember member = events[pos].Compile();
				declarationClass.Members.Add(member);;
			}



			return declarationClass;
		}

		public void Compile(string outPut)
		{
			CodeCompileUnit unit = new CodeCompileUnit();

			CodeNamespace codeNamespace = new CodeNamespace( useNamespace );

			for( int i = 0; i < importNamespace.Count; i++ ) {
				codeNamespace.Imports.Add( new CodeNamespaceImport( importNamespace[i] ) );
			}

			unit.Namespaces.Add( codeNamespace );

			codeNamespace.Types.Add((CodeTypeDeclaration)Compile());

			CodeDomProvider provider = CodeDomProvider.CreateProvider( defaultLanguage );

			CodeGeneratorOptions options = new CodeGeneratorOptions();

			options.BracingStyle = "C";

			options.BlankLinesBetweenMembers = true;

			using( System.IO.StreamWriter sw = new System.IO.StreamWriter( outPut ) ) {
				provider.GenerateCodeFromCompileUnit( unit, sw, options );
			}
        
			AssetDatabase.Refresh();
		}

		#endregion
	}
}

