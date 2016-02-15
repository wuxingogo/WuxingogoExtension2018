using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;
using System.Reflection;
using wuxingogo.Reflection;
using System;


[CustomPropertyDrawer( typeof( DisableExpresionAttribute), true )]
public class DisableExpresionDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var disableAttr = base.attribute as DisableExpresionAttribute;

		object o = GetBaseProperty(property);
		if(!disableAttr.obj.Equals(o))
		{
			EditorGUI.BeginDisabledGroup( true );
			EditorGUI.PropertyField( position, property, label );
			EditorGUI.EndDisabledGroup();
		}else
		{
			EditorGUI.PropertyField( position, property, label );
		}
	}


	private static object GetBaseProperty(SerializedProperty prop)
	{
		// Separate the steps it takes to get to this property
		string[] separatedPaths = prop.propertyPath.Split('.');
		
		// Go down to the root of this serialized property
		System.Object reflectionTarget = prop.serializedObject.targetObject as object;
		// Walk down the path to get the target object
		foreach (var path in separatedPaths)
		{
			FieldInfo fieldInfo = reflectionTarget.GetType().GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			reflectionTarget = fieldInfo.GetValue(reflectionTarget);
		}
		return reflectionTarget;
	}
}
