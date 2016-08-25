using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using wuxingogo.Runtime;

namespace wuxingogo.btFsm
{

	[System.Serializable]
	public sealed class BTEvent
	{

        public string Name = "BTEvent";

		public BTFsm Owner = null;

		public BTState TargetState = null;

		public bool isGlobal = false;

		public static BTEvent Create(BTFsm Owner)
		{
			var btEvent = new BTEvent();
			btEvent.Owner = Owner;
			Owner.totalEvent.Add(btEvent);
			return btEvent;
		}

        public static BTEvent Create( BTState Owner )
        {
            var btEvent = new BTEvent();
            btEvent.Owner = Owner.Owner;
            Owner.totalEvent.Add( btEvent );
            return btEvent;
        }

        public static BTEvent Create(BTState Owner, BTEvent source)
		{
            var btEvent = new BTEvent();
            btEvent.Owner = Owner.Owner;
            btEvent.Name = source.Name;
            btEvent.isGlobal = source.isGlobal;
            Owner.totalEvent.Add( btEvent );
            return source;
		}

		public static BTEvent Create(BTFsm Owner, BTEvent source)
		{
			var btEvent = new BTEvent();
			btEvent.Owner = Owner;
			btEvent.isGlobal = source.isGlobal;
			btEvent.Name = source.Name;
			Owner.totalEvent.Add(btEvent);
			return btEvent;
		}

		public void OnEnter()
		{

		}

		public void OnExit()
		{

		}

		public void Finish()
		{

		}


	}
}
