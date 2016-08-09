#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
#define Wuxingogo_Core
#endif
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
			AddObjectToObject();

		}

		public BTState(BTFsm parentFsm, BTState source) : this(parentFsm)
		{
			Name = source.Name;

			for (int i = 0; i < source.totalEvent.Count; i++)
			{
				var newEvent = BTEvent.Create(parentFsm, source.totalEvent[i]);
			}

			for (int i = 0; i < source.totalActions.Count; i++)
			{
				var newAction = BTAction.CreateAction(source.totalActions[i], this);
			}
			OwnerEvent = parentFsm.FindEvent(source.OwnerEvent.Name);

		}

		public void AddObjectToObject()
		{
#if Wuxingogo_Core

			if (BTFsm.HasPrefab)
			{
				UnityEditor.AssetDatabase.AddObjectToAsset(this, Owner);
				UnityEditor.EditorUtility.SetDirty(Owner);
			}
			else if (Owner.template != null)
			{
				UnityEditor.AssetDatabase.AddObjectToAsset(this, Owner.template);
				UnityEditor.EditorUtility.SetDirty(Owner.template);
			}


#endif
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

#if Wuxingogo_Core
		[HideInInspector]
		public Rect Bounds = new Rect(Screen.width / 2, Screen.height / 2, 100, 100);
#endif
	}
}
