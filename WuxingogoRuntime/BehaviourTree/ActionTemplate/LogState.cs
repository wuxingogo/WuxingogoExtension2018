using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
[ActionTitle("Debug/Log State Name")]
public class LogState : BTAction {

    public override void OnCreate()
    {
        base.OnCreate();

        XLogger.LogFormat( Fsm, "OnCreate at {0}====={1}", Fsm, Owner.Name );
    }

    public override void OnAwake()
    {
        base.OnAwake();

        XLogger.LogFormat( Fsm, "OnAwake at {0}====={1}", Fsm, Owner.Name );
    }

    

    public override void OnEnter()
    {
        base.OnEnter();

        XLogger.LogFormat( Fsm, "OnEnter at {0}====={1}", Fsm, Owner.Name );
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        XLogger.LogFormat( Fsm, "OnUpdate at {0}====={1}", Fsm, Owner.Name );
    }

    public override void OnExit()
    {
        base.OnExit();

        XLogger.LogFormat( Fsm, "OnExit at {0}====={1}", Fsm, Owner.Name );
    }
}
