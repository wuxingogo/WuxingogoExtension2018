using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;
using System.Collections.Generic;

public class XFavoritesWindow : XBaseWindow {
    [MenuItem( "Wuxingogo/Wuxingogo XFavoritesWindow" )]
    static void Init()
    {
        InitWindow<XFavoritesWindow>();
    }

    MarkObject markobj
    {
        get
        {
            if( _markObj == null )
            {
                _markObj = MarkObject.GetInstance();
            }
            return _markObj;
        }
    }
    MarkObject _markObj = null;

    public override void OnXGUI()
    {
        base.OnXGUI();

        if( markobj == null )
        {
            return;
        }

        for( int i = 0; i < markobj.objSet.Count; i++ )
        {
            BeginHorizontal();
            CreateObjectField( markobj.objSet[i] );
            DoButton( "X", () =>
             {
                 markobj.RemoveEntry( i );
             } );
            EndHorizontal();
        }

        DoButton( "Add Select", () =>
         {
             Object[] select = Selection.objects;
             for( int i = 0; i < select.Length; i++ )
             {
                 markobj.AddEntry( select[i] );
             }
         } );

    }

}
