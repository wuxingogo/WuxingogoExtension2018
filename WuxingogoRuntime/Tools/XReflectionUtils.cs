using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using wuxingogo.tools;


namespace wuxingogo.Reflection
{

	public static class XReflectionUtils
	{

		private const BindingFlags INSTANCE_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
		private const BindingFlags STATIC_FLAGS = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
		private const BindingFlags NOVIRTUAL_FLAGS = BindingFlags.DeclaredOnly;

		public static Assembly TryGetAssembly(string assemblyName, bool isPrecise = true)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();


			for( int pos = 0; pos < assemblies.Length; pos++ ) {
				//  TODO loop in assemblies.Length
				string currAssem = StringUtils.CutOnCharLeft( assemblies[pos].ManifestModule.Name, "." );
				if( isPrecise ) {
					if( currAssem.Equals( assemblyName ) )
						return assemblies[pos];
				} else {
					if( currAssem.Contains( assemblyName ) || assemblyName.Contains( currAssem ) )
						return assemblies[pos];
				}
			}
			Logger.LogError( string.Format( "Try Get Assembly {0} Not Found!", assemblyName ) );
			return null;
		}

		public static Assembly GetUnitySolotion()
		{
			return TryGetAssembly( "Assembly-CSharp" );
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
			Logger.LogError( "Try Get Class Not Found!" );
			return null;
		}

		/// <summary>
		/// Tries the invoke method.
		/// </summary>
		/// <returns>The invoke method.</returns>
		/// <param name="target">Target.</param>
		/// <param name="methodName">Method name.</param>
		/// <param name="methodParams">Method parameters.</param>
		public static object TryInvokeMethod(this Type type, object target, string methodName, params object[] methodParams)
		{
			return type.TryGetMethodInfo( methodName ).Invoke( target, methodParams );
		}

		public static object TryInvokeGlobalMethod(this Type target, string methodName, params object[] methodParams)
		{
			return target.TryGetGlobalMethodInfo( methodName ).Invoke( null, methodParams );
		}

		/// <summary>
		/// Tries the get field value.
		/// </summary>
		/// <returns>The get field value.</returns>
		/// <param name="target">Target.</param>
		/// <param name="fieldName">Field name.</param>
		public static object TryGetFieldValue(this object target, string fieldName)
		{
			return target.GetType().TryGetFieldInfo( fieldName ).GetValue( target );
		}

		public static object TryGetGlobalFieldValue(this Type target, string fieldName)
		{
			return target.TryGetGlobalFieldInfo( fieldName ).GetValue( null );
		}

		/// <summary>
		/// Tries the get property value.
		/// </summary>
		/// <returns>The get property value.</returns>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		public static object TryGetPropertyValue(this Type target, string propertyName)
		{
			return target.TryGetPropertyInfo( propertyName ).GetValue( target, null );
		}

		/// <summary>
		/// Tries the get field info.
		/// </summary>
		/// <returns>The get field info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="fieldName">Field name.</param>
		private static FieldInfo TryGetFieldInfo(this Type target, string fieldName)
		{
			return target.GetField( fieldName, DetectFlags(false, true) );
		}

		private static FieldInfo TryGetGlobalFieldInfo(this Type target, string fieldName)
		{
			return target.GetField( fieldName, STATIC_FLAGS );
		}

		/// <summary>
		/// Tries the get method info.
		/// </summary>
		/// <returns>The get method info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="methodName">Method name.</param>
		private static MethodInfo TryGetMethodInfo(this Type target, string methodName)
		{
			return target.GetMethod( methodName, INSTANCE_FLAGS );
		}

		private static MethodInfo TryGetGlobalMethodInfo(this Type target, string methodName)
		{
			return target.GetMethod( methodName, STATIC_FLAGS );
		}


		/// <summary>
		/// Tries the get property info.
		/// </summary>
		/// <returns>The get property info.</returns>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		private static PropertyInfo TryGetPropertyInfo(this Type target, string propertyName)
		{
			return target.GetProperty( propertyName, INSTANCE_FLAGS );
		}
		private static PropertyInfo TryGetGlobalPropertyInfo(this Type target, string propertyName)
		{
			return target.GetProperty( propertyName, STATIC_FLAGS );
		}

