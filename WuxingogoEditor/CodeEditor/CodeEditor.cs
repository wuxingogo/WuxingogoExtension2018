//CodeEditor.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/15/2015 1:32:55 PM 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;
using System.CodeDom;
using System;
using Object = UnityEngine.Object;

public class CodeEditor : XBaseWindow {
    [MenuItem( "Wuxingogo/Wuxingogo XCodeEditor" )]
    static void init()
    {
		Init<CodeEditor>();
    }
    CodeObject codeObject = null;

    public string[] supposeArray = new string[]{
        "void",
        "int",
        "float",
        "string",
        "UnityObject",
        "enum"
    };
    public override void OnXGUI()
    {

        if( CreateSpaceButton( "Create New Code Node" ) )
        {
            codeObject = new CodeObject();
        }
		if( CreateSpaceButton( "Open Code Templete" ) ){
			codeObject = OpenTemplete();
        }
        if( CreateSpaceButton( "Save Code Templete" ) ){
			SaveTemplete(codeObject);
        }
        if( null != codeObject )
        {
            codeObject.namespaceName = CreateStringField( "NameSpace", codeObject.namespaceName );

            if( CreateSpaceButton( "Add Import Namespace" ) )
            {
                codeObject.importNS.Add( "" );
            }
            for( int pos = 0; pos < codeObject.importNS.Count; pos++ )
            {
                codeObject.importNS[pos] = CreateStringField( "using", codeObject.importNS[pos] );
            }
            
            BeginHorizontal();
            for (int pos = 0; pos < codeObject.baseClasses.Count; pos++) {
            	//  TODO loop in codeObject.baseClasses
            	codeObject.baseClasses[pos] = CreateStringField("Base", codeObject.baseClasses[pos]);
            	if(CreateSpaceButton("Delete")){
					codeObject.baseClasses.RemoveAt(pos);
					codeObject.baseClasses.TrimExcess();
					return;
				}
			}
            EndHorizontal();
            
            BeginHorizontal();
            codeObject.className = CreateStringField( "className", codeObject.className );

            if( CreateSpaceButton( "Add a member" ) )
            {
                codeObject.members.Add( new CodeBase() );
            }
            
            if( CreateSpaceButton( "Add a baseClass" ) ){
            	codeObject.baseClasses.Add( "object" );
            }
            EndHorizontal();
            
            foreach( var item in codeObject.members )
            {
				if( CreateSpaceButton( "Delete Member" ) )
				{
					codeObject.members.Remove( item );
					codeObject.members.TrimExcess();
					//EndHorizontal();
					return;
				}
				item.Draw(this);          
            }

            if( CreateSpaceButton( "Compile Code And Update" ) ){
				codeObject.Compile(XEditorSetting.ProjectPath + "/" + codeObject.className + ".cs");
                
            }
        }

    }
    
    void SaveTemplete(ScriptableObject so){
		string path = EditorUtility.SaveFilePanel("Create A Templete", XEditorSetting.ProjectPath, codeObject.className + ".asset", "asset");
		if (path == "")
			return;
		
		path = FileUtil.GetProjectRelativePath(path); 
		
		AssetDatabase.CreateAsset(so, path);
		AssetDatabase.SaveAssets();
			  
    }
    
	CodeObject OpenTemplete(){
		string path = EditorUtility.OpenFilePanel("Create A Data", XEditorSetting.ProjectPath, "");
		if (path == "")
			return null;
		
		path = FileUtil.GetProjectRelativePath(path); 
		
		CodeObject co = AssetDatabase.LoadAssetAtPath<CodeObject>(path);
		
		return co;
    }
}