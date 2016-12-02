using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
[ActionTitle( "BTFsm/Do Finish Action" )]
public class FinishAction : BTAction
{
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
        Finish();
    }
}