		private static EventInfo TryGetEventInfo(this Type target, string eventName)
		{
			return target.GetEvent( eventName, INSTANCE_FLAGS );
		}
		private static EventInfo TryGetGlobalEventInfo(this Type target, string eventName)
		{
			return target.GetEvent( eventName, STATIC_FLAGS );
		}

		public static object TrySearchGlobalMemberValue(this Type target, string memberName)
		{
			FieldInfo field = target.TryGetGlobalFieldInfo( memberName );
			if(field != null)
				return field.GetValue(null);
			PropertyInfo property = target.TryGetGlobalPropertyInfo( memberName );
			if( null != property )
				return property.GetValue(null, null);
			MethodInfo method = target.TryGetGlobalMethodInfo( memberName );
			if( null != method )
				return method.Invoke( null, null );
			EventInfo eventInfo = target.TryGetGlobalEventInfo( memberName );
			if( null != eventInfo )
				return eventInfo;
			Logger.LogError( string.Format( "Search Member Not Found \"{0}\".", memberName ) );
			return null;
		}

		public static Type TrySearchMember(this Type target, string memberName)
		{
			FieldInfo field = target.TryGetFieldInfo( memberName );
			if(field != null)
				return field.FieldType;
			PropertyInfo property = target.TryGetPropertyInfo( memberName );
			if( null != property )
				return property.PropertyType;
			MethodInfo method = target.TryGetMethodInfo( memberName );
			if( null != method )
				return method.ReturnType;
			EventInfo eventInfo = target.TryGetEventInfo( memberName );
			if( null != eventInfo )
				return eventInfo.EventHandlerType;
			Logger.LogError( string.Format( "Search Member Not Found \"{0}\".", memberName ) );
			return null;
		}

		public static FieldInfo[] FieldMatch(this Type target, string fieldName, bool isStatic, bool isVirtual = true)
		{
			FieldInfo[] fieldCollection = target.GetFields( DetectFlags( isStatic, isVirtual ) );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		public static PropertyInfo[] PropertyMatch(this Type target, string fieldName, bool isStatic, bool isVirtual = true)
		{
			PropertyInfo[] fieldCollection = target.GetProperties( DetectFlags( isStatic, isVirtual ) );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		public static EventInfo[] EventMatch(this Type target, string fieldName, bool isStatic, bool isVirtual = true)
		{
			EventInfo[] fieldCollection = target.GetEvents( DetectFlags( isStatic, isVirtual ) );
			return fieldCollection.Where( field => 
				field.Name.Contains( fieldName )
			).ToArray();
		}

		public static MethodInfo[] MethodMatch(this Type target, string fieldName, bool isStatic, bool isVirtual = true)
		{
			MethodInfo[] fieldCollection = target.GetMethods( DetectFlags( isStatic, isVirtual ) );
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

		private static BindingFlags DetectFlags(bool isStatic, bool isVirtual)
		{
			return isStatic ? STATIC_FLAGS : isVirtual ? INSTANCE_FLAGS : BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | NOVIRTUAL_FLAGS;
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
		public static void TrySetField(this Type target, string fieldName, object value)
		{
			target.TryGetFieldInfo( fieldName ).TrySetFieldValue( target, value );
		}

		/// <summary>
		/// Tries the set property.
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="propertyName">Property name.</param>
		/// <param name="value">Value.</param>
		public static void TrySetProperty(this Type type, object target, string propertyName, object value)
		{
			type.TryGetPropertyInfo( propertyName ).TrySetPropertyValue( target, value );
		}

		public static IEnumerable<Type> FindSubClass(Type type)
		{
			return type.Assembly.GetTypes().Where( sub => sub.IsSubclassOf( type ) );
		}

		public static bool isSubClassOrEquals<T>(this Type type)
		{
			return type.IsSubclassOf( typeof( T ) ) || type == typeof( T );
		}


	}
}