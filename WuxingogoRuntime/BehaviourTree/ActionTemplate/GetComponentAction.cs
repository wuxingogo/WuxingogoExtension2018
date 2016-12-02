using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;
using wuxingogo.Reflection;
using System;

[ActionTitle("Component/GetComponent")]
public class GetComponentAction : BTAction {

    public GameObjectVar gameObject = null;
    public ComponentVar variable = null;
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject = Fsm.FindVar<GameObjectVar>( gameObject.Name );
        variable = Fsm.FindVar<ComponentVar>( variable.Name );
        variable.Value = gameObject.Value.GetComponent( variable.Value.GetType() );
    }
}
