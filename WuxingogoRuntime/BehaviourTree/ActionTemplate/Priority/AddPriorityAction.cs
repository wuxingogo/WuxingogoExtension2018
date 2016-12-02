//
//  AddPriority.cs
// 
//
//  Created by wuxingogo on 9/29/2016.
//
//
using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle("Priority/AddPriority")]
public class AddPriorityAction : BTAction
{
    public BTState targetState = null;
    public int Increment = 10;
    public override void OnEnter()
    {
        base.OnEnter();

        if( targetState != null )
            targetState = Fsm.FindState( targetState );

        targetState.Priority += Increment;
    }
}
