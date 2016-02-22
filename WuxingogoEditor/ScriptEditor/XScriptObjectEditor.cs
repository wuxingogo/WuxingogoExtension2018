using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;
using System.Reflection;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;

[CustomEditor( typeof( XScriptableObject ), true )]
public class XScriptObjectEditor : XMonoBehaviourEditor {

//	private Dictionary<string, object[]> methodParameters = new Dictionary<string, object[]>();
//
//	public override void OnXGUI()
//	{
//		foreach( var info in target.GetType().GetMethods() ){
//			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
//                CreateSpaceBox();
//                CreateLabel( "XMethod : " + info.Name );
//                ParameterInfo[] paras = info.GetParameters();
//                if( !methodParameters.ContainsKey( info.Name ) )
//                {
//                    object[] o = new object[paras.Length];
//                    methodParameters.Add(info.Name, o);
//                }
//                object[] objects = methodParameters[info.Name];
//                for( int pos = 0; pos < paras.Length; pos++ )
//                {
//					BeginHorizontal();
//					CreateLabel(paras[pos].ParameterType.Name);
//					objects[pos] = GetTypeGUI( objects[pos], paras[pos].ParameterType );
//					EndHorizontal();
//                }   
//				if(CreateSpaceButton(info.Name +"  "+ (att as XAttribute).title)){
//                    info.Invoke( target, objects );
//				}
//                CreateSpaceBox();
//			}
//		}
//
//
//
//		foreach( var info in target.GetType().GetFields() ) {
//			foreach( var att in info.GetCustomAttributes(typeof(XAttribute),true) ) {
//				CreateSpaceBox();
//				CreateLabel( "XField : " + info.Name + " || " + info.GetValue( target ).ToString() );
//
//				if( typeof( IDictionary ).IsAssignableFrom( info.FieldType ) ) {
//					SortedDictionary<int,int> sd;
//					IDictionary dictionary = (IDictionary)info.GetValue( target );
//					IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
//					IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
//					ICollection collection = dictionary.Values;
//					while ( iteratorKey.MoveNext() && iteratorValue.MoveNext() ) {
//
//						var oldValue = GetTypeGUI( dictionary[iteratorKey.Current], dictionary[iteratorKey.Current].GetType() );
//						if( oldValue != dictionary[iteratorKey.Current] )
//							dictionary[iteratorKey.Current] = oldValue;
//					}
//
//				}
//
//				if( typeof( IList ).IsAssignableFrom( info.FieldType ) ) {
//
//					IList collection = (IList)info.GetValue( target );
//
//					IEnumerator iteratorValue = collection.GetEnumerator();
//					int index = 0;
//					while ( iteratorValue.MoveNext() ) {
//						if(collection[index] != null)
//							collection[index] = GetTypeGUI( collection[index], collection[index].GetType() );
//						index++;
//					}
//				}
//			}
//		}
//
//
//		foreach( var info in target.GetType().GetProperties() ){
//			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
//                CreateSpaceBox();
//
//				object result = info.GetValue(target, null);
//
//				BeginHorizontal();
//
//				CreateLabel( "XProperty : " + info.Name + " || ");
//
//				EditorGUI.BeginDisabledGroup( !info.CanWrite );
//				var newValue = GetTypeGUI( result, info.PropertyType );
//				EditorGUI.EndDisabledGroup();
//
//				if( null != newValue && !newValue.Equals(result)  )
//					info.SetValue( target, newValue, null);
//
//				EndHorizontal();
//			}
//		}
//	
//	}
//
//	private void OpenInMethod(object target){
//		XReflectionWindow method = XBaseWindow.InitWindow<XReflectionWindow>();
//		method.Target = target;
//	}
//
//	object GetTypeGUI(object t, Type type)
//	{
//		if( type == typeof( Int32 ) ) {
//			t = CreateIntField( Convert.ToInt32( t ) );
//		} else if( type == typeof( String ) ) {
//			t = CreateStringField( (string)t );
//		} else if( type == typeof( Single ) ) {
//			t = CreateFloatField( Convert.ToSingle( t ) );
//		} else if( type == typeof( Boolean ) ) {
//			t = CreateCheckBox( Convert.ToBoolean( t ) );
//		} else if( type.BaseType == typeof( Enum ) ) {
//			t = CreateEnumSelectable("", (Enum)t ?? (Enum)Enum.ToObject( type, 0 ));
//		} else if( type.IsSubclassOf( typeof( Object ) ) ) {
//			t = CreateObjectField( (Object)t, type );
//		} else {
//			CreateLabel(type.Name + " is not support" );
//		}
//		return t;
//			
//	}
}
