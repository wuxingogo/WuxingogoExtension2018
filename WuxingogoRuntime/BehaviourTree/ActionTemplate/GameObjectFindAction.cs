using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle( "GameObject/Find GameObject Action" )]
public class FindGameObjectAction : BTAction {

    public GameObjectVar variable = null;
    public bool isLocal = true;
    public string path = null;

    public override void OnAwake()
    {
        base.OnAwake();
        variable = Fsm.FindVar<GameObjectVar>( variable.Name );
    }
    public override void OnEnter()
    {
        base.OnAwake();
        
        if( isLocal )
            variable.Value = Fsm.transform.Find( path ).gameObject;
        else
            variable.Value = GameObject.Find( path );



    }
}
