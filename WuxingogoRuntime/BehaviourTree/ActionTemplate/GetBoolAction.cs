//
//  GetBoolAction.cs
//  
//
//  Created by TSPlay on 9/18/2016.
//
//
using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle("Variable/GetBoolAction")]
public class GetBoolAction : BTAction
{
    public override void OnCreate()
    {
        base.OnCreate();
        var ownerState = Owner;
        if( ownerState.FindEvent( "NO" ) == null )
        {
            var newEvent = BTEvent.Create( Owner );
            newEvent.Name = "NO";
        }
        if( ownerState.FindEvent( "YES" ) == null )
        {
            var newEvent = BTEvent.Create( Owner );
            newEvent.Name = "YES";
        }
    }

    public BoolVar variable = null;
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

        if( variable.Value )
            Fsm.FireEvent( "YES" );
        else
            Fsm.FireEvent( "NO" );

    }
}
