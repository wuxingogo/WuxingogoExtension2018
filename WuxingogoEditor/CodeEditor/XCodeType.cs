using UnityEngine;
using System.Collections;
using System;
using wuxingogo.Runtime;
using System.Collections.Generic;
using wuxingogo.Code;




namespace wuxingogo.Code
{
    [System.Reflection.Obfuscation]
    [System.Serializable]
	public class XCodeType
	{
		[SerializeField] string typeName = string.Empty;

		public XCodeType(string typeName)
		{
			this.typeName = typeName;
		}
		public XCodeType(Type type)
		{
			this.typeName = type.AssemblyQualifiedName;
			this.type = type;
		}
		private Type type;
		public Type Target {
			get
			{
				if( type != null )
					return type;
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
