using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

public class FinishAction : BTAction
{

    public override void OnEnter()
    {
        Finish();
    }
}
