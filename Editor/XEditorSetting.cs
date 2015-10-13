using UnityEngine;
using System.Collections;
using UnityEditor;

public class XEditorSetting : XBaseWindow
{
    [MenuItem( "Wuxingogo/Wuxingogo XEditorSetting" )]
    static void init()
    {
        XEditorSetting window = ( XEditorSetting )EditorWindow.GetWindow( typeof( XEditorSetting ) );
    }

    public override void OnXGUI()
    {
        if( CreateSpaceButton( "ReImport All Asset" ) )
        {
            XResources.ReimportAll();
        }
    }
}
