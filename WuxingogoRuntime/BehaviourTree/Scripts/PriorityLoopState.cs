using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
using System.Linq;
using System.Collections.Generic;

[StateTitle("Priority/Loop All Child State")]
[Desc( "Priority State : All State will be sorting on fsm enter state. Then the fsm will be loops all child state. And Fire one name of \"Finish\" BTEvent On Updating" )]
public class PriorityLoopState : BTState
{

    public List<BTEvent> sortingEvents = new List<BTEvent>();

    public override void OnEnter()
    {
        base.OnEnter();

        Sorting();

    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        for( int i = 0; i < sortingEvents.Count; i++ )
        {
            if( sortingEvents[i].TargetState != null )
            {
                sortingEvents[i].TargetState.OnUpdate();
            }
        }
    }
    public override void OnExit()
    {
        base.OnEnter();

        for( int i = 0; i < sortingEvents.Count; i++ )
        {
            if( sortingEvents[i].TargetState != null )
            {
                sortingEvents[i].TargetState.OnExit();
            }
        }
    }

    public virtual void Sorting()
    {

        var notEmptyState = ( from t in totalEvent
                              where t.TargetState != null && t.Name != "Finish"
                              select t ).ToList();

        sortingEvents = ( from t in notEmptyState
                          orderby t.TargetState.Priority
                          select t ).ToList();

        Owner.FireEvent( "Finish" );

    }
}