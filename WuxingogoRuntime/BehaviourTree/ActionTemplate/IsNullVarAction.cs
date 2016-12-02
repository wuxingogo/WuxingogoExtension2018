using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
[ActionTitle("Variable/Nullable Action")]
public class IsNullVarAction : BTAction {
    public BTVariable variable = null;
    private bool isSelf = false;
    public bool isUpdate = false;
    public override void OnAwake()
    {
        base.OnAwake();
        variable = Fsm.FindVar( variable.Name );
    }
    public override void OnEnter()
    {
        base.OnEnter();

        if( variable.isNull() )
            Fsm.FireEvent( "YES" );
        else
        {
            if( variable.isNull() )
                Fsm.FireEvent( "YES" );
            else
                Fsm.FireEvent( "NO" );
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if( isUpdate )
        {
            if( variable.isNull() )
                Fsm.FireEvent( "YES" );
            else
            {
                if( variable.isNull() )
                    Fsm.FireEvent( "YES" );
                else
                    Fsm.FireEvent( "NO" );
            }
        }
    }

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
}
