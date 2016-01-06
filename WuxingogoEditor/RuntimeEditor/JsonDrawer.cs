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

[CustomPropertyDrawer(typeof(JSONObject), true)]
public class JsonDrawer : PropertyDrawer {
	
	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		
		//todo: put nice drawing code here
		
		EditorGUI.EndProperty ();
	}
}
