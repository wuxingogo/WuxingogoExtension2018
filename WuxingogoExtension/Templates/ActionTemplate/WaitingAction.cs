using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

public class WaitingAction : BTAction {

	public bool isFinish = false;
	public float secounds = 1.0f;
	public float time = 0.0f;
    public string noFinishEvent = null;
	public override void OnEnter()
	{
		base.OnEnter();
		isFinish = false;
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
            Owner.Owner.FireEvent( noFinishEvent );
        }

	}
}
