using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

public class SendMsgAction : BTAction {

	public bool isEveryFrame = false;
	public string msgName = "";
	public string paras = "";
	public override void OnEnter()
	{
		base.OnEnter();
		Owner.Owner.SendMessage( msgName, paras, SendMessageOptions.DontRequireReceiver  );
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		if(isEveryFrame)
		{
			Owner.Owner.SendMessage( msgName, paras, SendMessageOptions.DontRequireReceiver  );
		}
	}
}
