using UnityEngine;
using System.Collections;
using wuxingogo.Runtime;

namespace wuxingogo.Math
{
	[ExecuteInEditMode]
	public class RotateOperation : XMonoBehaviour
	{
		protected Vector3 euler = Vector3.zero;

		[X]
		protected Vector3 quaternionEuler {
			get {
				return transform.rotation.eulerAngles;
			}set {
				euler = value;
				transform.rotation = Quaternion.Euler (euler);
			}
		}

		[SerializeField]
		Matrix4x4 localToWorldMatrix;

		[SerializeField]
		Matrix4x4 worldToLocalMatrix;

		protected Quaternion quaternion;

		[X]
		protected Quaternion Quaternion {
			get {
				return transform.rotation;
			}
			set {
				quaternion = value;
				transform.rotation = quaternion;
			}
		}


		protected Vector3 eulerAngles = Vector3.zero;

		[X]
		protected Vector3 EulerAngles {
			get {
				return transform.eulerAngles;
			}
			set {
				eulerAngles = value;
				transform.eulerAngles = eulerAngles;
			}
		}

		protected Vector3 forward = Vector3.zero;

		[X]
		protected Vector3 Froward {
			get {
				return transform.forward;
			}
			set {
				forward = value;
				transform.forward = forward;
			}
		}

		[X]
		public void Reset ()
		{
			transform.rotation = Quaternion.identity;
		}


		void Start ()
		{
			forward = transform.forward;

			localToWorldMatrix = transform.localToWorldMatrix;

			worldToLocalMatrix = transform.worldToLocalMatrix;

			quaternion = transform.rotation;

			eulerAngles = transform.eulerAngles;
		}
	}
}