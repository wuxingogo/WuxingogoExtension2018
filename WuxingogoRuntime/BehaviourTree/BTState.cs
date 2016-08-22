
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
		public List<BTEvent> totalEvent = new List<BTEvent>();

		public List<BTAction> totalActions = new List<BTAction>();

		public BTEvent OwnerEvent = null;

		public BTFsm Owner = null;

		//	public BTState( BTEvent parentEvent )
		//	{
		//		OwnerEvent = parentEvent;
		//		Owner = parentEvent.Owner;
		//		Owner.AddNewState( this );
		//        AddObjectToObject();
		//
		//    }

		public BTState(BTFsm parentFsm)
		{
			Owner = parentFsm;
			Owner.AddNewState(this);

		}
		// Create from source template
		public BTState(BTFsm parentFsm, BTState source) : this(parentFsm)
		{
			Name = source.Name;
            // local state event
            for( int i = 0; i < source.totalEvent.Count; i++ )
            {
                var newEvent = BTEvent.Create( this, source.totalEvent[i] );
            }

            for (int i = 0; i < source.totalActions.Count; i++)
			{
				var newAction = BTAction.CreateAction(source.totalActions[i], this);
			}
            FindEvent( source.OwnerEvent );

        }

        public void FindEvent(BTEvent targetEvent)
        {
            if( targetEvent.isGlobal )
            {
                OwnerEvent = Owner.FindGlobalEvent( targetEvent.Name );
                if( OwnerEvent != null )
                    OwnerEvent.TargetState = this;
            }
            else
            {
                OwnerEvent = Owner.FindEvent( targetEvent.Name );
                if( OwnerEvent != null )
                    OwnerEvent.TargetState = this;
            }
            
        }

		public void OnEnter()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnEnter();
			}
		}

		public void OnUpdate()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnUpdate();
			}
		}

		public void OnExit()
		{
			for (int i = 0; i < totalActions.Count; i++)
			{
				totalActions[i].OnExit();
			}
		}

		public void Finish()
		{
			Owner.FireEvent("Finish");
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


		[HideInInspector]
		public Rect Bounds = new Rect(Screen.width / 2, Screen.height / 2, 100, 100);
	}
}
