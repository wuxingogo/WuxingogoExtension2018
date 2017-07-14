using System;
using System.Reflection;
using UnityEngine;

namespace wuxingogo.tools
{
	public static class ExtensionMethod
	{
		#region ==== Reflection
		public static T GetAttribute<T>(this MemberInfo type) where T : Attribute{
			var attributes = type.GetCustomAttributes (typeof(T), true);
			if (attributes.Length > 0) {
				return (T)attributes [0];
			}
			return null;
		}

		public static T[] GetAttributes<T>(this MemberInfo type) where T : Attribute{
			var attributes = type.GetCustomAttributes (typeof(T), true);
	
			return (T[])attributes;
		}

		#endregion

		#region ==== Transform
		 
		public static void SetPositionX(this Transform t, float newX)  
		{  
			t.position = new Vector3(newX, t.position.y, t.position.z);  
		}  

		public static void SetPositionY(this Transform t, float newY)  
		{  
			t.position = new Vector3(t.position.x, newY, t.position.z);  
		}  

		public static void SetPositionZ(this Transform t, float newZ)  
		{  
			t.position = new Vector3(t.position.x, t.position.y, newZ);  
		}  

		public static float GetPositionX(this Transform t)  
		{  
			return t.position.x;  
		}  

		public static float GetPositionY(this Transform t)  
		{  
			return t.position.y;  
		}  

		public static float GetPositionZ(this Transform t)  
		{  
			return t.position.z;  
		} 

		public static T As<T>(this Component c)where T : Component{
			var component = c.GetComponent<T> ();
			if (component == null) {
				component = c.gameObject.AddComponent<T> ();
			}
			return component;
		}

		public static T As<T>(this GameObject g)where T : Component{
			var component = g.GetComponent<T> ();
			if (component == null) {
				component = g.AddComponent<T> ();
			}
			return component;
		}

		#endregion

	}
}

