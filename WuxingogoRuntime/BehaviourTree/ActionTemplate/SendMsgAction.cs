using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
[ActionTitle( "Unity/SendMsg Action" )]
public class SendMsgAction : BTAction {

	public bool isEveryFrame = false;
	public string msgName = "";
	public override void OnEnter()
	{
		base.OnEnter();
		Fsm.SendMessage( msgName, SendMessageOptions.DontRequireReceiver  );
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		if(isEveryFrame)
		{
			Fsm.SendMessage( msgName, SendMessageOptions.DontRequireReceiver  );
		}
	}
}
