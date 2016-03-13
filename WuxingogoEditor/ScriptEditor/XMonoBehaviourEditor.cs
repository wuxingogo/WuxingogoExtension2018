using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using wuxingogo.Runtime;
using System;
using Object = UnityEngine.Object;
using System.Linq;

[CustomEditor( typeof( XMonoBehaviour ), true )]
public class XMonoBehaviourEditor : XBaseEditor
{
	private Dictionary<string, object[]> methodParameters = new Dictionary<string, object[]>();
	BindingFlags bindFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
	public override void OnXGUI()
	{
		
		GetTargetMethod<XAttribute>( target );
		GetTargetField<XAttribute>( target );
		GetTargetProperty<XAttribute>( target );


	}

	void GetTargetMethod<T>(object target)
	{
		if( target == null )
			return;
		foreach( var info in target.GetType().GetMethods(bindFlags) ) {
			foreach( var att in info.GetCustomAttributes(typeof(T),true) ) {
				BeginVertical();

				CreateSpaceBox();
				CreateLabel( "XMethod : " + info.Name );
				ParameterInfo[] paras = info.GetParameters();
				if( !methodParameters.ContainsKey( info.Name ) ) {
					object[] o = new object[paras.Length];
					methodParameters.Add( info.Name, o );
				}
				object[] objects = methodParameters[info.Name];
				for( int pos = 0; pos < paras.Length; pos++ ) {
					BeginHorizontal();
					CreateLabel( paras[pos].ParameterType.Name );
					objects[pos] = GetTypeGUI( objects[pos], paras[pos].ParameterType );
					EndHorizontal();
				}   
				if( CreateSpaceButton( info.Name ) ){
					info.Invoke( target, objects );
				}
				CreateSpaceBox();

				EndVertical();
			}
		}
	}

	void GetTargetField<T>(object target)
	{
		if( target == null )
			return;
		foreach( var info in target.GetType().GetFields(bindFlags) ) {
			foreach( var att in info.GetCustomAttributes(typeof(T), true) ) {
				BeginVertical();

				CreateSpaceBox();
				CreateLabel( "XField : " + info.Name + " || " + info.GetValue( target ).ToString() );

				if( typeof( IDictionary ).IsAssignableFrom( info.FieldType ) ) {
					SortedDictionary<int,int> sd;
					IDictionary dictionary = (IDictionary)info.GetValue( target );
					IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
					IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
					ICollection collection = dictionary.Values;


					while ( iteratorKey.MoveNext() && iteratorValue.MoveNext() ) {
						BeginHorizontal();
						GetTypeGUI( iteratorKey.Current, iteratorKey.Current.GetType() );
						var oldValue = GetTypeGUI( dictionary[iteratorKey.Current], dictionary[iteratorKey.Current].GetType() );
						EndHorizontal();
//						if( oldValue != dictionary[iteratorKey.Current] )
//							dictionary[iteratorKey.Current] = oldValue;
					}

				}

				if( typeof( IList ).IsAssignableFrom( info.FieldType ) ) {

					IList collection = (IList)info.GetValue( target );

					IEnumerator iteratorValue = collection.GetEnumerator();
					int index = 0;
					while ( iteratorValue.MoveNext() ) {
						if( collection[index] != null )
							collection[index] = GetTypeGUI( collection[index], collection[index].GetType() );
						index++;
					}
				}
				EndVertical();
			}
		}

	}

	void GetTargetProperty<T>(object target)
	{
		if( target == null )
			return;
		foreach( var info in target.GetType().GetProperties(bindFlags) ) {
			foreach( var att in info.GetCustomAttributes(typeof(XAttribute),true) ) {
				BeginVertical();

				CreateSpaceBox();

				object result = info.GetValue( target, null );

				CreateLabel( "XProperty : " + info.Name + " || " );
		
				EditorGUI.BeginDisabledGroup( !info.CanWrite );
				var newValue = GetTypeGUI( result, info.PropertyType );
				EditorGUI.EndDisabledGroup();

				if( null != newValue && !newValue.Equals( result ) )
					info.SetValue( target, newValue, null );
				

				EndVertical();
			}
		}
	}

	object GetTypeGUI(object t, Type type)
	{
		if( t is int || t is System.Int32 || type == typeof( int ) ) {
			t = CreateIntField( Convert.ToInt32( t ) );
		} else if( t is System.Int16 ) {
			t = (short)CreateIntField( Convert.ToInt16( t ) );
		} else if( t is System.Int64 ) {
			t = CreateLongField( Convert.ToInt64( t ) );
		} else if( t is byte ) {
			int value = Convert.ToInt32( t );
			t = Convert.ToByte( CreateIntField( value ) );
		} else if( type == typeof( String ) ) {
			t = CreateStringField( (string)t );
		} else if( type == typeof( Single ) ) {
			t = CreateFloatField( Convert.ToSingle( t ) );
		} else if( type == typeof( Boolean ) ) {
			t = CreateCheckBox( Convert.ToBoolean( t ) );
		} else if( type.BaseType == typeof( Enum ) ) {
			t = CreateEnumSelectable( "", (Enum)t ?? (Enum)Enum.ToObject( type, 0 ) );
		} else if( type.IsSubclassOf( typeof( Object ) ) ) {
			t = CreateObjectField( (Object)t, type );
		} else if( typeof( IList ).IsAssignableFrom( type ) ) {
			IList list = t as IList;
			if( list == null )
				return t;
			BeginVertical();
			for( int pos = 0; pos < list.Count; pos++ ) {
				//  TODO loop in list.Count
				var o = list[pos];
				GetTypeGUI( o, o.GetType() );
			}
			EndVertical();
		} else {
			
			CreateLabel( type.Name + " is not support" );


			GetTargetMethod<XAttribute>(t);
			GetTargetField<XAttribute>(t);
			GetTargetProperty<XAttribute>(t);

		}

		return t;
			
	}




	private void OpenInMethod(object target)
	{
		XReflectionWindow method = XBaseWindow.InitWindow<XReflectionWindow>();
		method.Target = target;
	}
}