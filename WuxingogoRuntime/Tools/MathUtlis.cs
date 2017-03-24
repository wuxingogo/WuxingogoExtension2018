
//
// MathUtils.cs
//
// Author:
//       wuxingogo <>
//
// Copyright (c) 2017 wuxingogo
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
