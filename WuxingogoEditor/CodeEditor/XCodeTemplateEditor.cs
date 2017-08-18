
using System;
using UnityEditor;
using wuxingogo.Code;
using wuxingogo.Editor;

[CustomEditor( typeof( XCodeTypeTemplate ) )]
public class XCodeTemplateEditor : XMonoBehaviourEditor
{
    public override void OnXGUI()
    {
        DoButton( "Open EditorWindow", () => XCodeTemplateWindow.Init( target as XCodeTypeTemplate ) );
    }

}

public class XCodeTemplateWindow : XBaseWindow
{
    private static XCodeTypeTemplate Target = null;

    internal static XCodeTemplateWindow Init( XCodeTypeTemplate template )
    {

        Target = template;
        return InitWindow<XCodeTemplateWindow>();
    }

    internal Action<XCodeType> currentAction = null;

    public override void OnXGUI()
    {
        if( null == Target )
            DoButton( "AddStructType", AddStuctType );

        else if( null != currentAction )
        {
            var templates = Target.templates;
            for( int pos = 0; pos < templates.Count; pos++ )
            {
                //  TODO loop in Target.
                DoButton<XCodeType>( templates[pos].ToString(), DoSelectType, templates[pos] );
            }
        }

        DoButton( "AddSnippet", () => Target.snippets.Add( "" ) );
        for( int pos = 0; pos < Target.snippets.Count; pos++ )
        {
            //  TODO loop in Target.snippets.Count
            Target.snippets[pos] = CreateStringField( Target.snippets[pos] );
        }
    }

    private void DoSelectType( XCodeType type )
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