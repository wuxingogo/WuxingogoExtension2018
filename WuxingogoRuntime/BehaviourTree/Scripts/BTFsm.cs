using System.Collections.Generic;
using UnityEngine;
using wuxingogo.Runtime;


namespace wuxingogo.btFsm
{
	public class BTFsm : XMonoBehaviour
	{
        public string Name = "BTFsm";

		public BTEvent startEvent = null;

        public BTState currState = null;

		/// <summary>
		/// The total global event.
		/// </summary>
		public List<BTEvent> totalEvent = new List<BTEvent>();
        
		public List<BTState> totalState = new List<BTState>();

        public List<BTVariable> totalVariable = new List<BTVariable>();

        public System.Action<BTState, BTEvent> OnFireEvent = null;

        public T FindVar<T>(string varName)where T : BTVariable
        {
            for( int i = 0; i < totalVariable.Count; i++ )
            {
                if( totalVariable[i].Name == varName )
                {
                    return totalVariable[i] as T;
                }
            }
            return null;
        }

        public BTVariable FindVar( string varName )
        {
            for( int i = 0; i < totalVariable.Count; i++ )
            {
                if( totalVariable[i].Name == varName )
                {
                    return totalVariable[i];
                }
            }
            return null;
        }

        public BTTemplate template = null;

        public bool isPrefab
        {
            get
            {
                return template != null;
            }
        }
        bool isPrepareFinish = false;
		void Start()
		{
			if(template != null)
			{
				BTTemplate.CreateFromOwnerTemplate( this, template );
			}
            for( int i = 0; i < totalState.Count; i++ )
            {
                totalState[i].OnAwake();
            }
            for( int i = 0; i < totalVariable.Count; i++ )
            {
                totalVariable[i].OnAwake();
            }
            isPrepareFinish = true;
            FireGlobalEvent("GlobalStart");
           

        }

		void Reset()
		{
			totalVariable = new List<BTVariable>();
			totalEvent = new List<BTEvent>();
			totalState = new List<BTState>();

			var startEvent = CreateStartEvent();
			startEvent.TargetState = BTState.Create<BTState>( this );
			startEvent.TargetState.GlobalEvent = startEvent;
			startEvent.TargetState.Name = "GlobalState";



			var type = wuxingogo.Reflection.XReflectionUtils.TryGetClass ("BTGenericMenu");
			if (type != null) {
				var method = type.GetMethod("AddStateToFsm");
				method.Invoke(null,new System.Object[]{this, startEvent.TargetState} );
			}
//			BTGenericMenu.AddStateToFsm( fsm, startEvent.TargetState );
//			BTEditorWindow.instance.Clear();
//			BTEditorWindow.target = fsm;
		}
		[X]
		public void FireGlobalEvent(string EventName)
		{
            if( !isPrepareFinish )
                return;
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (EventName == totalEvent[i].Name)
				{
					FireGlobalEvent(totalEvent[i]);
					break;
				}
			}
		}

		void FireGlobalEvent(BTEvent nextEvent)
		{
			if (currState != null)
			{
				currState.OnExit();
			}
			startEvent = nextEvent;
            currState = startEvent.TargetState;
			startEvent.OnEnter();
			startEvent.OnExit();
			startEvent.Finish();
			startEvent = null;
            currState.OnEnter();
            //currState.OnEnter();
            //nextState = null;
        }

        public void FireEvent( BTEvent nextEvent )
        {
            if( nextEvent != null )
            {
                if( nextState == null )
                {
                    nextState = nextEvent.TargetState;
                }
                if( OnFireEvent != null )
                    OnFireEvent( currState, nextEvent );
            }
        }
               
		[X]
		public void FireEvent(string firePara)
		{
			var nextEvent = currState.FindEvent( firePara);
            if( nextEvent != null )
            {
                if( nextState == null )
                {
                    // The next state must only one
                    nextState = nextEvent.TargetState;
                }
                if( nextState != null && OnFireEvent != null )
                    OnFireEvent( currState, nextEvent );
                
            }
            
		}
        public BTState nextState = null;

        public void Finish()
        {
            FireEvent( "Finish" );
        }

		void OnEnable()
		{
            //currState.OnEnter ();
        }

        void OnDisable()
		{
            //currState.OnExit ();
        }

		void OnCollisionEnter(Collision collision)
		{
			currState.OnCollisionEnter(collision);
		}
		void OnCollisionExit(Collision collision)
		{
			currState.OnCollisionExit(collision);
		}
		void OnCollisionStay(Collision collision)
		{
			currState.OnCollisionStay(collision);
		}
		void OnTriggerEnter(Collider collider)
		{
			currState.OnTriggerEnter(collider);
		}
		void OnTriggerExit(Collider collider)
		{
			currState.OnTriggerExit(collider);
		}
		void OnTriggerStay(Collider collider)
		{
			currState.OnTriggerStay(collider);
		}

        void Update()
		{
            if( nextState != null )
            {
                if( currState != null)
                    currState.OnExit();
                currState = nextState;
                nextState = null;
                currState.OnEnter();
                
            }
			if (currState == null) {
				Debug.LogError("BTFsm Current State is null", gameObject);
				return;
			}
            currState.OnUpdate();
        }

		public BTEvent CreateStartEvent()
		{
			return CreateEvent("GlobalStart");
		}

		public BTEvent CreateEvent(string name)
		{
			startEvent = BTEvent.Create(this);
			startEvent.Name = name;
            startEvent.isGlobal = true;
            return startEvent;
		}

		public void AddNewState(BTState state)
		{
			totalState.Add(state);
		}

		public void RemoveState(BTState state)
		{
			totalState.Remove(state);
		}

        public void RemoveVar( int index )
        {
            totalVariable.RemoveAt( index );
        }

        public bool ContainEvent(string name)
		{
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (totalEvent[i].Name.Equals(name))
					return true;
			}
			return false;
		}

        public BTState FindState( BTState targetState )
        {
            for( int i = 0; i < totalState.Count; i++ )
            {
                if( totalState[i].Source == targetState )
                {
                    return totalState[i];
                }
            }
            return null;
        }

		public BTState FindState(string stateName)
		{
			for (int i = 0; i < totalState.Count; i++)
			{
				if (totalState[i].Name == stateName)
				{
					return totalState[i];
				}
			}
			return null;
		}

		public BTEvent FindGlobalEvent(string eventName)
		{
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (totalEvent[i].Name.Equals( eventName))
				{
					return totalEvent[i];
				}
			}
			return null;
		}

        public BTEvent FindEvent( string eventName )
        {
            for( int i = 0; i < totalState.Count; i++ )
            {
                for( int j = 0; j < totalState[i].totalEvent.Count; j++ )
                {
                    if( totalState[i].totalEvent[j].Name == eventName )
                    {
                        return totalState[i].totalEvent[j];
                    }
                }
            }
            return null;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon( this.transform.position, "XLogo.jpg" );
        }
    }

}
