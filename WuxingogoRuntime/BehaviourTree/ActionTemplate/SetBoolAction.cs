//
//  SetBoolAction.cs
//  三生三世十里桃花
//
//  Created by TSPlay on 9/18/2016.
//
//
using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle( "Variable/SetBoolAction" )]
public class SetBoolAction : BTAction
{
    public BoolVar variable = null;
    public bool value = false;
    private bool isInit = false;
    public override void OnEnter()
    {
        base.OnEnter();

        if( variable == null )
            return;
        if( !isInit )
        {
            variable = Fsm.FindVar<BoolVar>( variable.Name );
            isInit = true;
        }

        variable.Value = value;

    }
}
