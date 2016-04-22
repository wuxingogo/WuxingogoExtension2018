using UnityEditor;
using UnityEngine;
using System;



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
namespace wuxingogoEditor
{
    [CustomPropertyDrawer(typeof(IDictionary), true)]
    public class UnityDictionaryDrawer : PropertyDrawer
    {

        override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, new GUIContent(label.text + "IDictionary"), property);

            //todo: put nice drawing code here

            EditorGUI.EndProperty();
        }
    }
}