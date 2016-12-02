using System;
using UnityEngine;


namespace wuxingogo.Node
{
	/// <summary>
	/// Manages the selection box for selecting multiple objects in the graph editor
	/// </summary>
	class GraphSelectionBox
	{
		public delegate void OnSelectionPerformed(Rect boundsScreenSpace);

		public event OnSelectionPerformed SelectionPerformed;

		// The bounds of the selection box in screen space
		Rect bounds = new Rect();

		public Rect Bounds {
			get {
				return bounds;
			}
			set {
				bounds = value;
			}
		}

		Vector2 dragStart = new Vector2();
		int dragButton = 0;
		bool dragging = false;

		public bool Dragging {
			get {
				return dragging;
			}
		}

		/// <summary>
		/// Handles user input (mouse)
		/// </summary>
		/// <param name="e"></param>
		public void HandleInput(Event e)
		{

			switch( e.type ) {
			case EventType.MouseDown:
				ProcessMouseDown( e );
				break;

			case EventType.MouseDrag:
				ProcessMouseDrag( e );
				break;

			case EventType.MouseUp:
				ProcessMouseUp( e );
				break;
			}
			// Handled captured mouse up event
			{
				var controlId = GUIUtility.GetControlID( FocusType.Passive );
				if( GUIUtility.hotControl == controlId && Event.current.rawType == EventType.MouseUp ) {
					ProcessMouseUp( e );
				}
			}
		}

		void ProcessMouseDrag(Event e)
		{
			if( dragging && e.button == dragButton ) {
				var dragEnd = e.mousePosition;
				UpdateBounds( dragStart, dragEnd );

				if( IsSelectionValid() && SelectionPerformed != null ) {
					SelectionPerformed( bounds );
				}
			}
		}

		void ProcessMouseDown(Event e)
		{
			if( e.button == dragButton ) {
				dragStart = e.mousePosition;
				UpdateBounds( dragStart, dragStart );
				dragging = true;
				GUIUtility.hotControl = GUIUtility.GetControlID( FocusType.Passive );
			}
		}

		void ProcessMouseUp(Event e)
		{
			if( e.button == dragButton && dragging ) {
				dragging = false;
				if( IsSelectionValid() && SelectionPerformed != null ) {
					SelectionPerformed( bounds );
				}
				GUIUtility.hotControl = 0;
			}
		}

		public bool IsSelectionValid()
		{
			return bounds.width > 0 && bounds.height > 0;
		}

		public void Draw()
		{
			if( !dragging || !IsSelectionValid() )
				return;

            var bg = GUI.backgroundColor;
            GUI.backgroundColor = new Color( 1, 0.6f, 0, 0.6f );
            GUI.Box( bounds, "" );
            GUI.backgroundColor = bg;
        }

		void UpdateBounds(Vector2 start, Vector2 end)
		{
			var x0 = Mathf.Min( start.x, end.x );
			var x1 = Mathf.Max( start.x, end.x );
			var y0 = Mathf.Min( start.y, end.y );
			var y1 = Mathf.Max( start.y, end.y );
			bounds.Set( x0, y0, x1 - x0, y1 - y0 );
		}

	}


}