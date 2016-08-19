using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using wuxingogo.btFsm;

public class UnityEventAction : BTAction {

	public UnityEvent onEnter;
	public UnityEvent onUpdate;
	public override void OnEnter()
	{
		base.OnEnter();
		onEnter.Invoke();
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		onUpdate.Invoke();
	}
}
