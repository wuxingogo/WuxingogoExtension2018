using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;


public class XPropertyWindow : XBaseWindow
{
	Object uObject = null;
	
	private object _target = null;

	public object Target {
		get {
			return _target;
		}
		set {
			if( _target != value ) {
				
				_target = value;
				OnChangeTarget();
			}
		}
	}

	public override object[] closeRecordArgs {
		get {
			return new object[]{ Target };
		}
	}

	public override void OnInitialization(params object[] args)
	{
		if( args.Length > 0 )
			Target = args[0];
	}

	public override void OnXGUI()
	{
			
		if( CreateSpaceButton( "Clean" ) ) {
			uObject = null;
			Target = null;
		}
		
		if( null == Target ) {
			uObject = CreateObjectField( "", uObject );
			Target = uObject;
		} else if( Target != null ) {
		
		
			PropertyInfo[] properties = Target.GetType().GetProperties();
			foreach( PropertyInfo property in properties ) {
				if( Target.GetType() == property.DeclaringType ) {
//					CreateStringField("method name " , field.Name);
					
					EditorGUILayout.BeginVertical();
					{
						
						if( property.Name.Equals( "rigidbody" ) ) {
							return;
						}
						object value = property.GetValue( Target, null );
						object changeValue = null;


						if( property.PropertyType == typeof( System.Int32 ) ) {
							changeValue = CreateIntField( property.Name + ": int", (int)value );
						} else if( property.PropertyType == typeof( System.Int64 ) ) {
//							CreateIntField(field.Name + ": int" , (int)value);
						} else if( property.PropertyType == typeof( System.Byte ) ) {
							changeValue = CreateIntField( property.Name + ": byte", ((int)(byte)(value)) );
						} else if( property.PropertyType == typeof( System.Single ) ) {
							changeValue = CreateFloatField( property.Name + ": float", (float)value );
						} else if( property.PropertyType.BaseType == typeof( System.Array ) ) {
//							for( int pos = 0; pos < System.Array.) {
//								
//							}
							Object[] array = value as Object[];
							if( null != array ) {
								for( int i = 0; i < array.Length; i++ ) {
									array[i] = CreateObjectField( property.Name + "[" + i + "]", array[i] );
								}
								changeValue = array;
							} else
								changeValue = value;
//							changeValue = CreateStringField(field.Name + ": array" , value.ToString());
						} else if( property.PropertyType == typeof( System.Boolean ) ) {
							changeValue = CreateCheckBox( property.Name + ": bool", (bool)value );
						} else if( property.PropertyType == typeof( System.String ) ) {
							changeValue = CreateStringField( property.Name + ": string", (string)value );
						} else if( property.PropertyType.BaseType == typeof( System.Enum ) ) {
							changeValue = CreateEnumSelectable( property.Name + ": string", (System.Enum)value );
						}
//						else if(field.FieldType.BaseType == ))
////							Debug.Log(field.FieldType.ToString());
////							CreateObjectField(field.Name + ": object", (Object)value);
//						}
						else if( property.PropertyType.BaseType == typeof( UnityEngine.Object ) ) {
							
							changeValue = CreateObjectField( property.Name + ": " + property.PropertyType, (UnityEngine.Object)value );
						} else if( property.PropertyType.BaseType == typeof( UnityEngine.Behaviour ) ) {
							
							changeValue = CreateObjectField( property.Name + ": " + property.PropertyType, (UnityEngine.Behaviour)value );
						} else if( property.PropertyType.BaseType == typeof( UnityEngine.MonoBehaviour ) ) {
							
							changeValue = CreateObjectField( property.Name + ": " + property.PropertyType, (UnityEngine.Behaviour)value );
						} else if( property.PropertyType.BaseType == typeof( System.Object ) ) {
							
							changeValue = value;
//							CreateObjectField(field.Name + ": " + field.FieldType, (UnityEngine.Behaviour)value);
						} else if( property.PropertyType.BaseType == typeof( System.ValueType ) ) {
							changeValue = value;
						} else {
//							changeValue = CreateObjectField(property.Name + ": " + property.PropertyType, (Object)value);
						}
						if( GUI.changed && changeValue != value && uObject != null ) {
//							Debug.Log("gui change");
							Undo.RecordObject( uObject, "Record ScrObject" );
							try {
								property.SetValue( Target, changeValue, null );
							} catch( System.ArgumentException e ) {
								
							} catch( System.FieldAccessException e ) {
								
							}
							
							Repaint();
						}
					}
					EditorGUILayout.EndVertical();
					
				}
			}
		}
		
	}

	System.Type showType;

	string targetType;

	private Stack<object> storeTargets = new Stack<object>();

	public void OnChangeTarget()
	{
		
		if( Target != null ) {
			storeTargets.Push( Target );
			
			targetType = Target.GetType().ToString();
			
			showType = Target.GetType();
		}
		
		
		this.Repaint();
	}
}