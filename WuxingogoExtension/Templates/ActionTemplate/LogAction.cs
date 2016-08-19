using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;


	public class LogAction : BTAction {

		public bool isEveryFrame = false;
		public string content = "";
		public override void OnEnter()
		{
			base.OnEnter();
			Debug.Log( content );
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if(isEveryFrame)
			{
				Debug.Log( content );
			}
		}
	}


