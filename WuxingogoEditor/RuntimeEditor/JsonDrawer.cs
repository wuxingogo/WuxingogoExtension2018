//
//  JsonDrawer.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;


using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(JSONObject), true)]
//public class JsonDrawer : PropertyDrawer {
//	
//	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//	{
//		base.OnGUI( position, property, label );
////		EditorGUI.BeginProperty (position, label, property);
//		
//		//todo: put nice drawing code here
////		JSONObject json = property.serializedObject.targetObject as JSONObject;
////		switch( json.type ) {
////			case JSONObject.Type.NUMBER:
////				json.n = EditorGUILayout.DoubleField( json.n );
////			break;
////			case JSONObject.Type.BOOL:
////				json.b = EditorGUILayout.Toggle( json.b );
////			break;
////			case JSONObject.Type.STRING:
////				json.str = EditorGUILayout.TextField( json.str );
////			break;
////			default:
////			break;
////		}
//
////		EditorGUI.EndProperty ();
//	}
//}

//[CustomPropertyDrawer(typeof(JSONObject))]
//internal sealed class JsonDraw : DecoratorDrawer
//{
//	public override float GetHeight()
//	{
//		return (base.attribute as SpaceAttribute).height;
//	}
//}