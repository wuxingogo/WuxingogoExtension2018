using UnityEngine;
using wuxingogo.btFsm;
using wuxingogo.Editor;
using UnityEditor;

namespace wuxingogo.BTNode
{
    class VariableWindow : XMonoBehaviourEditor
    {
        public Rect container = new Rect( 0, 0, 200, 300 );
        public Vector2 scrollPos = Vector2.zero;
        private const int windowID = 100;
        BTFsm fsm;
        public void Draw(BTFsm fsm )
        {
            container = GUI.Window( windowID, container,drag , "Variable Window" );
            this.fsm = fsm;
           
        }

        public void drag( int id )
        {
            GUILayoutUtility.BeginGroup( "str" );
            scrollPos = EditorGUILayout.BeginScrollView( scrollPos );
            int deleteIndex = -1;
            for( int i = 0; i < fsm.totalVariable.Count; i++ )
            {

                BeginHorizontal();
                
                fsm.totalVariable[i].Name = CreateStringField( fsm.totalVariable[i].Name );
                DoButton( "Delete", () =>
                 {
                     deleteIndex = i;
                 } );
                EndHorizontal();
                fsm.totalVariable[i].variableValue = GetTypeGUI( fsm.totalVariable[i].variableValue, fsm.totalVariable[i].variableType, null );
                EditorUtility.SetDirty( fsm.totalVariable[i] );
                
            }
            if( deleteIndex != -1 )
            {
                

                if( fsm.template != null )
                {
                    fsm.template.totalVariable.RemoveAt( deleteIndex );
                    DestroyImmediate( fsm.totalVariable[deleteIndex], true );
                }
                else
                {
                    DestroyImmediate( fsm.totalVariable[deleteIndex] );
                }
                fsm.RemoveVar( deleteIndex );

            }
            EditorGUILayout.EndScrollView();
            GUILayoutUtility.EndGroup( "str" );
            GUI.DragWindow();
            //EditorGUILayout.TextField( GraphTitle );

           

        }

        public bool isPointInContainer( Vector2 point )
        {
            return container.Contains( point );
        }
    }
}
