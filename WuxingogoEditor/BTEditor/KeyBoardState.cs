using System.Collections.Generic;
using UnityEngine;


namespace wuxingogo.Node{
	

	#region Key board Listener
	/// <summary>
	/// Caches the keyboard state 
	/// </summary>
	class KeyBoardState
	{
		Dictionary<KeyCode, bool> state = new Dictionary<KeyCode, bool>();
		bool shift;
		bool control;
		bool alt;

		public void SetState(KeyCode keyCode, bool pressed)
		{
			if( !state.ContainsKey( keyCode ) ) {
				state.Add( keyCode, false );
			}
			state[keyCode] = pressed;
		}

		public void HandleInput(Event e)
		{

			if( e.type == EventType.KeyDown ) {
				SetState( e.keyCode, true );
			} else if( e.type == EventType.KeyUp ) {
				SetState( e.keyCode, false );
			}

			alt = e.alt;
			shift = e.shift;
			control = e.control || e.command;
		}

		public bool GetSate(KeyCode keyCode)
		{
			if( !state.ContainsKey( keyCode ) ) {
				return false;
			}
			return state[keyCode];
		}

		public bool ControlPressed {
			get {
				return control;
			}
		}

		public bool ShiftPressed {
			get {
				return shift;
			}
		}

		public bool AltPressed {
			get {
				return alt;
			}
		}
	}

	#endregion
}