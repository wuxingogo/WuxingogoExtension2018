using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace wuxingogo.Node
{
	public abstract class DragNode
	{
		private bool isDragging = false;

		public bool IsDragging {
			get {
				return isDragging;
			}
			set {
				isDragging = value;
			}
		}

		public bool Selected = false;

		public virtual Rect Bounds{
			get;
			set;
		}
			
		public void MoveNode(Vector2 delta)
		{
			Bounds = new Rect( Bounds.position + delta, Bounds.size );
		}

		public abstract UnityEngine.Object Asset();

		public abstract void Draw();

		public virtual void OnSelected(){
			
		}

		protected Vector2 drawPosition = Vector2.zero;

		public Vector2 DrawPosition {
			get {
				return drawPosition;
			}
			set {
				drawPosition = value;
			}
		}

		Vector2 position = Vector2.zero;

		public Vector2 Position {
			get {
				return position;
			}
			set {
				position = value;

			}
		}


		public virtual Rect DrawBounds {
			get {
				return new Rect( Bounds.position + drawPosition, Bounds.size );
			}
		}

		public List<DragNode> ChildNodes = new List<DragNode>();

		public void DrawChildNodes()
		{
			for( int pos = 0; pos < ChildNodes.Count; pos++ ) {
				//  TODO loop in NextNodes.Count
				DrawBesizeFromRect( new Rect( DrawBounds.x, DrawBounds.y + ( DrawBounds.height / ChildNodes.Count * pos ), DrawBounds.width, DrawBounds.height / ChildNodes.Count )
					, ChildNodes[pos].DrawBounds );

			}
		}

		public static void DrawBesizeFromRect(Rect lhs, Rect rhs)
		{
			var dir = (lhs.position - rhs.position).normalized;
			Vector3 startPos = new Vector3( lhs.x + lhs.width, lhs.y + lhs.height / 2, 0 );
			Vector3 endPos = new Vector3( rhs.x, rhs.y + rhs.height / 2, 0 );

			var distance = ( startPos - endPos ).magnitude * 0.5f;
			Vector3 startTan = startPos + Vector3.right * distance;
			Vector3 endTan = endPos + Vector3.left * distance;
			Color shadowCol = new Color( 0, 0, 0, .06f );

			for( int i = 0; i < 3; i++ ) {
				Handles.DrawBezier( startPos, endPos, startTan, endTan, shadowCol, null, ( i + 1 ) * 5 );

			}
			Handles.DrawBezier( startPos, endPos, startTan, endTan, Color.gray, null, 1 );
		}


	}
}

