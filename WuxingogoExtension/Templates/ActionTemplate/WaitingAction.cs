using UnityEngine;
using System.Collections;

public class WaitingAction : BTAction {

	public bool isFinish = false;
	public float secounds = 1.0f;
	public float time = 0.0f;
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
		if(time > secounds)
		{
			Finish();
		}

	}
}
