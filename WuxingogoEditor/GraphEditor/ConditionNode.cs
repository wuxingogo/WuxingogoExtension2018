using UnityEngine;
using System.Collections;

public class ConditionNode : BaseNode {

    public ConditionNode() : base()
    {
        GraphType = NodeType.Condition;
        GraphTitle = "ConditionNode";
    }
    public bool Value = false;
    public override void DrawGraph( int id )
    {
        base.DrawGraph( id );

        Event e = Event.current;

        if(GUILayout.Button(Value.ToString())){
            Value = !Value;
        }

        //m_Object = EditorGUILayout.ObjectField( m_Object, typeof( UnityEngine.Object ) );
        if( e.type == EventType.Repaint )
        {
            //JointRect = GUILayoutUtility.GetLastRect();
        }
    }
	
	
}
