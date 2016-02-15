using UnityEngine;
using System;

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
}