using System;
using UnityEngine;
using wuxingogo.Node;
using UnityEditor;
using wuxingogo.Reflection;
using System.Collections.Generic;
using System.Linq;
using wuxingogo.btFsm;
using wuxingogo.Runtime;

namespace wuxingogo.BTNode
{
    public class BTGenericMenu : DTGenericMenu<BTFsm>
    {
        #region implemented abstract members of DTGenericMenu
        private BTNode currNode = null;
        private BTFsm currFsm = null;
        private static BTState copyState = null;
        public override void OnClickNode( DragNode targetNode )
        {
            currNode = ( BTNode )targetNode;
            var allActions = XReflectionUtils.FindUnitySubClass( typeof( BTAction ) ).ToList();
            var allVariables = XReflectionUtils.FindUnitySubClass( typeof( BTVariable ) ).ToList();
            GenericMenu gm = new GenericMenu();
            gm.AddItem( new GUIContent( "New Fsm Event" ), false, ClickNode, ( object )"NewEvent" );
            gm.AddItem( new GUIContent( "Open State Script" ), false, ClickNode, "OpenScript" );
            gm.AddItem( new GUIContent( "Delet State" ), false, ClickNode, ( object )"DeleteState" );
            gm.AddItem( new GUIContent( "Copy State" ), false, ClickNode, "CopyState" );
            for( int i = 0; i < allActions.Count; i++ )
            {
                var allObject = allActions[i].GetCustomAttributes( typeof( ActionTitleAttribute ), true );
                if( allObject.Length > 0 )
                    gm.AddItem( new GUIContent( "Add Action/" + ( ( ActionTitleAttribute )allObject[0] ).title ), false, ClickNode, ( object )allActions[i].AssemblyQualifiedName );
            }

            gm.ShowAsContext();
        }

        public void ClickNode( object obj )
        {
            var className = obj as string;
            var btNode = currNode as BTNode;
            if( className == "NewEvent" )
            {
                BTEvent.Create( btNode.BtState );
            }
            else if( className == "OpenScript" )
            {
                btNode.OpenScript();
            }
            else if( className == "DeleteState" )
            {
                DeleteCurrState();
            }
            else if( className == "CopyState" )
            {
                copyState = btNode.BtState;
            }
            else
            {
                var type = Type.GetType( className );
                if( type.IsSubclassOf( typeof( BTAction ) ) )
                {
                    var newAction = CreateAction( type, btNode.BtState );
                    newAction.OnCreate();
                    EditorUtility.SetDirty( newAction );
                    Debug.Log( type.ToString() );
                }
            }


        }

        public void DeleteCurrState()
        {
            BTEditorWindow.instance.RemoveState( currNode );

        }
        public override void OnClickNone( BTFsm targetNode )
        {
            currFsm = targetNode;
            GenericMenu gm = new GenericMenu();
            gm.AddItem( new GUIContent( "New Fsm Event" ), false, ClickNone, "NewGlobalEvent" );
            gm.AddItem( new GUIContent( "New Fsm State" ), false, ClickNone, "NewState" );
            gm.AddItem( new GUIContent( "Cancel Edit Fsm" ), false, ClickNone, "CancelEditFsm" );
            if( copyState != null )
                gm.AddItem( new GUIContent( "Paste State" ), false, ClickNone, "PasteState" );

            var allState = XReflectionUtils.FindUnitySubClass( typeof( BTState ) ).ToList();
            for( int i = 0; i < allState.Count; i++ )
            {
                var allObject = allState[i].GetCustomAttributes( typeof( StateTitleAttribute ), true );
                if( allObject.Length > 0 )
                {
                    gm.AddItem( new GUIContent( "Add Custom State/" + ( ( StateTitleAttribute )allObject[0] ).title ), false, AddCustomState,
                         ( object )allState[i].AssemblyQualifiedName );
                }
            }

            var allVariables = XReflectionUtils.FindUnitySubClass( typeof( BTVariable ) ).ToList();
            for( int i = 0; i < allVariables.Count; i++ )
            {
                var allObject = allVariables[i].GetCustomAttributes( typeof( ActionTitleAttribute ), true );
                if( allObject.Length > 0 )
                    gm.AddItem( new GUIContent( "Add Variable/" + ( ( ActionTitleAttribute )allObject[0] ).title ), false, ClickNone,
                        ( object )allVariables[i].AssemblyQualifiedName );
            }
            gm.ShowAsContext();
        }

