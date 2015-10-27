using UnityEngine;
using System.Collections;
using UnityEditor;

public class BehaviourNode : BaseNode {

	public BehaviourNode() : base()
    {
		GraphType = NodeType.Behaviour;
		GraphTitle = "BeahaviourNode";
	}
}
