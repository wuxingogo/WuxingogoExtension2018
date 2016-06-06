using UnityEngine;
using System.Collections;
using wuxingogo.Runtime;

namespace XBehaviorRunntime{
	public class XDebugLog : XBehaviorAction {
		
		public string info = "";
		public override void OnStart()
		{
			base.OnStart();
			Logger.Log(info);
		}
	}
	
}
