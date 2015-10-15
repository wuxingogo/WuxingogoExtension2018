//GenerateCodeFactory.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/14/2015 22:28:37 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using System.Collections;
using System.CodeDom;
using System.CodeDom.Compiler;
using System;
using System.Reflection;

namespace ccf
{
    public class GenerateCodeFactory
    {

        static CodeNamespace codeNamespace = null;
        static CodeCompileUnit unit = null;
        static CodeTypeDeclaration declarationClass = null;
        static CodeTypeMember currentTypeMember = null;
        //  Author:
        //       ${wuxingogo} <52111314ly@gmail.com>
        // ------------------------------------------------------------------------------
        // 2015/10/15  1.Generate compile unit
        // ------------------------------------------------------------------------------
        public static void GenerateUnit()
        {

            unit = new CodeCompileUnit();
        }
        //  Author:
        //       ${wuxingogo} <52111314ly@gmail.com>
        // ------------------------------------------------------------------------------
        // 2015/10/15  2.Generate and import namespace
        // ------------------------------------------------------------------------------
        public static void GenerateAndImportNS( string currNamespace, params string[] usingNamespace )
        {
            codeNamespace = new CodeNamespace( currNamespace );
            for( int i = 0; i < usingNamespace.Length; i++ )
            {
                codeNamespace.Imports.Add( new CodeNamespaceImport( usingNamespace[i] ) );
            }
            unit.Namespaces.Add( codeNamespace );
        }
        //  Author:
        //       ${wuxingogo} <52111314ly@gmail.com>
        // ------------------------------------------------------------------------------
        // 2015/10/15  3.Generate class. Next step is free
        // ------------------------------------------------------------------------------
        public static void GenerateClass( string declaration, string[] baseType, TypeAttributes ta = TypeAttributes.Public )
        {
            declarationClass = new CodeTypeDeclaration( declaration );

            if( baseType != null )
            {
                for( int i = 0; i < baseType.Length; i++ )
                {
                    declarationClass.BaseTypes.Add( baseType[i] );
                }
            }

            codeNamespace.Types.Add( declarationClass );
            currentTypeMember = declarationClass;
        }

        public static void GenerateField( Type type, string fieldName, MemberAttributes ma = MemberAttributes.Private )
        {
            CodeMemberField field = new CodeMemberField( type, fieldName );

            field.Attributes = ma;

            declarationClass.Members.Add( field );

            currentTypeMember = field;

        }

        public static void GenerateProperty( Type type, string propertyName, string member = "", MemberAttributes ma = MemberAttributes.Public )
        {
            CodeMemberProperty property = new CodeMemberProperty();

            property.Attributes = ma;

            property.Name = propertyName;

            property.HasGet = true;

            property.HasSet = true;

            property.Type = new CodeTypeReference( type );

            if( member.Equals( "" ) )
            {
                //property.GetStatements.Add( new CodeMethodReturnStatement( new CodeFieldReferenceExpression() ) );

                //property.SetStatements.Add( new CodeAssignStatement( new CodeFieldReferenceExpression(), new CodePropertySetValueReferenceExpression() ) );

            }
            else
            {
                property.GetStatements.Add( new CodeMethodReturnStatement( new CodeFieldReferenceExpression( new CodeThisReferenceExpression(), member ) ) );

                property.SetStatements.Add( new CodeAssignStatement( new CodeFieldReferenceExpression( new CodeThisReferenceExpression(), member ), new CodePropertySetValueReferenceExpression() ) );
            }

            declarationClass.Members.Add( property );
            currentTypeMember = property;

        }

        public static void GenerateCommon(string comments)
        {
            currentTypeMember.Comments.Add( new CodeCommentStatement( comments ) );
        }

        public static void GenerateCodeAttribute( string attrName, object attrArg = null )
        {
            CodeAttributeArgument arg = new CodeAttributeArgument( new CodePrimitiveExpression( attrArg ) );
            //        CodeAttributeDeclaration cad = new CodeAttributeDeclaration(new CodeTypeReference(typeof(XAttribute)));
            CodeAttributeDeclaration cad = new CodeAttributeDeclaration( attrName, arg );
            currentTypeMember.CustomAttributes.Add( cad );
        }
        public static void GenerateMethod( string methodName, string returnType, CodeParameterDeclarationExpression[] expression = null, MemberAttributes ma = MemberAttributes.Public )
        {
            CodeMemberMethod FiboncMethod = new CodeMemberMethod();
            FiboncMethod.Name = methodName;//方法名
            FiboncMethod.Attributes = ma;
            if( null != expression )
            {
                FiboncMethod.Parameters.AddRange( expression );
            }
            FiboncMethod.ReturnType = new CodeTypeReference( returnType );
            currentTypeMember = FiboncMethod;
        }

        public static void GenerateMethod( string methodName, Type backType, CodeParameterDeclarationExpression[] expression = null, MemberAttributes ma = MemberAttributes.Public )
        {
            CodeMemberMethod FiboncMethod = new CodeMemberMethod();
            FiboncMethod.Name = methodName;//方法名
            FiboncMethod.Attributes = ma;
            if( null !=expression )
            {
                FiboncMethod.Parameters.AddRange( expression );
            }
            FiboncMethod.ReturnType = new CodeTypeReference( backType );
            currentTypeMember = FiboncMethod;
        }
        //  Author:
        //       ${wuxingogo} <52111314ly@gmail.com>
        // ------------------------------------------------------------------------------
        // 2015/10/15  Final Step. Generate and Compile. Mannal assign ur file.
        // ------------------------------------------------------------------------------
        public static void GenerateAndCompile( string outputFile )
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider( "CSharp" );

            CodeGeneratorOptions options = new CodeGeneratorOptions();

            options.BracingStyle = "C";

            options.BlankLinesBetweenMembers = true;

            using( System.IO.StreamWriter sw = new System.IO.StreamWriter( outputFile ) )
            {

                provider.GenerateCodeFromCompileUnit( unit, sw, options );

            }
        }
    }
}

