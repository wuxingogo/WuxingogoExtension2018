using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;


namespace wuxingogo.Reflection
{

	public static class XReflectionUtils
	{

		private static BindingFlags INSTANCE_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;
		private static BindingFlags STATIC_FLAGS = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

		public static Assembly TryGetAssembly(string assemblyName, bool isPrecise = true)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for( int pos = 0; pos < assemblies.Length; pos++ ) {
				//  TODO loop in assemblies.Length
				if( isPrecise ) {
					if( assemblies[pos].FullName.Equals( assemblyName ) )
						return assemblies[pos];
				} else {
					if( assemblies[pos].FullName.Contains( assemblyName ) || assemblyName.Contains( assemblies[pos].FullName ) )
						return assemblies[pos];
				}
			}
			Debug.LogError( "Try Get Assembly Not Found!" );
			return null;
		}

		static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;

		/// <summary>
		/// Tries the get class.
		/// </summary>
		/// <returns>The get class.</returns>
		/// <param name="className">Class name.</param>
		public static Type TryGetClass(string className)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for( int pos = 0; pos < assemblies.Length; pos++ ) {
				//  TODO loop in Length
				var types = assemblies[pos].GetTypes();
				for( int idx = 0; idx < types.Length; idx++ ) {
					//  TODO loop in types.Length
					if( types[idx].Name.Equals( className, ignoreCase ) || types[idx].Name.Contains( '+' + className ) ) //+ check for inline classes
					return types[idx];
				}
			}
			Debug.LogError( "Try Get Class Not Found!" );
			return null;
		}

		/// <summary>
		/// Tries the invoke method.
		/// </summary>
		/// <returns>The invoke method.</returns>
		/// <param name="target">Target.</param>
		/// <param name="methodName">Method name.</param>
		/// <param name="methodParams">Method parameters.</param>
		public static object TryInvokeMethod(this object target, string methodName, params object[] methodParams)
		{
			return target.TryGetMethodInfo( methodName ).Invoke( target, methodParams );
		}

		public static object TryInvokeGlobalMethod(this Type target, string methodName, params object[] methodParams)
		{
			return target.TryGetGlobalInfo( methodName ).Invoke( null, methodParams );
		}

		/// <summary>
		/// Tries the get field value.
		/// </summary>
		/// <returns>The get field value.</returns>
		/// <param name="target">Target.</param>
		/// <param name="fieldName">Field name.</param>
		public static object TryGetFieldValue(this object target, string fieldName)
		{
			return target.TryGetFieldInfo( fieldName ).GetValue( target );
		}

		/// <summary>
		/// Tries the get property value.
		/// </summary>
		/// <returns>The get property value.</returns>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		public static object TryGetPropertyValue(this object target, string propertyName)
		{
			return target.TryGetPropertyInfo( propertyName ).GetValue( target, null );
		}

		/// <summary>
		/// Tries the get field info.
		/// </summary>
		/// <returns>The get field info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="fieldName">Field name.</param>
		private static FieldInfo TryGetFieldInfo(this object target, string fieldName)
		{
			return target.GetType().GetField( fieldName, INSTANCE_FLAGS );
		}

		/// <summary>
		/// Tries the get method info.
		/// </summary>
		/// <returns>The get method info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="methodName">Method name.</param>
		private static MethodInfo TryGetMethodInfo(this object target, string methodName)
		{
			return target.GetType().GetMethod( methodName, INSTANCE_FLAGS );
		}

		private static MethodInfo TryGetGlobalInfo(this Type target, string methodName)
		{
			return target.GetMethod( methodName, STATIC_FLAGS );
		}


		/// <summary>
		/// Tries the get property info.
		/// </summary>
		/// <returns>The get property info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		private static PropertyInfo TryGetPropertyInfo(this object target, string propertyName)
		{
			return target.GetType().GetProperty( propertyName, INSTANCE_FLAGS );
		}

		private static EventInfo TryGetEventInfo(this object target, string eventName)
		{
			return target.GetType().GetEvent( eventName, INSTANCE_FLAGS );
		}

		public static MemberInfo TrySearchMember(this object target, string memberName)
		{
			MemberInfo info = null;
			info = target.TryGetFieldInfo( memberName );
			if( null == info )
				target.TryGetPropertyInfo( memberName );
			if( null == info )
				target.TryGetMethodInfo( memberName );
			if( null == info )
				target.TryGetEventInfo( memberName );
			Debug.LogError( string.Format( "Search Member Not Found \"{0}\".", memberName ) );
			return info;
		}

		private static FieldInfo[] FieldMatch(this Type target, string fieldName, bool isStatic)
		{
			FieldInfo[] fieldCollection = target.GetFields( isStatic ? STATIC_FLAGS : INSTANCE_FLAGS );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		private static PropertyInfo[] PropertyMatch(this Type target, string fieldName, bool isStatic)
		{
			PropertyInfo[] fieldCollection = target.GetProperties( isStatic ? STATIC_FLAGS : INSTANCE_FLAGS );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		private static EventInfo[] EventMatch(this Type target, string fieldName, bool isStatic)
		{
			EventInfo[] fieldCollection = target.GetEvents( isStatic ? STATIC_FLAGS : INSTANCE_FLAGS );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		private static MethodInfo[] MethodMatch(this Type target, string fieldName, bool isStatic)
		{
			MethodInfo[] fieldCollection = target.GetMethods( isStatic ? STATIC_FLAGS : INSTANCE_FLAGS );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		public static MemberInfo[] MemberMatch(this Type target, string memberName, bool isStatic = false)
		{
			List<MemberInfo> memberCollection = new List<MemberInfo>();

			memberCollection.AddRange( target.FieldMatch( memberName, isStatic ) );
			memberCollection.AddRange( target.PropertyMatch( memberName, isStatic ) );
			memberCollection.AddRange( target.EventMatch( memberName, isStatic ) );
			memberCollection.AddRange( target.MethodMatch( memberName, isStatic ) );
			
			return memberCollection.ToArray();
		}

		/// <summary>
		/// Tries the set field value.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="target">Target.</param>
		/// <param name="value">Value.</param>
		private static void TrySetFieldValue(this FieldInfo info, object target, object value)
		{
			info.SetValue( target, value );
		}

		/// <summary>
		/// Tries the set property value.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="target">Target.</param>
		/// <param name="value">Value.</param>
		private static void TrySetPropertyValue(this PropertyInfo info, object target, object value)
		{
			info.SetValue( target, value, null );
		}

		/// <summary>
		/// Tries the set field.
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="fieldName">Field name.</param>
		/// <param name="value">Value.</param>
		public static void TrySetField(this object target, string fieldName, object value)
		{
			target.TryGetFieldInfo( fieldName ).TrySetFieldValue( target, value );
		}

		/// <summary>
		/// Tries the set property.
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		/// <param name="value">Value.</param>
		public static void TrySetProperty(this object target, string propertyName, object value)
		{
			target.TryGetPropertyInfo( propertyName ).TrySetPropertyValue( target, value );
		}


	}
}