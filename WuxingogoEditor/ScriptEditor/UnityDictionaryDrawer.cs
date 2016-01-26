using UnityEditor;
using UnityEngine;
using System;
using Mono.CSharp;



//[CustomPropertyDrawer(typeof(UnityDictionary<object, object>))]
//public class UnityDictionaryDrawer : PropertyDrawer {
//	
//	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//	{
//		EditorGUI.BeginProperty (position, label, property);
//		
//		//todo: put nice drawing code here
//		
//		EditorGUI.EndProperty ();
//	}
//}
using System.Collections.Generic;
using System.Collections;

[CustomPropertyDrawer(typeof(IDictionary), true)]
public class UnityDictionaryDrawer : PropertyDrawer {
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		
		//todo: put nice drawing code here
		
		EditorGUI.EndProperty ();
	}
}