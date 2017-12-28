using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace wuxingogo.tools
{
	public static class ExtensionMethod
	{
		public static T As<T>(this Component c) where T : Component{
			var component = c.GetComponent<T> ();
			if (component == null) {
				component = c.gameObject.AddComponent<T> ();
			}
			return component;
		}

		public static T As<T>(this GameObject g) where T : Component{
			var component = g.GetComponent<T> ();
			if (component == null) {
				component = g.AddComponent<T> ();
			}
			return component;
		}

		public static T[] GetComponentsAsArray<T>( this GameObject[] gameObjects, bool includeChildren = false ) where T : Component
		{
			return GetComponentsAsList<T>( gameObjects, includeChildren ).ToArray();
		}
		
		public static List<T> GetComponentsAsList<T>( this GameObject[] gameObjects, bool includeChildren = false ) where T : Component
		{
			var list = new List<T>();
			if( includeChildren )
			{
				for( int i = 0; i < gameObjects.Length; i++ )
				{
					list.AddRange(gameObjects[ i ].GetComponentsInChildren<T>( true ));
				}
			}
			else
			{
				for( int i = 0; i < gameObjects.Length; i++ )
				{
					list.AddRange(gameObjects[ i ].GetComponents<T>());
				}
			}
			
			return list;
		}
		
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

		public static Color SetR(this Color c, float r)
		{
			c.r = r;
			return c;
		}

		public static Color SetG(this Color c, float g)
		{
			c.g = g;
			return c;
		}

		public static Color SetB(this Color c, float b)
		{
			c.b = b;
			return c;
		}

		public static Color SetAlpha(this Color c, float newAlpha)
		{
			c.a = newAlpha;
			return c;
		}

		public static GameObject SetParent(this GameObject g, GameObject p){
			g.transform.SetParent (p.transform);
			return g;
		}

		public static GameObject SetParent(this GameObject g, Transform p){
			g.transform.SetParent (p);
			return g;
		}

		public static GameObject SetParent(this GameObject g, Component c){
			g.transform.SetParent (c.transform);
			return g;
		}

		public static Transform SetParent(this Transform g, GameObject p){
			g.SetParent (p.transform);
			return g;
		}

		public static Transform SetParent(this Transform g, Component c){
			g.SetParent (c.transform);
			return g;
		}


		public static Transform GetParent(this GameObject g)
		{
			return g.transform.parent;
		}

		public static object SendMessageBetter(this MonoBehaviour obj, string name, params object[] parameters) {
			Type[] types = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++) {
				types[i] = parameters[i].GetType();
			}
              
			MethodInfo mInfo = obj.GetType().GetMethod(name, types);
             
			if (mInfo != null) {
				return mInfo.Invoke(obj, parameters);
			}
			return null;
		}

        public static void SetUIActive(this GameObject g, bool active)
        {
            var components = g.GetComponentsInChildren<UnityEngine.UI.Graphic>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = active;
            }
        
        }
		
		#endregion
	}


	

	
}

