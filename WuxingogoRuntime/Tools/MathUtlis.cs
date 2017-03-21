using UnityEngine;
using System.Collections;

namespace wuxingogo.tools{
	public class MathUtlis {

		public static bool IsPointInCircularSector(
	    float cx, float cy, float ux, float uy, float r, float theta,
	    float px, float py)
	    {
	        // D = P - C
	        float dx = px - cx;
	        float dy = py - cy;

	        // |D| = (dx^2 + dy^2)^0.5
	        float length = Mathf.Sqrt(dx * dx + dy * dy);

	        // |D| > r
	        if (length > r)
	            return false;

	        // Normalize D
	        dx /= length;
	        dy /= length;

	        // acos(D dot U) < theta
	        return Mathf.Acos(dx * ux + dy * uy) < theta;
	    }

		public static Vector3 NearlyPoint(Vector3 from, Vector3 to, float distance)
		{
			var v = from - to;
			if (v.magnitude > distance)
			{
				Vector3 p = (v.normalized) * distance + to;
				return p;
			}
			else
			{
				return from;
			}
		}
	}
}
