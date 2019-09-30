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
				//OnChangeTarget();
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
                        var type = property.DeclaringType;


                        object changeValue = GetValue(type, type.Name, value);

                        if ( GUI.changed && changeValue != value && uObject != null ) {
							Undo.RecordObject( uObject, "Record ScrObject" );
							try
							{
								property.SetValue(Target, changeValue, null);
							}
							catch
							{
							}
							
							Repaint();
						}
					}
					EditorGUILayout.EndVertical();
					
				}
			}
		}
		
	}

}