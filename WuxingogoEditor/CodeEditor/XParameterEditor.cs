using UnityEngine;
using UnityEditor;
using System.Collections;
using System;


namespace wuxingogo.Code
{

	public class XParameterEditor : ScriptableWizard
	{

		internal Vector2 _scrollPos = Vector2.zero;
		const int Xoffset = 5;
		const int XButtonWidth = 100;
		const int XButtonHeight = 20;

		internal XCodeParameter target = null;

		public int typeID = 0;

		public static T Init<T>(XCodeParameter target) where T : XParameterEditor
		{
			string cmdPrefs = typeof( T ).ToString() + "_isPrefix";
			bool isPrefix = EditorPrefs.GetBool( cmdPrefs, false );
			T window = XParameterEditor.DisplayWizard<T>( typeof( T ).Name, "Type", "Create" );
			window.OnInitialization( target );
			return window;
		}

		public void OnInitialization(XCodeParameter target)
		{
			this.target = target;
		}

		protected override bool DrawWizardGUI()
		{
			GUILayout.Box( XResources.LogoTexture, GUILayout.Width( this.position.width - Xoffset ), GUILayout.Height( 100 ) );
			if( GUI.Button( GUILayoutUtility.GetLastRect(), XResources.LogoTexture ) ) {
				this.Close();

			}

			if( null != target ) {
				typeID = CreateSelectableFromString( typeID, StrTypes );
//				target.type = StrTypes[typeID];

//				target.value = CreateStringField( "value", target.value );
			}

			return true;
		}

		public void DoButton(GUIContent content, Action callback, params GUILayoutOption[] options)
		{
			if( GUILayout.Button( content, options ) ) {
				callback();
			}
		}

		public string CreateStringField(string fieldName, string value)
		{
			return EditorGUILayout.TextField( fieldName, value );
		}

		public int CreateSelectableFromString(int rootID, string[] array)
		{
			return EditorGUILayout.Popup( array[rootID], rootID, array );
		}

		internal string[] StrTypes = new string[] {
			"void",
			"int",
			"float",
			"string",
			"UnityObject",
			"enum"
		};
	}
}
