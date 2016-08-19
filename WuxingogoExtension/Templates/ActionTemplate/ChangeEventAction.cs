using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

public class ChangeEventAction : BTAction {

	public bool isEveryFrame = false;
	public BTEvent fireEvent = null;
	public override void OnEnter()
	{
		base.OnEnter();

	}

}
