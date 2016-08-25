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

			GenericMenu gm = new GenericMenu();
			gm.AddItem( new GUIContent( "New Fsm Event" ), false, ClickNode, (object)"NewEvent");
			gm.AddItem( new GUIContent( "Delet State" ), false, ClickNode, (object)"DeleteState");
            gm.AddItem( new GUIContent( "Copy State" ), false, ClickNode, "CopyState" );
            for( int i = 0; i < allActions.Count; i++ ) {
				gm.AddItem( new GUIContent( "Add " + allActions[i].FullName ), false, ClickNode, (object)allActions[i].AssemblyQualifiedName );
			}
			gm.ShowAsContext();
		}

		public void ClickNode(object obj)
		{
			var className = obj as string;
			var btNode = currNode as BTNode;
			if(className == "NewEvent"){
				BTEvent.Create( btNode.BtState );
			}
			else if(className == "DeleteState"){
				DeleteCurrState();
			}
            else if( className == "CopyState" )
            {
                copyState = btNode.BtState;
            }
            else
            {
				var type = Type.GetType( className );

				CreateAction( type, btNode.BtState );
				Debug.Log( type.ToString() );
			}


		}

		public void DeleteCurrState()
		{
			BTEditorWindow.instance.RemoveState( currNode );

		}
		public override void OnClickNone(BTFsm targetNode)
		{
			currFsm = targetNode;
			GenericMenu gm = new GenericMenu();
			gm.AddItem( new GUIContent( "New Fsm Event" ), false, ClickNone, "NewGlobalEvent" );
			gm.AddItem( new GUIContent( "New Fsm State" ), false, ClickNone, "NewState" );
            if( copyState != null )
                gm.AddItem( new GUIContent( "Paste State" ), false, ClickNone, "PasteState" );
            gm.ShowAsContext();
		}

		public void ClickNone(object obj)
		{
			var para = obj as string;
			switch( para ) {
			case "NewGlobalEvent":{
					var newEvent = BTEvent.Create( currFsm );
					var newState = new BTState( currFsm );
					newEvent.TargetState = newState;
					newState.GlobalEvent = newEvent;
					BTEditorWindow.instance.AddNewBTNode( newState );
					AddStateToFsm(currFsm, newState);
				}


				break;
			case "NewState":{
					var newState = new BTState( currFsm );
					newState.Name = "NewState";
					BTEditorWindow.instance.AddNewBTNode( newState );
					EditorUtility.SetDirty( currFsm );
					AddStateToFsm(currFsm, newState);
				}
				break;
                case "PasteState":
                    {
                        var newState = new BTState( currFsm, copyState );
                        newState.Name = "NewState";
                        BTEditorWindow.instance.AddNewBTNode( newState );
                        EditorUtility.SetDirty( currFsm );
                        AddStateToFsm( currFsm, newState );
                        for( int i = 0; i < newState.totalActions.Count; i++ )
                        {
                            AddActionToState( newState, newState.totalActions[i] );
                        }
                    }
                    break;
            default:
				break;
			}


		}
		public static void AddStateToFsm(BTFsm owner, BTState targetState)
		{

			if (BTEditorWindow.HasPrefab)
			{
				if(owner.template == null)
				{
					owner.template = XScriptableObject.CreateInstance<BTTemplate>();
					AssetDatabase.AddObjectToAsset(owner.template, owner);
					EditorUtility.SetDirty(owner);
                    owner.template.startEvent = owner.startEvent;
                    owner.template.totalEvent = owner.totalEvent;
                    if( owner.template.totalState == null )
						owner.template.totalState = new List<BTState>();
				}
				AssetDatabase.AddObjectToAsset(targetState, owner.template);
				owner.template.totalState.Add( targetState );
				EditorUtility.SetDirty(owner);

			}
			else if (owner.template != null)
			{
				owner.template.totalState.Add( targetState );
				AssetDatabase.AddObjectToAsset(targetState, owner.template);
				EditorUtility.SetDirty(owner.template);
			}
		}

		public static BTAction CreateAction(Type type, BTState parentState)
		{
			BTAction action = XScriptableObject.CreateInstance(type) as BTAction;
            action.Name = type.Name;
            action.Owner = parentState;
			parentState.totalActions.Add(action);
			AddActionToState(parentState, action);
			return action;
		}


		public static void AddActionToState(BTState Owner, BTAction action)
		{
		if(Owner.Owner.template == null){
			AssetDatabase.AddObjectToAsset( action, Owner );
			EditorUtility.SetDirty( Owner );
		}
		else{
			AssetDatabase.AddObjectToAsset( action, Owner.Owner.template );
			EditorUtility.SetDirty( Owner );
		}
		}
		#endregion
		
	}
}

