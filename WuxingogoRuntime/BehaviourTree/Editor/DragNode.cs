using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using wuxingogo.tools;
using wuxingogo.Runtime;
using UnityEditor.Graphs;

namespace wuxingogo.Node
{
	public abstract class DragNode : UnityEditor.Graphs.Node
    {
		private bool isDragging = false;
        [X]
        public bool IsDragging {
			get {
				return isDragging;
			}
			set {
				isDragging = value;
			}
		}

		public bool Selected = false;
        [X]
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
        [X]
        public Vector2 DrawPosition {
			get {
				return drawPosition;
			}
			set {
				drawPosition = value;
			}
		}

		Vector2 position = Vector2.zero;
        [X]
        public Vector2 Position {
			get {
				return position;
			}
			set {
				position = value;

			}
		}

        [X]
		public virtual Rect DrawBounds
        {
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
            var sign = Mathf.Sign( dir.x );

            Vector3 startPos = new Vector3( lhs.x + Mathf.Max(0, lhs.width * -sign ), lhs.y + lhs.height / 2, 0 );
			Vector3 endPos = new Vector3( rhs.x + Mathf.Max( 0, rhs.width * sign ), rhs.y + rhs.height / 2, 0 );

			var distance = ( startPos - endPos ).magnitude * 0.5f;
			Vector3 startTan = startPos + Vector3.right * distance * -sign;
			Vector3 endTan = endPos + Vector3.left * distance * -sign;
			Color shadowCol = new Color( 0, 0, 0, .06f );

            for( int i = 0; i < 3; i++ )
            {
                Handles.DrawBezier( startPos, endPos, startTan, endTan, shadowCol, null, ( i + 1 ) * 5 );

                //var allPoint = Handles.MakeBezierPoints( startPos, endPos, startTan, endTan, ( i + 1 ) * 5 );
                //for( int j = 0; j < allPoint.Length; j++ )
                //{
                //    Handles.DrawPolyLine( 0, allPoint[j], Quaternion.identity, 10 );
                //}
                //Handles.DrawPolyLine( allPoint );
                //var totalPoint = GetPoints( startPos, endPos, startTan, endTan, ( i + 1 ) * 5 );
                //for( int j = 0; j < totalPoint.Count; j++ )
                //{
                //  Handles.MakeBezierPoints( totalPoint[j], 1 );
                //}

            }
           
            Handles.DrawBezier( startPos, endPos, startTan, endTan, Color.white, null, 3 );
            
            
		}

        private static List<Vector3> GetPoints( Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float detail )
        {
            //temporary list for final points on this segment
            List<Vector3> segmentPoints = new List<Vector3>();
            //multiply detail value to have at least 5-10+ iterations
            float iterations = detail * 10f;
            for( int n = 0; n <= iterations; n++ )
            {
                //cannot increment i as a float
                float i = ( float )n / iterations;
                float rest = ( 1f - i );
                //bezier formula
                Vector3 newPos = Vector3.zero;
                newPos += p0 * rest * rest * rest;
                newPos += p1 * i * 3f * rest * rest;
                newPos += p2 * 3f * i * i * rest;
                newPos += p3 * i * i * i;
                //add calculated point to segment
                segmentPoints.Add( newPos );
            }
            //return points on this segment
            return segmentPoints;
        }


    }
}

