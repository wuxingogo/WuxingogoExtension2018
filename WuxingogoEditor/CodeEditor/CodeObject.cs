//CodeObject.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/15/2015 1:38:10 PM 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

public class CodeObject{
    public string namespaceName = "";
    public List<string> importNS = new List<string>();

    public List<CodeBase> members = new List<CodeBase>();
    
    public List<string> baseClasses = new List<string>();

    public string className = "";

    public CodeObject()
    {

    }

    public void Compile(){
        CodeCompileUnit unit = new CodeCompileUnit();

        CodeNamespace codeNamespace = new CodeNamespace( namespaceName );

        for( int i = 0; i < importNS.Count; i++ )
        {
            codeNamespace.Imports.Add( new CodeNamespaceImport( importNS[i] ) );
        }

        unit.Namespaces.Add( codeNamespace );

        codeNamespace.Types.Add( GenerateClass() );

        CodeDomProvider provider = CodeDomProvider.CreateProvider( "CSharp" );

        CodeGeneratorOptions options = new CodeGeneratorOptions();

        options.BracingStyle = "C";

        options.BlankLinesBetweenMembers = true;

        using( System.IO.StreamWriter sw = new System.IO.StreamWriter( XEditorSetting.ProjectPath + "/" + className + ".cs" ) )
        {

            provider.GenerateCodeFromCompileUnit( unit, sw, options );

        }
    }

    public CodeTypeDeclaration GenerateClass()
    {
        CodeTypeDeclaration declarationClass = new CodeTypeDeclaration( className );
		
		foreach (var item in baseClasses) {
			declarationClass.BaseTypes.Add(item);
		}
        
        foreach( var item in members )
        {
            declarationClass.Members.Add( item.Member() );
        }
        return declarationClass;
    }
    
}