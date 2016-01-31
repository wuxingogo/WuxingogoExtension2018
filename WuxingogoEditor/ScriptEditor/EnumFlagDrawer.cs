//
//  EnumFlagDrawer.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.


namespace WuxingogoEditor
{
	using System;
	using System.Reflection;
	using UnityEditor;
	using UnityEngine;
	using wuxingogo.Runtime;


	[CustomPropertyDrawer( typeof( EnumFlagAttribute ) )]
	public class EnumFlagDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
			Enum targetEnum = (System.Enum)System.Enum.ToObject( fieldInfo.FieldType, property.intValue );
			string propName = flagSettings.enumName;
			if( string.IsNullOrEmpty( propName ) )
				propName = property.name;

			EditorGUI.BeginProperty( position, label, property );
			Enum newEnum = EditorGUI.EnumMaskField( position, propName, targetEnum );

			int old = (int)Convert.ChangeType( targetEnum, targetEnum.GetType() );
			int newE = (int)Convert.ChangeType( newEnum, targetEnum.GetType() );
			if(old != newE){
				property.intValue = newE;
			}

			EditorGUI.EndProperty();

		}
	}
}

