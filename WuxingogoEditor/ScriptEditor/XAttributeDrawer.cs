//
//  XAttributeDrawer.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using wuxingogo.Runtime;
using UnityEngine;

[CustomPropertyDrawer(typeof(XAttribute), true)]
public class XAttributeDrawer : PropertyDrawer {
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		XAttribute xTarget = (XAttribute)base.attribute;
		EditorGUILayout.TextField(xTarget.title);

		if (property.propertyType == SerializedPropertyType.Float)
		{
			EditorGUI.FloatField(position, property.floatValue);
		}
		else if (property.propertyType == SerializedPropertyType.Integer)
		{
			EditorGUI.FloatField(position, property.intValue);
		}
		else if(property.propertyType == SerializedPropertyType.ObjectReference)
		{
			EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(Object));
		}
	}
}
