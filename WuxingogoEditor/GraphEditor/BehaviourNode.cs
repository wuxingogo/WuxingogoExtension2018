using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GraphEditor
{
    public class BehaviourNode : BaseNode
    {

        public BehaviourNode() : base()
        {
            GraphType = NodeType.Behaviour;
            GraphTitle = "BeahaviourNode";
        }
    }
}