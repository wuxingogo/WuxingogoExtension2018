using UnityEngine;
using System.Collections;
using wuxingogo.Runtime;
using System.Collections.Generic;

namespace wuxingogo.btFsm
{

	public class BTTemplate : XScriptableObject
	{

		public List<BTEvent> totalEvent = new List<BTEvent>();

		public List<BTState> totalState = new List<BTState>();

		public BTEvent startEvent = null;

		public BTTemplate(BTFsm source)
		{
			totalEvent = source.totalEvent;
			totalState = source.totalState;
			startEvent = source.startEvent;
		}

		public static BTFsm CreateFromOtherTemplate(BTFsm targetFsm, BTTemplate source)
		{
			for (int i = 0; i < source.totalEvent.Count; i++)
			{
				BTEvent.Create(targetFsm, source.totalEvent[i]);
			}

			for (int i = 0; i < source.totalState.Count; i++)
			{
				new BTState(targetFsm, source.totalState[i]);
			}

			targetFsm.startEvent = targetFsm.FindGlobalEvent(source.startEvent.Name);

			return targetFsm;
		}

		public static BTFsm CreateFromOwnerTemplate(BTFsm targetFsm, BTTemplate source)
		{
			for (int i = 0; i < source.totalEvent.Count; i++)
			{
				targetFsm.totalEvent[i].Owner = targetFsm;
			}
            targetFsm.totalState.Clear();

            for (int i = 0; i < source.totalState.Count; i++)
			{
                new BTState( targetFsm, source.totalState[i] );
            }
            targetFsm.startEvent = targetFsm.FindGlobalEvent( targetFsm.startEvent.Name );
            return targetFsm;
		}
	}
}
