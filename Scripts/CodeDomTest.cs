using System.Collections;
using System.CodeDom;
using System.CodeDom.Compiler;
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using ccf;

public class CodeDomTest : XMonoBehaviour{
    [XAttribute("generate code")]
    public void Create(){
        //准备一个代码编译器单元
        
        
        var unit = new CodeCompileUnit();
        
        //准备必要的命名空间（这个是指要生成的类的空间）
        
//        CodeNamespace sampleNamespace=new CodeNamespace("wuxingogo");
        CodeNamespace sampleNamespace=new CodeNamespace("");
        //导入必要的命名空间
        
        sampleNamespace.Imports.Add(new CodeNamespaceImport("System"));
        sampleNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine"));
        
        //准备要生成的类的定义
        
        CodeTypeDeclaration Customerclass = new CodeTypeDeclaration("TestCompile");
        
        
        Customerclass.IsClass = true;
        
        Customerclass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
        
        //把这个类放在这个命名空间下
        
        sampleNamespace.Types.Add(Customerclass);
        
        //把该命名空间加入到编译器单元的命名空间集合中
        
        unit.Namespaces.Add(sampleNamespace);
        
        string outputFile = "Assets/TestCompile.cs";
        
        CodeMemberField field = new CodeMemberField(typeof(System.String), "_Id");
        
        field.Attributes = MemberAttributes.Private;
        
        Customerclass.Members.Add(field);
        
        //添加属性
        
        CodeMemberProperty property = new CodeMemberProperty();
        
        property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        
        property.Name = "Id";
        
        property.HasGet = true;
        
        property.HasSet = true;
        
        property.Type = new CodeTypeReference(typeof(System.String));
        
        property.Comments.Add(new CodeCommentStatement("这是Id属性"));
        
        property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id")));
        
        property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id"), new CodePropertySetValueReferenceExpression()));
        
        Customerclass.Members.Add(property);
        
        //添加方法（使用CodeMemberMethod)--此处略
        
        //添加构造器(使用CodeConstructor) --此处略
        
        //添加程序入口点（使用CodeEntryPointMethod） --此处略
        
        //添加事件（使用CodeMemberEvent) --此处略
        
        //添加特征(使用 CodeAttributeDeclaration)
        
       
        CodeAttributeArgument codeAttr =
            new CodeAttributeArgument( new CodePrimitiveExpression("This class is obsolete."));
//        CodeAttributeDeclaration cad = new CodeAttributeDeclaration(new CodeTypeReference(typeof(XAttribute)));
        CodeAttributeDeclaration cad = new CodeAttributeDeclaration("XAttribute",codeAttr);
        Customerclass.CustomAttributes.Add(cad);
        
        //生成代码
        
        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        
        CodeGeneratorOptions options = new CodeGeneratorOptions();
        
        options.BracingStyle = "C";
        
        options.BlankLinesBetweenMembers = true;
        
        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile)) {
            
            provider.GenerateCodeFromCompileUnit(unit, sw, options);
            
        }
        
        AssetDatabase.Refresh();
    
    
    }
    [XAttribute("generate !!!")]
    public void Generate()
    {
        GenerateCodeFactory.GenerateUnit();
        GenerateCodeFactory.GenerateAndImportNS( string.Empty, "System", "UnityEngine" );
        GenerateCodeFactory.GenerateClass( "WuxingogoTestDom", new string[]{"XMonoBehaviour"} );
        GenerateCodeFactory.GenerateField( typeof( string ), "strTest", MemberAttributes.Public );
        GenerateCodeFactory.GenerateCommon( "这是Field的注释" );
        GenerateCodeFactory.GenerateProperty( typeof( string ), "StrTest" );
        GenerateCodeFactory.GenerateCommon("这是Property的注释");
        GenerateCodeFactory.GenerateAndCompile( Application.streamingAssetsPath + "/TestDom.cs" );


    }
}
