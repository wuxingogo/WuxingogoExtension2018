using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
using System.Linq;
using System.Collections.Generic;

[StateTitle( "Priority/Select State with highest priority" )]
[Desc( "Priority State : All State will be sorting on fsm enter state. Fire the BTEvent with priority highest.")]
public class PrioritySelectState : BTState
{

    public List<BTEvent> sortingEvents = new List<BTEvent>();

    public override void OnEnter()
    {
        base.OnEnter();

        Sorting();
    }

    public virtual void Sorting()
    {

        BTEvent highestEvent = null;

        for( int i = 0; i < totalEvent.Count; i++ )
        {
            var currEvent = totalEvent[i];
            if( currEvent.TargetState == null )
                continue;
            else if( highestEvent == null )
                highestEvent = currEvent;
            else if( currEvent.TargetState.Priority > highestEvent.TargetState.Priority )
                highestEvent = currEvent;
        }

        if( highestEvent != null )
            Owner.FireEvent( highestEvent );

    }
}
