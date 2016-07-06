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
using System.IO;

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

		public string reviewContent = "";


		public XCodeClass()
		{
			name = "DefaultClass";

		}

		#region implemented abstract members of CodeBase

		public void DrawSelf(XBaseWindow window)
		{
			
			name = XBaseWindow.CreateStringField("ClassName", name);

			XBaseWindow.DoButton("Add New NameSpace", ()=> importNamespace.Add("UnityEngine"));
			for( int pos = 0; pos < importNamespace.Count; pos++ ) {
				//  TODO loop in importNamespace.Count
				XBaseWindow.BeginHorizontal();
				importNamespace[pos] = XBaseWindow.CreateStringField(importNamespace[pos]);
				XBaseWindow.DoButton("Delete", ()=> importNamespace.RemoveAt(pos));
				XBaseWindow.EndHorizontal();
			}

			XBaseWindow.DoButton("Add New BaseClass", ()=> baseClass.Add("Object"));
			for( int pos = 0; pos < baseClass.Count; pos++ ) {
				//  TODO loop in importNamespace.Count
				XBaseWindow.BeginHorizontal();
				baseClass[pos] = XBaseWindow.CreateStringField(baseClass[pos]);
				XBaseWindow.DoButton("Delete", ()=> baseClass.RemoveAt(pos));
				XBaseWindow.EndHorizontal();
			}

			XBaseWindow.BeginHorizontal();
			XBaseWindow.DoButton("Add Method", ()=> methods.Add(new XCodeMethod()));
			XBaseWindow.DoButton("Add Field", ()=> fields.Add(new XCodeField()));
			XBaseWindow.DoButton("Add Property", ()=> properties.Add(new XCodeProperty()));
			XBaseWindow.EndHorizontal();

			for( int pos = 0; pos < methods.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				methods[pos].DrawSelf(window);
				XBaseWindow.DoButton("Delete", ()=> methods.RemoveAt(pos));

			}
			for( int pos = 0; pos < fields.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				fields[pos].DrawSelf(window);
				XBaseWindow.DoButton("Delete", ()=> fields.RemoveAt(pos));

			}
			for( int pos = 0; pos < properties.Count; pos++ ) {
				//  TODO loop in classChildren.Count
				properties[pos].DrawSelf(window);
				XBaseWindow.DoButton("Delete", ()=> properties.RemoveAt(pos));

			}

			XBaseWindow.DoButton("Review Content", () =>
			{
				reviewContent = ReviewContent();
			});

			reviewContent = EditorGUILayout.TextArea(reviewContent);

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
			if (string.IsNullOrEmpty(reviewContent))
			{
				reviewContent = ReviewContent();
			}
			File.WriteAllText(outPut, reviewContent);
			AssetDatabase.Refresh();
		}

		public string ReviewContent()
		{
			CodeCompileUnit unit = new CodeCompileUnit();

			CodeNamespace codeNamespace = new CodeNamespace(useNamespace);

			for (int i = 0; i < importNamespace.Count; i++)
			{
				codeNamespace.Imports.Add(new CodeNamespaceImport(importNamespace[i]));
			}

			unit.Namespaces.Add(codeNamespace);

			codeNamespace.Types.Add((CodeTypeDeclaration)Compile());

			CodeDomProvider provider = CodeDomProvider.CreateProvider(defaultLanguage);

			CodeGeneratorOptions options = new CodeGeneratorOptions();

			options.BracingStyle = "C";

			options.BlankLinesBetweenMembers = true;

			StringWriter sw = new StringWriter();
			provider.GenerateCodeFromCompileUnit(unit, sw, options);
			var result = sw.GetStringBuilder().ToString();
			sw.Close();

			return result;

		}

		#endregion
	}
}

