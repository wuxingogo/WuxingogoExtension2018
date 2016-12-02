using wuxingogo.Editor;
using UnityEditor;
using wuxingogo.btFsm;

namespace wuxingogo.BTNode
{
    [CustomEditor(typeof(BTState), true)]
    [CanEditMultipleObjects]
    public class BTStateEditor : XScriptObjectEditor
    {
        BTState targetState
        {
            get
            {
                return target as BTState;
            }
        }
        public override void OnInspectorGUI()
        {
            DrawLogo();
            OnXGUI();
        }
        public override void OnXGUI()
        {
            base.OnXGUI();
            targetState.Owner = CreateObjectField( "Owner", targetState.Owner ) as BTFsm;
            targetState.Name = CreateStringField( "Name", targetState.Name );

            Space();
            Space();
            var totalEvent = targetState.totalEvent;

            CreateLabel( "total events :" );
            int deleteIndex = -1;
            for( int i = 0; i < totalEvent.Count; i++ )
            {
                BeginHorizontal();
                totalEvent[i].Name = CreateStringField( totalEvent[i].Name );
                DoButton( "Delete", () =>
                {
                    deleteIndex = i;
                } );
                EndHorizontal();
            }
            if( deleteIndex != -1 )
            {
                totalEvent.RemoveAt( deleteIndex );
            }

            //var totalEventProperty = serializedObject.FindProperty( "totalEvent" );
            //CreatePropertyField( totalEventProperty );
            
            Space();
            Space();
            
            CreateLabel( "total actions :" );
            var totalAction = targetState.totalActions;

            deleteIndex = -1;
            for( int i = 0; i < totalAction.Count; i++ )
            {
                BeginHorizontal();
                CreateObjectField( totalAction[i] );
                DoButton( "Delete", () =>
                 {
                     deleteIndex = i;
                 } );
                EndHorizontal();
            }
            
            if( deleteIndex != -1 )
            {
                var action = totalAction[deleteIndex];
                DestroyImmediate( action, true );
                totalAction.RemoveAt( deleteIndex );
            }
            Space();
            Space();
            DoButton( "Back to Fsm", () =>
             {
                 Selection.objects = new UnityEngine.Object[] { targetState.Owner };
             } );
           
            ShowXAttributeMember( target );


            targetState.GlobalEvent.Name = CreateStringField( "GlobalEvent", targetState.GlobalEvent.Name );
            targetState.GlobalEvent.isGlobal = CreateCheckBox( "isGlobal", targetState.GlobalEvent.isGlobal );
            
            targetState.Bounds = CreateRectField( "Bounds", targetState.Bounds );

            targetState.Notes = EditorGUILayout.TextArea( targetState.Notes, XStyles.GetInstance().textArea );

            targetState.Priority = CreateIntField( "Priority", targetState.Priority );
            serializedObject.Update();
            serializedObject.UpdateIfDirtyOrScript();
        }
    }
}
