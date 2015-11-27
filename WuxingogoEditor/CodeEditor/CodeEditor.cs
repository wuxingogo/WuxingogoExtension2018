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

    string[] supposeArray = new string[]{
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
                BeginHorizontal();
                
                item.type = (CodeType)CreateEnumSelectable( "mode", item.type );
                item.attrs = (MemberAttributes)CreateEnumPopup( "Type", item.attrs );
                //                Debug.Log(item.attrs.ToString());
                item.TypeID = CreateSelectableString(item.TypeID, supposeArray );
                item.name = CreateStringField( "Name", item.name );
                
                if( CreateSpaceButton( "Add Commen" ) )
                {
                    item.comment.Add( "//TODO List" );
                }
                if( CreateSpaceButton( "Delete" ) )
                {
                    codeObject.members.Remove( item );
                    codeObject.members.TrimExcess();
                    //EndHorizontal();
                    return;
                }
                EndHorizontal();
                
                item.Draw();
                
                for( int pos = 0; pos < item.comment.Count; pos++ )
                {
                    BeginHorizontal();
                    item.comment[pos] = CreateStringField( "//", item.comment[pos] );
                    
                    if( CreateSpaceButton( "Delete Comment" ) )
                    {
                        item.comment.RemoveAt( pos );
                        item.comment.TrimExcess();
                        
                    }
                    EndHorizontal();
                }
                
                
                
            }

            if( CreateSpaceButton( "Compile Code And Update" ) ){
                codeObject.Compile();
                AssetDatabase.Refresh();
            }
        }

    }
}