using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
using System;

public class FuncBoolAction : BTAction
{
    public Func<bool> func = null;
    public bool isUpdate = false;
    public sealed override void OnEnter()
    {
        base.OnEnter();
        var result = func();
        if( result )
	        Fsm.FireEvent( "YES" );
        else
            Fsm.FireEvent( "NO" );
    }
    public override void OnUpdate()
    {
        if( isUpdate )
        {
            var result = func();
            if( result )
                Fsm.FireEvent( "YES" );
            else
                Fsm.FireEvent( "NO" );
        }
    }
    public override void OnCreate()
    {
        base.OnCreate();
        var ownerState = Owner;
        if( ownerState.FindEvent( "YES" ) == null )
        {
            var newEvent = BTEvent.Create( Owner );
            newEvent.Name = "YES";
        }
        if( ownerState.FindEvent( "NO" ) == null )
        {
            var newEvent = BTEvent.Create( Owner );
            newEvent.Name = "NO";
        }
    }

}
