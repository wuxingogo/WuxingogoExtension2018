//
//  MoveVectorOperation.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UnityEngine;
using wuxingogo.Runtime;

namespace wuxingogo.Math
{
	[ExecuteInEditMode]
	public class MoveVectorOperation : XMonoBehaviour
	{
		[SerializeField]
		protected Transform owner = null;
		[SerializeField]
		protected Transform target = null;

		void Start ()
		{
		    
		}

		[X]
		protected void OwnerToTarget ()
		{
			var value = target.position - owner.position;
			//长度(距离)
			Debug.Log (string.Format ("magnitude is {0} => {1} : " + value.magnitude, owner.name, target.name));

			Debug.Log (string.Format ("sqrMagnitude is {0} => {1} : " + value.sqrMagnitude, owner.name, target.name));
			// 方向
			Debug.Log (string.Format ("nommalized is {0} => {1} " + value.normalized, owner.name, target.name));
		}

		[X]
		protected void OwnerToTargetDot ()
		{
			var lhs = owner.position;
			var rhs = target.position;

			var result = (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * lhs.z);
			Debug.Log (string.Format ("Dot is {0}", result));
			Debug.LogFormat ("Dot is {0}", Vector3.Dot (lhs, rhs));

			var norDot = Vector3.Dot (lhs.normalized, rhs.normalized);
			Debug.LogFormat ("normalize Dot is {0}", norDot);
			//求3方向夹角
			result = Mathf.Acos (norDot) * Mathf.Rad2Deg;
			Debug.Log (string.Format ("Angle is {0}", result));
		}

		[X]
		protected void OwnerToTargetAngle ()
		{
			var diff = owner.position - target.position;

			Debug.LogFormat ("XY Angle is {0}", Mathf.Acos (diff.y / diff.x) * Mathf.Rad2Deg);
		}
	}
}

