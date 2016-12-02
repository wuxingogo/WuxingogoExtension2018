//
//  nSendMsgWithArgActio.cs
// 
//
//  Created by TSPlay on 9/23/2016.
//
//
using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle( "Unity/SendMsgWithArgAction" )]
public class SendMsgWithArgAction : BTAction
{
    public bool isEveryFrame = false;
    public string msgName = "";
    public BTVariable arg = null;
    public override void OnAwake()
    {
        base.OnAwake();
        arg = Fsm.FindVar( arg.Name );
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Fsm.SendMessage( msgName, arg, SendMessageOptions.DontRequireReceiver );
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if( isEveryFrame )
        {
            Fsm.SendMessage( msgName, arg, SendMessageOptions.DontRequireReceiver );
        }
    }
}
