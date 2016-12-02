using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
using wuxingogo.Runtime;

[ActionTitle( "Wait/WaitingAction" )]
public class WaitingAction : BTAction {

	public float secounds = 1.0f;
    [Disable]
	public float time = 0.0f;
    public string noFinishEvent = null;

    public override void OnCreate()
    {
        base.OnCreate();
        var ownerState = Owner;
        if( ownerState.FindEvent( "Finish" ) == null )
        {
            var newEvent = BTEvent.Create( Owner );
            newEvent.Name = "Finish";
        }
    }

    public override void OnEnter()
	{
		base.OnEnter();
		time = 0.0f;
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		time += Time.deltaTime;
        if( time > secounds )
        {
            Finish();
        }
        else if(!string.IsNullOrEmpty( noFinishEvent ) )
        {
            Fsm.FireEvent( noFinishEvent );
        }

	}
}
