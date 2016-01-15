using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace wuxingogo.Runtime
{

	public class DisableAttribute : PropertyAttribute
	{
		public bool IsEditInEditor {
			get {
				return isEditInEditor;
			}
			set {
				isEditInEditor = value;
			}
		}

		private bool isEditInEditor = false;

		public DisableAttribute() : this( false )
		{
			
		}

		public DisableAttribute(bool isEditInEditor)
		{
			this.isEditInEditor = isEditInEditor;
		}
	}


	#if UNITY_EDITOR

	[CustomPropertyDrawer( typeof( DisableAttribute ) )]
	public class DisableDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var disableAttr = base.attribute as DisableAttribute;
			if( disableAttr.IsEditInEditor && !EditorApplication.isPlaying ) {
				EditorGUI.PropertyField( position, property, label );
			} else {
				EditorGUI.BeginDisabledGroup( true );
				EditorGUI.PropertyField( position, property, label );
				EditorGUI.EndDisabledGroup();
			}
		}
	}

	#endif
}