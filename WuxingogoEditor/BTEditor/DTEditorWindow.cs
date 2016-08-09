using System;
using UnityEngine;
using System.Linq;
using UnityEditor;
using wuxingogo.btFsm;

namespace wuxingogo.Node
{
	/// <summary>
	/// Drag Tree editor window.
	/// </summary>
	public abstract class DTEditorWindow<T> : XBaseWindow where T : MonoBehaviour
	{
		GraphSelectionBox selectionBox = null;
		KeyBoardState keyboardState = null;
		GraphCamera graphCamera = null;

		public Vector2 mousePosition = Vector2.zero;

		public abstract DragNode[] DragNodes();

		public abstract DTGenericMenu<T> GetGenericMenu();

		public virtual T targetNode{
			get;set;
		}

		DTGenericMenu<T> genericMenu = null;

		public override void OnXGUI()
		{
			base.OnXGUI();

			Event e = Event.current;
			mousePosition = e.mousePosition;

			keyboardState.HandleInput( e );

			graphCamera.HandleInput( e );

			HandleSelect( e );
			HandleDrag( e );
			HandleDragPerform( e );

			var allNodes = DragNodes();
			for( int i = 0; i < allNodes.Length; i++ ) {
				allNodes[i].DrawPosition = graphCamera.WorldToScreen( allNodes[i].Position );
				allNodes[i].Draw();
			}

			bool inputProcessed = false;
			foreach( var node in allNodes ) {
				if( node == null )
					continue;
				inputProcessed = HandleNodeInput( node, e, graphCamera );
				if( inputProcessed ) {
					break;
				}
			}

			if( !inputProcessed ) {
				selectionBox.HandleInput( e );
				HandleEvent( e );
			}

			selectionBox.Draw();

			if( e != null ) {
//				e.Use();
				Repaint();

			}
		}

		public void OnEnable()
		{
			graphCamera = new GraphCamera();
			keyboardState = new KeyBoardState();
			selectionBox = new GraphSelectionBox();
			selectionBox.SelectionPerformed += HandleBoxSelection;

			genericMenu = GetGenericMenu();
		}

		public void OnDisable()
		{

			keyboardState = null;
			graphCamera = null;
			if( selectionBox != null ) {
				selectionBox.SelectionPerformed -= HandleBoxSelection;
				selectionBox = null;
			}
		}

		protected virtual void OnSelectNode(DragNode node){
			
			node.OnSelected();
		}


		void HandleSelect(Event e)
		{
			// Update the node selected flag
			var mousePositionWorld = TransportWorldPos( e );
			var buttonId = 0;
			if( e.type == EventType.MouseDown && e.button == buttonId ) {
				bool multiSelect = keyboardState.ShiftPressed;
				bool toggleSelect = keyboardState.ControlPressed;
				// sort the nodes front to back
				DragNode[] sortedNodes = DragNodes();
				//                System.Array.Sort(sortedNodes, new NodeReversedZIndexComparer());

				DragNode mouseOverNode = SelectedNode( mousePositionWorld );;



				foreach( var node in sortedNodes ) {
					var mouseOver = ( node.Equals( mouseOverNode ) );

					if( mouseOverNode != null && mouseOverNode.Selected && !toggleSelect ) {
//						multiSelect = true;	// select multi-select so that we can drag multiple objects
					}
					if( multiSelect || toggleSelect ) {
						if( mouseOver && multiSelect ) {
							node.Selected = true;

						} else if( mouseOver && toggleSelect ) {
							node.Selected = !node.Selected;
						}
					} else {
						node.Selected = mouseOver;
						if(mouseOverNode != null)
							OnSelectNode( mouseOverNode );
					}

					if( node.Selected ) {
						BringToFront( node );
					}
				}

				if( mouseOverNode == null ) {
					// No nodes were selected 
					Selection.activeObject = null;
				}

				OnNodeSelectionChanged();
			}
		}

		bool draggingNodes = false;

		void HandleDrag(Event e)
		{
			int dragButton = 0;
			if( draggingNodes ) {
				if( e.type == EventType.MouseUp && e.button == dragButton ) {
					draggingNodes = false;
				} else if( e.type == EventType.MouseDrag && e.button == dragButton ) {
					// Drag all the selected nodes
					foreach( var node in DragNodes() ) {
						if( node.Selected ) {
//                            Undo.RecordObject(node, "Move Node");
							node.MoveNode( e.delta );
						}
					}
				}
			} else {
				// Check if we have started to drag
				if( e.type == EventType.MouseDown && e.button == dragButton ) {
					// Find the node that was clicked below the mouse
					var mousePositionWorld = TransportWorldPos( e );

					DragNode mouseOverNode = null;

					mouseOverNode = SelectedNode(mousePositionWorld);

					if( mouseOverNode != null && mouseOverNode.Selected ) {
					// Make sure we are not over a pin
					//                        var pins = new List<GraphPin>();
					//                        pins.AddRange(mouseOverNode.InputPins);
					//                        pins.AddRange(mouseOverNode.OutputPins);
					//                        bool isOverPin = false;
					//                        GraphPin overlappingPin = null;
					//                        foreach (var pin in pins)
					//                        {
					//                            if (pin.ContainsPoint(mousePositionWorld))
					//                            {
					//                                isOverPin = true;
					//                                overlappingPin = pin;
					//                                break;
					//                            }
					//                        }
					//                        if (!isOverPin)
					//                        {
						draggingNodes = true;
					//                        }
					//                        else
					//                        {
					//                            HandleDragPin(overlappingPin);
					//                        }
					}
				}
			}
		}


