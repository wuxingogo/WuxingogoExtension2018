using UnityEngine;
using System.Collections;
namespace wuxingogo.btFsm{

	public class DynamicComponent : MonoBehaviour {

		// Use this for initialization
		void Start () {
			var btFsm = gameObject.AddComponent<BTFsm>();
			var btEvent = btFsm.CreateStartEvent();
			var btState = BTState.Create<BTState>(btFsm);
			btState.Name = "Dynamic1State";
			btEvent.TargetState = btState;
			btState.Bounds = new Rect (250, 200, 100, 100);

			

			var btState1 = BTState.Create<BTState> (btFsm);
			btState1.Name = "Dynamic2State";
			btState1.Bounds = new Rect (400, 200, 100, 100);

			var privateEvent = BTEvent.Create (btState);
			privateEvent.Name = "Finish";
			privateEvent.TargetState = btState1;

			var privateEvent2 = BTEvent.Create (btState1);
			privateEvent2.Name = "Finish";
			privateEvent2.TargetState = btState;

			var waitAction = BTAction.CreateAction<WaitingAction>(btState);
			waitAction.time = 1;
			var waitAction1 = BTAction.CreateAction<WaitingAction>(btState1);
			waitAction1.time = 1;
		}
	}
}