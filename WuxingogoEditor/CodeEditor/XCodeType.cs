using UnityEngine;
using System.Collections;
using System;
using wuxingogo.Runtime;
using System.Collections.Generic;
using wuxingogo.Code;




namespace wuxingogo.Code
{
	

	[System.Serializable]
	public class XCodeType
	{
		[SerializeField] string typeName = string.Empty;

		public XCodeType(string typeName)
		{
			this.typeName = typeName;
		}
		public XCodeType(Type type) : this(type.AssemblyQualifiedName)
		{
			
		}

		public Type Target {
			get
			{
				return Type.GetType( typeName);
			}
		}

		public override string ToString()
		{
			return string.Format("[XCodeType: Target={0}]", Target.Name.ToString());
		}
	}



	

	public enum XTypeEnum
	{
		Integer,
		Float,
		String,
		Double,
		Vector,
		GameObject
	}

	
}
