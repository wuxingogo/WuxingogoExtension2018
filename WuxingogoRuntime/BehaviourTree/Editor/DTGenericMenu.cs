using System;
using UnityEngine;
using UnityEditor;


namespace wuxingogo.Node
{
	public abstract class DTGenericMenu<T> where T : MonoBehaviour
	{
		GenericMenu genericMenu = null;


		public abstract void OnClickNode(DragNode targetNode);

		public abstract void OnClickNone(T targetNode);


	}
}

