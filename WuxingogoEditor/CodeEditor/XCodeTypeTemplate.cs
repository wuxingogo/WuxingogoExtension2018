//
//  XCodeTypeTemplate.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UnityEngine;
using System.Collections.Generic;
using wuxingogo.Runtime;
using UnityEditor;


namespace wuxingogo.Code
{

	[InitializeOnLoad]
	[CreateAssetMenu( fileName = "XCodeTypeTemplate", menuName = "Wuxingogo/XcodeTemplate" )]
	public class XCodeTypeTemplate : XScriptableObject
	{
		static XCodeTypeTemplate()
		{
			string assetPath = XEditorSetting.TemplatesPath + "/" + "XCodeTypeTemplate.asset";
			assetPath = FileUtil.GetProjectRelativePath( assetPath );
			instance = AssetDatabase.LoadAssetAtPath<XCodeTypeTemplate>( assetPath );
		}

		[SerializeField] internal List<XCodeType> templates = new List<XCodeType>();
		[SerializeField] internal List<string> snippets = new List<string>();

		public void AddTemplate(Type type)
		{
			templates.Add( new XCodeType( type.AssemblyQualifiedName ) );
		}

		public XCodeType GetTemplate(Type type)
		{
			for( int pos = 0; pos < templates.Count; pos++ ) {
				//  TODO loop in templates.Count
				if( templates[pos].Target.Equals( type ) ) {
					return templates[pos];
				}
			}
			return null;
		}

		public string GetSnippets(int index)
		{
			if(snippets.Count > index)
			{
				return snippets[index];
			}
			return "";
		}

		public static void SelectType(Action<XCodeType> callback)
		{
			XCodeTemplateWindow window = XCodeTemplateWindow.Init( instance );
			window.currentAction = callback;
		}

		static XCodeTypeTemplate instance = null;

		public static XCodeTypeTemplate GetInstance()
		{
			return instance;
		}





	}

	public class XCodeTemplateWindow : XBaseWindow
	{
		private static XCodeTypeTemplate Target = null;

		internal static XCodeTemplateWindow Init(XCodeTypeTemplate template)
		{
				
			Target = template;
			return InitWindow<XCodeTemplateWindow>();
		}

		internal Action<XCodeType> currentAction = null;

		public override void OnXGUI()
		{
			if( null == Target )
				DoButton( "AddStructType", AddStuctType );
			
			else if( null != currentAction ) {
				var templates = Target.templates;
				for( int pos = 0; pos < templates.Count; pos++ ) {
					//  TODO loop in Target.
					DoButton<XCodeType>( templates[pos].ToString(), DoSelectType, templates[pos] );
				}
			}

			DoButton( "AddSnippet", ()=> Target.snippets.Add( "" ) );
			for( int pos = 0; pos < Target.snippets.Count; pos++ ) {
				//  TODO loop in Target.snippets.Count
				Target.snippets[pos] = CreateStringField( Target.snippets[pos] );
			}
		}

		private void DoSelectType(XCodeType type)
		{
			currentAction( type );
			currentAction = null;
			this.Close();
		}

		private void AddStuctType()
		{
			Target.AddTemplate( typeof( int ) );
			Target.AddTemplate( typeof( string ) );
			Target.AddTemplate( typeof( float ) );
			Target.AddTemplate( typeof( double ) );
			Target.AddTemplate( typeof( void ) );
		}
	}

	[CustomEditor( typeof( XCodeTypeTemplate ) )]
	public class XCodeTemplateEditor : XBaseEditor
	{
		public override void OnXGUI()
		{
			DoButton( "Open EditorWindow", () => XCodeTemplateWindow.Init( target as XCodeTypeTemplate ) );
		}

	}




}