		public void HandleDragPerform(Event e)
		{
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
		}

		protected Vector2 TransportWorldPos(Event e)
		{
			return graphCamera.ScreenToWorld(e.mousePosition);
		}

		protected virtual void BringToFront(DragNode node)
		{
			/// to do list
		}


		const int Mouse_left = 0;
		const int Mouse_right = 1;

		void HandleEvent( Event e )
		{
			// Update the node selected flag
			switch( e.button ) {
			case Mouse_left:
				HandleClick( e );
				break;
			case Mouse_right:
				if(e.type == EventType.MouseUp){
					var mousePositionWorld = TransportWorldPos( e );
					var node = SelectedNode( mousePositionWorld );
					if( node != null )
						genericMenu.OnClickNode( node );
					else
						genericMenu.OnClickNone(targetNode);
				}

				break;
			default:
				break;
			}
		}

		public static bool HandleNodeInput(DragNode node, Event e, GraphCamera camera)
		{
			bool inputProcessed = false;
			if( !node.IsDragging ) {
				// let the pins handle the input first
//                foreach (var pin in node.InputPins)
//                {
//                    if (inputProcessed) break;
//                    inputProcessed |= HandlePinInput(pin, e, camera);
//                }
//                foreach (var pin in node.OutputPins)
//                {
//                    if (inputProcessed) break;
//                    inputProcessed |= HandlePinInput(pin, e, camera);
//                }
			}

			var mousePosition = e.mousePosition;
			var mousePositionWorld = camera.ScreenToWorld( mousePosition );
			int dragButton = 0;
			// If the pins didn't already handle the input, then let the node handle it
			if( !inputProcessed ) {
				bool insideRect = node.Bounds.Contains( mousePositionWorld );
				if( e.type == EventType.MouseDown && insideRect && e.button == dragButton ) {
					node.IsDragging = true;
					inputProcessed = true;
				} else if( e.type == EventType.MouseUp && insideRect && e.button == dragButton ) {
					node.IsDragging = false;
				}
			}

			if( node.IsDragging && !node.Selected ) {
				node.IsDragging = false;
			}

			if( node.IsDragging && e.type == EventType.MouseDrag ) {
				inputProcessed = true;
			}

			return inputProcessed;
		}


		void HandleBoxSelection( Rect boundsScreenSpace )
		{
			bool multiSelect = keyboardState.ShiftPressed;
			bool selectedStateChanged = false;
			foreach( var node in DragNodes() ) {
				// node bounds in world space

				var selected = node.Bounds.Overlaps( boundsScreenSpace );
				if( multiSelect ) {
					if( selected ) {
						selectedStateChanged |= SetSelectedState( node, selected );
					}
				} else {
					selectedStateChanged |= SetSelectedState( node, selected );
				}


				if( selectedStateChanged ) {
					OnNodeSelectionChanged();
				}
			}
		}

		public void OnNodeSelectionChanged()
		{
			// Fetch all selected nodes
			var selectedNodes = from node in DragNodes()
			                    where node.Selected
			                    select node.Asset();
			int count = selectedNodes.Count( n => true );

			if( count > 0 ) {
				Selection.objects = selectedNodes.ToArray();
			} else {
				OnNoneSelectedNode();
			}

		}

		public abstract void OnNoneSelectedNode();


		bool SetSelectedState( DragNode node, bool selected )
		{
			bool stateChanged = ( node.Selected != selected );
			node.Selected = selected;
			return stateChanged;
		}


		void HandleClick( Event e )
		{
			switch( e.type ) {
			case EventType.MouseDown:
				break;
			case EventType.MouseDrag:
				
				break;
			default:
				break;
			}
		}

		protected DragNode SelectedNode( Vector2 worldPosition )
		{
			foreach( var item in DragNodes() ) {
				if( item.Bounds.Contains( worldPosition ) ) {
					return item;
				}
			}
			return null;
		}

		void OnSelectionChange()
		{
			GameObject[] gameObjs = Selection.gameObjects;
			if(gameObjs.Length > 0)
			{
				BTFsm.HasPrefab = PrefabUtility.GetPrefabObject( gameObjs[0] ) != null;
				Debug.Log( BTFsm.HasPrefab );
			}
		}
	}
}

