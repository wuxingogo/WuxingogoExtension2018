using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle( "Variable/Component Variable" )]
public class ComponentVar : BTVariableT<Component>
{
    public override Component Value
    {
        get
        {
            return base.Value;
        }

        set
        {
            base.Value = value;
        }
    }

}
