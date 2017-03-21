using UnityEngine;
using System.Collections;

namespace wuxingogo.tools{
	public class SectorShape : MonoBehaviour
	{
		public float m_Radius = 0.0f;
		[Range( 0, 360 )]
		public float angle = 0.0f;

		[Range(0.01f, 180f)]
		public float m_Theta = 1.0f;

		public Transform Target = null;
		[SerializeField] Color _color = Color.red;

		public bool isDebug = true;

		void OnDrawGizmos()
		{
			if( !isDebug )
				return;
			// set matrix
			Matrix4x4 defaultMatrix = Gizmos.matrix;
			Matrix4x4 newMatrix = Matrix4x4.identity;
			newMatrix.SetTRS(this.transform.position,Quaternion.Euler(new Vector3(0f,this.transform.eulerAngles.y,0f)),Vector3.one);

			Gizmos.matrix = newMatrix;

			// set color 
			Color defaultColor = Gizmos.color;
			Gizmos.color = _color;

			// draw circle
			Vector3 beginPoint = Vector3.zero;
			Vector3 firstPoint = Vector3.zero;
			for( float theta = 90 - angle / 2; theta <= 90; theta += m_Theta ) {
				float x = m_Radius * Mathf.Cos( theta * Mathf.Deg2Rad );
				float z = m_Radius * Mathf.Sin( theta * Mathf.Deg2Rad );
				Vector3 endPoint = new Vector3( x, 0, z );


				Gizmos.DrawLine( firstPoint, endPoint );

				beginPoint = endPoint;
			}
			for( float theta = 90; theta <= 90 + angle / 2; theta += m_Theta ) {
				float x = m_Radius * Mathf.Cos( theta * Mathf.Deg2Rad );
				float z = m_Radius * Mathf.Sin( theta * Mathf.Deg2Rad );
				Vector3 endPoint = new Vector3( x, 0, z );
				Gizmos.DrawLine( firstPoint, endPoint );
				beginPoint = endPoint;
			}

			// draw last line 
			Gizmos.DrawLine( firstPoint, beginPoint );

			// restore default color
			Gizmos.color = defaultColor;

			// restore default matrix
			Gizmos.matrix = defaultMatrix;


		}
		void OnRenderObject()
		{
			if( !isDebug )
				return;
			GL.PushMatrix();

			Matrix4x4 newMatrix = Matrix4x4.identity;
			newMatrix.SetTRS(this.transform.position,Quaternion.Euler(new Vector3(0f,this.transform.eulerAngles.y,0f)),Vector3.one);

			// match our transform
			GL.MultMatrix( newMatrix );
			// Draw lines
		
			GL.Begin( GL.LINES );
			GL.Color(_color);
			// draw circle
			Vector3 beginPoint = Vector3.zero;
			Vector3 firstPoint = Vector3.zero;

			for( float theta = 90 - angle / 2; theta <= 90; theta += m_Theta ) {
				float x = m_Radius * Mathf.Cos( theta * Mathf.Deg2Rad );
				float z = m_Radius * Mathf.Sin( theta * Mathf.Deg2Rad );
				Vector3 endPoint = new Vector3( x, 0, z );

				// One vertex at transform position

				// Another vertex at edge of circle
				GL.Vertex( firstPoint );
				GL.Vertex( endPoint );

				beginPoint = endPoint;
			}
			for( float theta = 90; theta <= 90 + angle / 2; theta += m_Theta ) {
				float x = m_Radius * Mathf.Cos( theta * Mathf.Deg2Rad );
				float z = m_Radius * Mathf.Sin( theta * Mathf.Deg2Rad );
				Vector3 endPoint = new Vector3( x, 0, z );

				// One vertex at transform position
				// Another vertex at edge of circle
				GL.Vertex( firstPoint );
				GL.Vertex( endPoint );

				beginPoint = endPoint;
			}
			GL.End();
			GL.PopMatrix();
		}
		public bool isInSector()
		{
			Vector3 forward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;
			if( Target != null ) {
				
				if( MathUtlis.IsPointInCircularSector( transform.position.x, transform.position.z, forward.x, forward.z,
					    m_Radius, Mathf.Deg2Rad * angle / 2, Target.position.x, Target.position.z ) ) {
					return true;
				}
			}
			return false;
		}
	}
}
