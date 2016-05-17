using UnityEngine;
using System.Collections;

namespace wuxingogo.Math
{
	[ExecuteInEditMode]
	public class VectorAngleOperation : MonoBehaviour
	{
		[SerializeField]
		protected Transform v = null;
		[SerializeField]
		protected Transform v1 = null;
		// Use this for initialization
		void Start ()
		{
		

		}
	
		// Update is called once per frame
		void Update ()
		{
			Vector3 v = this.v.position;
			Vector3 v1 = this.v1.position;
			Vector3 dis = v1 - v;

			float delta = Mathf.Atan (dis.y / dis.x) * Mathf.Rad2Deg;
			Debug.Log ("delta is : " + delta);
		}
	}
}