        void AddCustomState( object obj )
        {
            var para = obj as string;
            var type = Type.GetType( para );
            var newState = BTState.Create( currFsm, type );
            newState.Name = type.Name;
            var newNode = BTEditorWindow.instance.AddNewBTNode( newState );
            newState.OnCreate();
            newNode.SetPosition( BTEditorWindow.instance.GetMousePosition() );
            EditorUtility.SetDirty( currFsm );
            AddStateToFsm( currFsm, newState );
        }
        public void ClickNone( object obj )
        {
            var para = obj as string;
            switch( para )
            {
                case "NewGlobalEvent":
                    {
                        var newEvent = BTEvent.Create( currFsm );
                        var newState = BTState.Create<BTState>( currFsm );
                        newEvent.TargetState = newState;
                        newState.GlobalEvent = newEvent;
                        BTEditorWindow.instance.AddNewBTNode( newState );
                        AddStateToFsm( currFsm, newState );
                    }
                    break;
                case "NewState":
                    {
                        var newState = BTState.Create<BTState>( currFsm );
                        newState.Name = "NewState";
                        var newNode = BTEditorWindow.instance.AddNewBTNode( newState );
                        newNode.SetPosition( BTEditorWindow.instance.GetMousePosition() );
                        EditorUtility.SetDirty( currFsm );
                        AddStateToFsm( currFsm, newState );
                    }
                    break;
                case "PasteState":
                    {
                        var newState = BTState.Create( currFsm, copyState );
                        newState.Name = "NewState";
                        newState.OnCreate();
                        BTEditorWindow.instance.AddNewBTNode( newState );
                        EditorUtility.SetDirty( currFsm );
                        AddStateToFsm( currFsm, newState );
                        for( int i = 0; i < newState.totalActions.Count; i++ )
                        {
                            AddActionToState( newState, newState.totalActions[i] );
                        }
                    }
                    break;
                case "CancelEditFsm":
                    BTEditorWindow.target = null;
                    break;
                default:
                    {
                        var type = Type.GetType( para );
                        if( type.IsSubclassOf( typeof( BTVariable ) ) )
                        {
                            var newVariable = CreateVariable( type, currFsm );
                            newVariable.OnCreate();
                            EditorUtility.SetDirty( newVariable );
                            Debug.Log( type.ToString() );
                        }
                    }
                    break;
            }


        }
        public static void AddStateToFsm( BTFsm owner, BTState targetState )
        {

            if( BTEditorWindow.HasPrefab( owner ) )
            {
                if( owner.template == null )
                {
                    owner.template = XScriptableObject.CreateInstance<BTTemplate>();
                    BTEditorWindow.AddObjectToAsset( owner.template, owner.gameObject );
                    EditorUtility.SetDirty( owner );
                    owner.template.startEvent = owner.startEvent;
                    owner.template.totalEvent = owner.totalEvent;
                    if( owner.template.totalState == null )
                        owner.template.totalState = new List<BTState>();
                }
                BTEditorWindow.AddObjectToAsset( targetState, owner.template );
                owner.template.totalState.Add( targetState );
                EditorUtility.SetDirty( owner );

            }
            else if( owner.template != null )
            {
                owner.template.totalState.Add( targetState );
                BTEditorWindow.AddObjectToAsset( targetState, owner.template );
                EditorUtility.SetDirty( owner.template );
            }
        }

        public static BTAction CreateAction( Type type, BTState parentState )
        {
            BTAction action = XScriptableObject.CreateInstance( type ) as BTAction;
            action.Name = type.Name;
            action.Owner = parentState;
            parentState.totalActions.Add( action );
            AddActionToState( parentState, action );
            return action;
        }

        public static BTVariable CreateVariable( Type type, BTFsm parentFsm )
        {
            BTVariable variable = XScriptableObject.CreateInstance( type ) as BTVariable;
            variable.Name = type.Name;
            variable.Owner = parentFsm;
            parentFsm.totalVariable.Add( variable );
            AddVarToFsm( parentFsm, variable );
            return variable;
        }

        public static void AddVarToFsm( BTFsm owner, BTVariable variable )
        {
            if( BTEditorWindow.HasPrefab( owner ) )
            {
                if( owner.template == null )
                {
                    owner.template = XScriptableObject.CreateInstance<BTTemplate>();
                    BTEditorWindow.AddObjectToAsset( owner.template, owner.gameObject );
                    EditorUtility.SetDirty( owner );
                    owner.template.startEvent = owner.startEvent;
                    owner.template.totalEvent = owner.totalEvent;
                }
				if( owner.template.totalVariable == null )
					owner.template.totalVariable = new List<BTVariable>();
                owner.template.totalVariable.Add( variable );
                BTEditorWindow.AddObjectToAsset( variable, owner.template );
                EditorUtility.SetDirty( owner );
            }
            else
            {
                if( owner.template != null )
                {
                    owner.template.totalVariable.Add( variable );
                    BTEditorWindow.AddObjectToAsset( variable, owner.template );
                }
                EditorUtility.SetDirty( owner );
            }
        }

        public static void AddActionToState( BTState Owner, BTAction action )
        {
			BTFsm owner = Owner.Owner;
			if( BTEditorWindow.HasPrefab( owner ) )
			{
				if( owner.template == null )
				{
					owner.template = XScriptableObject.CreateInstance<BTTemplate>();
					BTEditorWindow.AddObjectToAsset( owner.template, owner.gameObject );
					EditorUtility.SetDirty( owner );
					owner.template.startEvent = owner.startEvent;
					owner.template.totalEvent = owner.totalEvent;
				}
				BTEditorWindow.AddObjectToAsset( action, Owner );
				EditorUtility.SetDirty( owner );
			}
			else
			{
				BTEditorWindow.AddObjectToAsset( action, Owner );
				EditorUtility.SetDirty( owner );
			}
        }
        #endregion

    }
}

