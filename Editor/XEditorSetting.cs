using UnityEngine;
using System.Collections;
using UnityEditor;

public class XEditorSetting : XBaseWindow
{
    private Vector2 _scrollPos = Vector2.zero;
    const int Xoffset = 5;
    const int XButtonWidth = 100;
    const int XButtonHeight = 20;
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
