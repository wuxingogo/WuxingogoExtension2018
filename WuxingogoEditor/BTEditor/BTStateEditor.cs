using wuxingogo.Editor;
using UnityEditor;
using wuxingogo.btFsm;

namespace wuxingogo.BTNode
{
    [CustomEditor(typeof(BTState), true)]
    public class BTStateEditor : XScriptObjectEditor
    {
        BTState targetState
        {
            get
            {
                return target as BTState;
            }
        }
        public override void OnXGUI()
        {
            targetState.Name = CreateStringField( "Name", targetState.Name );


            var totalEvent = targetState.totalEvent;

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

            DoButton( "Back to Fsm", () =>
             {
                 Selection.objects = new UnityEngine.Object[] { targetState.Owner };
             } );
           
            ShowXAttributeMember( target );

            if( targetState.GlobalEvent.TargetState != null )
            {
                targetState.GlobalEvent.Name = CreateStringField( "GlobalEvent", targetState.GlobalEvent.Name );
                targetState.GlobalEvent.isGlobal = CreateCheckBox( "isGlobal", targetState.GlobalEvent.isGlobal );
            }

            serializedObject.Update();
            serializedObject.UpdateIfDirtyOrScript();
        }
    }
}
