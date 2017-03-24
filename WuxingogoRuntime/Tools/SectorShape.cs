//
// SectorShape.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2016 ly-user
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
