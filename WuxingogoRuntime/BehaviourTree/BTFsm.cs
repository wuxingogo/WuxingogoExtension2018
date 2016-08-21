using System.Collections.Generic;
using wuxingogo.Runtime;

namespace wuxingogo.btFsm
{
	public class BTFsm : XMonoBehaviour
	{

		public string Name = "BTFsm";

		public BTEvent startEvent = null;

		public BTState currState
		{
			get;
			set;
		}

		/// <summary>
		/// The total global event.
		/// </summary>
		public List<BTEvent> totalEvent = new List<BTEvent>();

		public List<BTState> totalState = new List<BTState>();

		public BTTemplate template = null;

		void Start()
		{
			if(template != null)
			{
				BTTemplate.CreateFromOwnerTemplate( this, template );
			}
			FireGlobalEvent("GlobalStart");
		}
		[X]
		public void FireGlobalEvent(string EventName)
		{
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
		}
		[X]
		public void FireEvent(string firePara)
		{
			var nextState = currState.FireEvent(firePara);
			if (nextState != null)
			{
				currState.OnExit();
				currState = nextState;
				currState.OnEnter();
			}
		}

		void OnEnable()
		{
            //currState.OnEnter ();
        }

        void OnDisable()
		{
            //currState.OnExit ();
        }

        void Update()
		{
			currState.OnUpdate();
		}



		public BTEvent CreateEvent(string name)
		{
			startEvent = BTEvent.Create(this);
			startEvent.Name = name;

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


		public bool ContainEvent(string name)
		{
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (totalEvent[i].Name.Equals(name))
					return true;
			}
			return false;
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

		public BTEvent FindEvent(string eventName)
		{
			for (int i = 0; i < totalEvent.Count; i++)
			{
				if (totalEvent[i].Name == eventName)
				{
					return totalEvent[i];
				}
			}
			return null;
		}
	}

}
