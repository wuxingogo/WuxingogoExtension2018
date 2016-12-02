using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Editor;
using wuxingogo.btFsm;

namespace wuxingogo.BTNode
{
    [CustomPreview( typeof( BTFsm ) )]
    public class BTFsmPreview : ObjectPreview
    {
        public override bool HasPreviewGUI()
        {
            return true;
        }
        public override void OnInteractivePreviewGUI( Rect r, GUIStyle background )
        {
            if( GUI.Button( r, "Preview" ) )
            {

                Selection.objects = new Object[] {
                    target
                };

            }
        }

        //public override void OnPreviewGUI( Rect r, GUIStyle background )
        //{
        //    if( GUI.Button( r, "Preview" ) )
        //    {

        //        Selection.objects = new Object[] {
        //            target
        //        };

        //    }
        //}
    }

    [CustomEditor( typeof( BTFsm ), true )]
    public class BTFsmEditor : XMonoBehaviourEditor
    {
        public override void OnPreviewGUI( Rect r, GUIStyle background )
        {
            if( GUI.Button( r, "Preview" ) )
            {

            }
       
        }
        public override void OnXGUI()
        {
            base.OnXGUI();
            DoButton( "Open In EditorWindow", () =>
            {
                BTEditorWindow.InitWindow<BTEditorWindow>();
                BTEditorWindow.target = target as BTFsm;
            } );
        }
        [MenuItem( "Assets/Create/BTFsm" )]
        public static void CreateFsm()
        {
            var gos = Selection.gameObjects;
            if( gos.Length > 0 )
            {
                var fsm = gos[0].AddComponent<BTFsm>();
                
            }
            else
            {

            }
        }
    }
}