//CodeGenerateEditor.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/15/2015 1:32:55 PM 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

namespace wuxingogo.Code
{
	using UnityEngine;
	using System.Collections;
	using System.Reflection;
	using UnityEditor;
	using System.CodeDom;
	using System;
	using Object = UnityEngine.Object;

    public class CodeGenerateEditor : XBaseWindow
	{
		[MenuItem( "Wuxingogo/Code/CodeGenerateEditor" )]
		static void init()
		{
			InitWindow<CodeGenerateEditor>();
		}

		public XCodeObject codeObject = null;

		public override void OnXGUI()
		{
			if( null != codeObject ) {

				DoButton<XCodeObject>( "SaveTemplate", SaveTemplete, codeObject );
				DoButton( "Clean", () => codeObject = null );
				DoButton("Compile", Compile);
				DoButton("Help MemberAttributes", () =>
				{
					for (int i = 0; i <= 61440; i++)
					{
						XLogger.Log(Enum.ToObject(typeof(MemberAttributes), i).ToString());
					}
				});
        		
				codeObject.Draw( this );
			} else {
				if(CreateSpaceButton("Create")){
					GenerateNewCode();
				}
				DoButton( "OpenTemplate", () => codeObject = OpenTemplate() );
			}
		}

		void SaveTemplete(XCodeObject so)
		{
			if (so.isFile)
			{
				EditorUtility.SetDirty(so);
			}
			else {

				string path = EditorUtility.SaveFilePanel("Create A Templete", XEditorSetting.PluginPath, codeObject.className + ".asset", "asset");
				if (path == "")
					return;

				path = FileUtil.GetProjectRelativePath(path);
				so.isFile = true;
				AssetDatabase.CreateAsset(so, path);

				AssetDatabase.SaveAssets();
			}
		}

		void Compile(){
			string path = EditorUtility.SaveFilePanel("OutPut Path", XEditorSetting.PluginPath, codeObject.className + ".cs", "cs");

			codeObject.Compile(path);
		}

		XCodeObject OpenTemplate()
		{
			string path = EditorUtility.OpenFilePanel( "Open A Template", XEditorSetting.PluginPath, "" );
			if( path == "" )
				return null;
		
			path = FileUtil.GetProjectRelativePath( path ); 
		
			XCodeObject co = AssetDatabase.LoadAssetAtPath<XCodeObject>( path );
		
			return co;
		}

		public XCodeObject GenerateNewCode(){
			codeObject = ScriptableObject.CreateInstance<XCodeObject>();
			return codeObject;
		}
	}

	[CustomEditor(typeof(XCodeObject))]
	public class XCodeInspector : XBaseEditor{
		public override void OnXGUI()
		{

			if(CreateSpaceButton("Open In XCodeWindow")){
				CodeGenerateEditor window = XBaseWindow.InitWindow<CodeGenerateEditor>();
				window.codeObject = target as XCodeObject;
			}
		}
	}
}