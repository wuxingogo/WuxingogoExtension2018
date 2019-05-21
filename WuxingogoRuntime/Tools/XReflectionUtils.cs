//
// XReflectionUtils.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2015 ly-user
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using wuxingogo.tools;
using wuxingogo.Runtime;


namespace wuxingogo.Reflection
{

	public static class XReflectionUtils
	{

		public const BindingFlags DECLARED_ONLY = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
		public const BindingFlags INSTANCE_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
		public const BindingFlags STATIC_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;
		public const BindingFlags NOVIRTUAL_FLAGS = BindingFlags.DeclaredOnly;

		public static Assembly TryGetAssembly(string assemblyName, bool isPrecise = true)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();


			for( int pos = 0; pos < assemblies.Length; pos++ ) {
				//  TODO loop in assemblies.Length
				string currAssem = StringUtils.Substring( assemblies[pos].ManifestModule.Name, "." );
				if( isPrecise ) {
					if( currAssem.Equals( assemblyName ) )
						return assemblies[pos];
				} else {
					if( currAssem.Contains( assemblyName ) || assemblyName.Contains( currAssem ) )
						return assemblies[pos];
				}
			}
			XLogger.LogError( string.Format( "Try Get Assembly {0} Not Found!", assemblyName ) );
			return null;
		}

		public static Assembly GetUnitySolotion()
		{
			return TryGetAssembly( "Assembly-CSharp" );
		}

		static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;

		public static Type GetType(string assemblyName, string className)
		{
			return Assembly.Load(assemblyName).GetType(className);
		}

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
			XLogger.LogError( "Try Get Class Not Found!" );
			return null;
		}

        public static Assembly GetEditorAssembly()
        {
            return AppDomain.CurrentDomain.Load( "WuxingogoEditor" );
        }

        public static Type GetEditorType(string typeName)
        {
            return GetEditorAssembly().GetType( typeName );
        }
		// Need add namespace
		public static Type GetUnityEditor(string typeName){
			return AppDomain.CurrentDomain.Load( "UnityEditor" ).GetType(typeName);
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
			XLogger.Log (string.Format("target is null{0}", target == null));
			var methodInfo = target.GetMethod( methodName, STATIC_FLAGS );
			XLogger.Log (string.Format("method is null, {0}", methodInfo == null));
			return methodInfo;
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
			XLogger.LogError( string.Format( "Search Member Not Found \"{0}\".", memberName ) );
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
			XLogger.LogError( string.Format( "Search Member Not Found \"{0}\".", memberName ) );
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

        public static IEnumerable<Type> FindUnitySubClass( Type type )
        {
            return GetUnitySolotion().GetTypes().Where( sub => sub.IsSubclassOf( type ) );
        }

        public static List<Type> FindAllSubClass(Type type)
        {
	        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
	        List<Type> totalTypes = new List<Type>();
	        for (int i = 0; i < assemblies.Length; i++)
	        {
		        var types = assemblies[i].GetTypes().Where(sub => sub.IsSubclassOf(type));
		        totalTypes.AddRange(types);
	        }

	        return totalTypes;
        }

		public static bool isSubClassOrEquals<T>(this Type type)
		{
			return type.IsSubclassOf( typeof( T ) ) || type == typeof( T );
		}

        /// <summary>
        /// create object from System.Type
        /// </summary>
        /// <param name="t" the object 's type></param>
        /// <returns>object</returns>
        public static object CreateInstance( Type t )
        {
            if( t.IsGenericType && t.GetGenericTypeDefinition() == typeof( Nullable<> ) )
            {
                t = Nullable.GetUnderlyingType( t );
            }
            return Activator.CreateInstance( t, true );
        }

		public static UnityEngine.Object GetPrefabObject(UnityEngine.Object asset)
		{
			
			System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.AssetsUtilites" );
			if (type != null) {
				var method = type.GetMethod( "GetPrefabObject" );
				var result = (UnityEngine.Object)method.Invoke( null, new object[]{asset});
				return result;
			}
			return null;
		}
		public static string GetPrefabType(UnityEngine.Object asset)
		{

			System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.AssetsUtilites" );
			if (type != null) {
				var method = type.GetMethod( "GetPrefabType" );
				var result = (string)method.Invoke( null, new object[]{asset});
				return result;
			}
			return "";
		}
		// editor only
		public static void AddObjectToObject(UnityEngine.Object asset, UnityEngine.Object parent)
		{
			if (!Application.isPlaying )
			{
				
				System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.AssetsUtilites" );
				var method = type.GetMethod( "AddObjectToAsset" );
				method.Invoke( null, new object[] { asset, parent } );

			}
		}

		public static void SetDirty(UnityEngine.Object asset)
		{
			System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.AssetsUtilites" );
			var method = type.GetMethod( "SetDirty" );
			method.Invoke( null, new object[] { asset } );
		}

		public static void Save(XScriptableObject asset)
		{
			if( !Application.isPlaying )
			{

				System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.XScriptObjectEditor" );
				var method = type.GetMethod( "Save" );
				method.Invoke( null, new object[] { asset } );

			}
		}
		
		public static Component CopyComponent(Component original, GameObject destination) 
		{ 
			Type type = original.GetType(); 
			Component copy = destination.AddComponent(type); 
			FieldInfo[] fields = type.GetFields(); 
			PropertyInfo[] properties = type.GetProperties(DECLARED_ONLY);  
			foreach (FieldInfo field in fields) 
			{ 
				field.SetValue(copy, field.GetValue(original)); 
			} 
			foreach (PropertyInfo propertyInfo in properties) 
			{
				if( propertyInfo.CanRead )
				{

					if( propertyInfo.CanWrite )
					{
						try
						{
						
							var propertyValue = propertyInfo.GetValue( original, null );
							
							propertyInfo.SetValue(copy, propertyValue, null); 
							
						}
						catch( Exception e )
						{
							XLogger.Log( e );
							throw;
						}
					}
				}
				
			}  
			return copy; 
		} 
     
		public static T CopyComponent<T>(T original, GameObject destination, BindingFlags bindingFlags = DECLARED_ONLY) where T : Component 
		{ 
			Type type = original.GetType(); 
			Component copy = destination.AddComponent(type); 
			FieldInfo[] fields = type.GetFields(); 
			PropertyInfo[] properties = type.GetProperties(bindingFlags);  
			foreach (FieldInfo field in fields) 
			{ 
				field.SetValue(copy, field.GetValue(original)); 
			} 
			foreach (PropertyInfo propertyInfo in properties) 
			{
				if( propertyInfo.CanRead )
				{

					if( propertyInfo.CanWrite )
					{
						try
						{
						
							var propertyValue = propertyInfo.GetValue( original, null );
							
							propertyInfo.SetValue(copy, propertyValue, null); 
							
						}
						catch( Exception e )
						{
							XLogger.Log( e );
							throw;
						}
					}
				}
				
			} 
			return copy as T; 
		} 
    }
}