
using UnityEngine;
using System.Collections.Generic;
using wuxingogo.Runtime;
namespace wuxingogo.btFsm
{
	public class BTState : XScriptableObject
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

        public BTState Source = null;

        public List<BTEvent> totalEvent = new List<BTEvent>();

		public List<BTAction> totalActions = new List<BTAction>();

		public BTEvent GlobalEvent = null;

		public BTFsm Owner = null;

        public static BTState Create<T>( BTFsm parentFsm) where T : BTState
		{
            var newState = XScriptableObject.CreateInstance<T>();
            newState.Owner = parentFsm;
            parentFsm.AddNewState( newState );
            return newState;

		}

        public static BTState Create( BTFsm parentFsm, System.Type type )
        {
            var newState = XScriptableObject.CreateInstance( type ) as BTState;
            newState.Owner = parentFsm;
            parentFsm.AddNewState( newState );
            return newState;
        }

        public static BTState Create( BTFsm parentFsm, BTState source)
        {
            var newState = Instantiate<BTState>( source );
            newState.Source = source;
            newState.Name = source.Name;
            newState.Owner = parentFsm;
            newState.Owner.AddNewState( newState );

            newState.totalActions.Clear();
            for( int i = 0; i < source.totalActions.Count; i++ )
            {
                var newAction = BTAction.CreateAction( source.totalActions[i], newState );
            }

            newState.FindEvent( newState.GlobalEvent );
            
            return newState;
        }


        public BTEvent FindEvent( string eventName )
        {
            for( int i = 0; i < totalEvent.Count; i++ )
            {
                if( totalEvent[i].Name.Equals( eventName ) )
                {
                    return totalEvent[i];
                }
            }
            return null;
        }

        public void FindEvent(BTEvent targetEvent)
        {
            if( targetEvent.isGlobal )
            {
                GlobalEvent = Owner.FindGlobalEvent( targetEvent.Name );
                if( GlobalEvent != null )
                    GlobalEvent.TargetState = this;
            }
        }

        public bool Equals( BTState other )
        {
            return Source == other;
        }

        public virtual void OnCreate()
        {

        }
        public virtual void OnAwake()
        {
            for( int i = 0; i < totalActions.Count; i++ )
            {
                totalActions[i].OnAwake();
            }
        }

		public virtual void OnEnter()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnEnter();
			}
		}

        public virtual void OnUpdate()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnUpdate();
			}
		}

        public virtual void OnExit()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnExit();
			}
		}

		public virtual void OnCollisionEnter(Collision collision)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnCollisionEnter(collision);
			}
		}
		public virtual void OnCollisionExit(Collision collision)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnCollisionExit(collision);
			}
		}
		public virtual void OnCollisionStay(Collision collision)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnCollisionStay(collision);
			}
		}
		public virtual void OnTriggerEnter(Collider collider)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnTriggerEnter(collider);
			}
		}
		public virtual void OnTriggerExit(Collider collider)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnTriggerExit(collider);
			}
		}
		public virtual void OnTriggerStay(Collider collider)
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnTriggerStay(collider);
			}
		}

        public int Priority = 10;

        [X]
		public void Finish()
		{
            Owner.Finish();
		}
		[X]
		public BTState FireEvent(string eventName)
		{
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (eventName.Equals(totalEvent[i].Name))
				{
					return totalEvent[i].TargetState;
				}
			}
			return null;
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

        [HideInInspector]
		public Rect Bounds = new Rect(Screen.width / 2, Screen.height / 2, 100, 100);

        public string Notes = "";
	}
}
