using wuxingogo.Runtime;
using System;
using UnityEngine;

namespace wuxingogo.btFsm
{
	public class BTAction : XScriptableObject
	{
        [X]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
		public BTState Owner = null;

        public BTFsm Fsm
        {
            get
            {
                return Owner.Owner;
            }
        }

        public virtual void OnCreate()
        {

        }

        public virtual void OnAwake()
        {

        }


		public virtual void OnEnter()
		{

		}

		public virtual void OnExit()
		{

		}

		public virtual void OnUpdate()
		{

		}

		public virtual void OnCollisionEnter(Collision collision)
		{
			
		}
		public virtual void OnCollisionExit(Collision collision)
		{
			
		}
		public virtual void OnCollisionStay(Collision collision)
		{
			
		}
		public virtual void OnTriggerEnter(Collider collider)
		{
			
		}
		public virtual void OnTriggerExit(Collider collider)
		{
			
		}
		public virtual void OnTriggerStay(Collider collider)
		{
			
		}

		public void Finish()
		{
			Owner.Finish();
		}

        public GameObject gameObject
        {
            get
            {
                return Owner.gameObject;
            }
        }

        public Transform transform
        {
            get
            {
                return Owner.transform;
            }
        }

        public static T CreateAction<T>(BTState parentState) where T : BTAction
        {
        	var action = XScriptableObject.CreateInstance<T>();
        	action.Owner = parentState;
        	parentState.totalActions.Add(action);
        	return action;
        }

        public static BTAction CreateAction(BTAction source, BTState parentState)
		{
//			BTAction action = XScriptableObject.CreateInstance(source.GetType()) as BTAction;
			BTAction action = Instantiate<BTAction>(source);
            action.Name = source.Name;
            action.Owner = parentState;
			parentState.totalActions.Add(action);
			

            return action;
		}


	}

}
