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
		// targetFsm must be deactive.
		public static BTFsm Create(BTFsm targetFsm, BTTemplate source)
		{
			for (int i = 0; i < source.totalEvent.Count; i++)
			{
				BTEvent.Create(targetFsm, source.totalEvent[i]);
			}

			for (int i = 0; i < source.totalState.Count; i++)
			{
				//			targetFsm.totalState[i].Owner = targetFsm;
				new BTState(targetFsm, source.totalState[i]);
			}
			//		targetFsm.totalEvent = source.totalEvent;

			//		targetFsm.totalState = source.totalState;
			targetFsm.startEvent = targetFsm.FindEvent(source.startEvent.Name);

			return targetFsm;
		}
	}
}
