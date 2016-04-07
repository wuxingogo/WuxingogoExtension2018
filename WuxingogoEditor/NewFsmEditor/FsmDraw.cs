//
//  FsmDraw.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using UnityEngine;
using System.Collections;
using UnityEditor;


namespace behaviour
{

	public class FsmDraw : XBaseWindow
	{
		public void DrawBox(FsmState fsmState)
		{
			var stateRect = GetStateRect(fsmState);
			GUI.Box( stateRect, "", XStyles.GetInstance().window );


			for( int i = 0; i < fsmState.Count; i++ ) {
				//  TODO loop in fsmState.Count
				var rect = GetEventRect( fsmState[i] );
				if( GUI.Button( rect , fsmState[i].name ) ) {
					FsmEditor.selectEvent = fsmState[i];
				}
				DrawLink(fsmState[i], fsmState[i].target);

			}
			fsmState.name = GUI.TextField( new Rect( fsmState.position, stateRect.size ), fsmState.name, XStyles.GetInstance().box );
		}

		internal Rect GetStateRect(FsmState fsmState){
			var boxPos = fsmState.isPersent ? new Vector2( 120, 120 ) :  new Vector2( 100, 100 );
			return new Rect( fsmState.position, boxPos);
		}

		internal Rect GetEventRect(FsmEvent fsmEvent)
		{
			var fsmState = fsmEvent.owner;
			var index = fsmEvent.owner.FindIndex( fsmEvent );
			return new Rect( fsmState.position.x, fsmState.position.y + 25 + ( 80 / fsmState.Count * index ), 100, 100 / 2 / fsmState.Count );
		}

		public void DragLine(FsmEvent fsmEvent, Vector2 mousePosition)
		{
			if( fsmEvent != null ) {
				var rect = GetEventRect( fsmEvent );
				DrawBezier(PivotPosition(rect, new Vector2(1f,0.5f)), mousePosition);
			}
		}

		internal void DrawLink(FsmEvent fsmEvent, FsmState target){
			if( target != null ) {
				var rect = GetEventRect( fsmEvent );
				var targetRect = GetStateRect(target);
				DrawBezier(PivotPosition(rect, new Vector2(1f,0.5f)), PivotPosition(targetRect, new Vector2(0, 0.5f)));
			}
		}

		internal Vector2 PivotPosition(Rect original, Vector2 pivot)
		{
			return Vector2.Scale(original.size, pivot) + original.position;
		}

		internal void DrawBezier(Vector2 lhs, Vector2 rhs){
			Vector3 startPos = new Vector3( lhs.x, lhs.y, 0 );
			Vector3 endPos = new Vector3( rhs.x, rhs.y, 0 );
			
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
