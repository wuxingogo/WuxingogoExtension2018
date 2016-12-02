using UnityEngine;
using System.Collections;

using wuxingogo.btFsm;
[ActionTitle( "Unity/Debug Break Action" )]
public class DebugBreakAction : BTAction
{
	public bool onAwake = false;
	public bool onEnter = false;
	public bool onExit = false;
	public bool onUpdate = false;
	public override void OnAwake()
	{
		if( onAwake ) {
			Debug.Break();
			Debug.Log(string.Format("Debug Break Action When State OnAwake : {0},{1}", Fsm.Name, Owner.Name ), gameObject);
		}
	}
    public override void OnEnter()
    {
		if( onEnter ) {
			Debug.Break();
			Debug.Log(string.Format("Debug Break Action When State OnEnter : {0},{1}", Fsm.Name, Owner.Name ), gameObject);
		}
    }

	public override void OnExit()
	{
		if( onExit ) {
			Debug.Break();
			Debug.Log(string.Format("Debug Break Action When State OnExit : {0},{1}", Fsm.Name, Owner.Name ), gameObject);
		}
	}

	public override void OnUpdate()
	{
		if( onUpdate ) {
			Debug.Break();
			Debug.Log(string.Format("Debug Break Action When State OnUpdate : {0},{1}", Fsm.Name, Owner.Name ), gameObject);
		}
	}

}
