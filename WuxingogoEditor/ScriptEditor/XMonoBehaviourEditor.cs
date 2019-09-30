//
// XMonoBehaviourEditor.cs
//
// Author:
//       ly-user <52111314ly@gmail.com>
//
// Copyright (c) 2015 ly-user
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace wuxingogo.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using wuxingogo.Reflection;
    using wuxingogo.Runtime;
	using wuxingogo.tools;
    using Object = UnityEngine.Object;

    [CustomEditor( typeof( XMonoBehaviour ), true )]
    [CanEditMultipleObjects]
    public class XMonoBehaviourEditor : XBaseEditor
    {
		private static Dictionary<MethodInfo, object[]> methodParameters = new Dictionary<MethodInfo, object[]>();
        private Dictionary<object, bool> groupDict = new Dictionary<object, bool>();
		public static BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
		public static BindingFlags staticFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

		private static Type XType = typeof(XAttribute);
        public override void OnXGUI()
        {
            base.OnXGUI();

            serializedObject.Update();
            serializedObject.UpdateIfDirtyOrScript();

            ShowXAttributeMember( target );
        }
		public static void ShowXAttributeMember( object target )
        {
            try
            {
	            EditorGUI.indentLevel++;
	            var type = target.GetType();
	            
                ShowXMethods<XAttribute>( target, type );
                ShowXFields<XAttribute>( target,type  );
                ShowProperties<XAttribute>( target, type );
	            EditorGUI.indentLevel--;
            }
            catch(Exception ex)
            {
				Debug.Log(ex);
            }

        }

		public static void ShowXAttributeType( Type type )
		{
			try
			{
				var c = GUI.contentColor;
				GUI.contentColor = EditorGUIUtility.isProSkin ? new Color( 1f, 1f, 1f, 0.7f ) : new Color( 0f, 0f, 0f, 0.7f );
				ShowXMethods<XAttribute>(null, type);
				ShowXFields<XAttribute>(null, type);
				ShowProperties<XAttribute>(null, type);
				GUI.contentColor = c;
			}
			catch
			{

			}

		}
	    /// <summary>
	    /// https://forum.unity.com/threads/indenting-guilayout-objects.113494/
	    /// </summary>
	    private static int TAB_SIZE = 10;    // pixel size of 4 spaces?
		
		    
        static public bool DrawHeader( string text, string key, bool forceOn, bool minimalistic )
        {
            bool state = EditorPrefs.GetBool( key, true );

            if( !minimalistic )
                GUILayout.Space( 3f );
            // if( !forceOn && !state )
            //     GUI.backgroundColor = new Color( 0.8f, 0.8f, 0.8f );
            BeginHorizontal();
            GUI.changed = false;
	        
            if( minimalistic )
            {
                if( state )
                    text = "\u25BC" + ( char )0x200a + text;
                else
                    text = "\u25BA" + ( char )0x200a + text;

                BeginHorizontal();
	            // GUILayout.Space ( EditorGUI.indentLevel * TAB_SIZE);
                if( !GUILayout.Toggle( true, text, "PreToolbar2", GUILayout.MinWidth( 20f ) ) )
                    state = !state;
                // GUI.contentColor = Color.white;
                EndHorizontal();
            }
            else
            {
                text = "<b><size=11>" + text + "</size></b>";
                if( state )
                    text = "\u25BC " + text;
                else
                    text = "\u25BA " + text;
	            BeginHorizontal();
	            // GUILayout.Space ( EditorGUI.indentLevel * TAB_SIZE);
	            
                if( !GUILayout.Toggle( true, text, "dragtab", GUILayout.MinWidth( 20f ) ) )
                    state = !state;
	            EndHorizontal();
            }

            if( GUI.changed )
                EditorPrefs.SetBool( key, state );

            if( !minimalistic )
                GUILayout.Space( 2f );
            EndHorizontal();
            //GUI.backgroundColor = Color.white;
            if( !forceOn && !state )
                GUILayout.Space( 3f );
            return state;
        }

		static void ShowMulitXMethods<T> (object[] targets)
		{
			Dictionary<MethodInfo,int> methodTimes = new Dictionary<MethodInfo, int> ();
			for (int i = 0; i < targets.Length; i++) {
				var methods = targets [i].GetType ().GetMethods (bindFlags);
				for (int j = 0; j < methods.Length; j++) {
					var method = methods [j];
					if (methodTimes.ContainsKey (method)) {
						methodTimes [method] = methodTimes [method]++;
					} else {
						methodTimes.Add (method, 1);
					}
				}
			}
		}
		
		static void ShowXMethods<T>( object target, Type type )
        {
	        MethodInfo[] methods = null;
	        if (target == null)
		        methods = type.GetMethods( staticFlag );
	        else
		        methods = type.GetMethods( bindFlags );
		    //MethodInfo[] methods = target.GetType().GetMethods( bindFlags );
	        
            bool noAttribute = true;
            foreach( var info in methods )
            {
                foreach( var att in info.GetCustomAttributes( typeof( T ), true ) )
                {
                    noAttribute = false;
                    break;
                }
            }
            if( noAttribute )
                return;

            bool toggle = DrawHeader( "Method", "Method", false, false );

            if( !toggle )
            {
                return;
            }

            List<object> nextShow = new List<object>();

            

            foreach( var info in methods )
            {
	            DrawMethod(info, target, nextShow);
            }
        }

		static void DrawMethod(MethodInfo info, object target, List<object> nextShow)
		{
			var xAttributes = info.GetCustomAttributes(XType, true);
			foreach( var att in xAttributes )
			{
				DrawFieldHeader( info.ReturnType,  info.Name );
				ParameterInfo[] paras = info.GetParameters();

				if (!methodParameters.ContainsKey (info)) {
					object[] o = new object[paras.Length];
					methodParameters.Add (info, o);
				}
				object[] objects = methodParameters [info];

				//if (paras.Length != 0) {
						
					//Use horizontal layout
					using (new GUILayout.VerticalScope ( Skin.textArea, GUILayout.MaxWidth (ForcusWindow.position.width - 40))) {
					
					//using (new GUILayout.AreaScope ( Skin.textArea, GUILayout.MaxWidth (ForcusWindow.position.width - 40))) {
						for (int pos = 0; pos < paras.Length; pos++) {
							if ((paras [pos].Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.None && objects [pos] == null) {
								objects [pos] = paras [pos].DefaultValue;
							}
							
							DrawFieldHeader (paras [pos].ParameterType, paras [pos].Name);
							objects [pos] = GetTypeGUIOpt (objects [pos], paras [pos].ParameterType, paras [pos].Name, nextShow);
							   
						}
						
						if (GUILayout.Button (info.Name)) {
							var invokeValue = info.Invoke (target, objects);
							if(invokeValue != null)
							{
								XLogger.Log(info.Name + " return : " + invokeValue.ToString());
							}
						}
					}

				//}

				
			}
		}

		static void DrawProperty(PropertyInfo property, bool isStatic, object target)
		{
			var xAttributes = property.GetCustomAttributes(XType, true);
			foreach( var att in xAttributes )
			{
				List<object> nextShow = new List<object>();


				BeginVertical();

				try
				{
					object result = null;
					if (isStatic) {
						result = property.GetValue( null, null );
					} else {
						result = property.GetValue( target, null );
					}

					DrawFieldHeader( property.PropertyType, property.Name );

					EditorGUI.BeginDisabledGroup( !property.CanWrite );
					var newValue = GetTypeGUI( result, property.PropertyType, property.Name, nextShow );
					EditorGUI.EndDisabledGroup();

					if( !newValue.Equals( result ) )
						property.SetValue( target, newValue, null );
				}catch(Exception e){
					// XLogger.LogError(e.Message);
				}

				EndVertical();

				foreach( var entry in nextShow )
				{
					var type = entry.GetType();
					bool isShow = DrawHeader( type.Name, type.Name, false, false );
					if( isShow )
					{
						ShowXAttributeMember( entry );
					}
				}
			}
		}

		static void DrawField(FieldInfo field, object target)
		{
			var xAttributes = field.GetCustomAttributes(XType, true);
			foreach( var att in xAttributes)
			{
				List<object> nextShow = new List<object>();

				BeginVertical();

				try{
					DrawFieldHeader( field.FieldType,  field.Name );

					object result;
					if (field.IsStatic) {
						result = field.GetValue( null );
					} else {
						result = field.GetValue( target );
					}
					

					var newValue = GetTypeGUIOpt
						( result, field.FieldType,field.Name, nextShow );
					//XLogger.Log (nextShow.Count + " result : " + result + " field.FieldType " + field.FieldType );
					if (!newValue.Equals(result))
					{
						if (field.IsStatic)
							field.SetValue( null, newValue );
						else
							field.SetValue( target, newValue );
					}
				}catch(Exception e)
				{
					// XLogger.Log(e.Message);
				}

				EndVertical();

				foreach( var entry in nextShow )
				{
					var type = entry.GetType();
					bool isShow = DrawHeader( type.Name, type.Name, false, false );

					if( isShow )
					{
						ShowXAttributeMember( entry );
					}
				}
			}
		}
		static void ShowXFields<T>( object target, Type type)
		{
			FieldInfo[] Fields = null;
			if (target == null)
				Fields = type.GetFields( staticFlag );
			else
				Fields = target.GetType().GetFields( bindFlags );
            bool noAttribute = true;
            foreach( var info in Fields )
            {
                foreach( var att in info.GetCustomAttributes( typeof( T ), true ) )
                {
                    noAttribute = false;
                    break;
                }
            }
            if( noAttribute )
                return;

            bool toggle = DrawHeader( "Field", "Field", false, false );

            if( !toggle )
            {
                return;
            }
            

            foreach( var field in Fields )
            {
	            DrawField(field, target);
            }

            

        }

		

		static void ShowProperties<T>( object target, Type type )
        {
	        PropertyInfo[] Properties = null;
	        bool isStatic = false;
	        if (target == null)
	        {
		        isStatic = true;
		        Properties = type.GetProperties( staticFlag );
	        }
	        else
		        Properties = target.GetType().GetProperties( bindFlags );
	        
            bool noAttribute = true;
            foreach( var info in Properties )
            {
                foreach( var att in info.GetCustomAttributes( typeof( T ), true ) )
                {
                    noAttribute = false;
                    break;
                }
            }
            if( noAttribute )
                return;
            bool toggle = DrawHeader( "Property", "Property", false, false );

            if( !toggle )
            {
                return;
            }

            

            foreach( var property in Properties )
            {
                DrawProperty(property, isStatic, target);
            }

            
        }

        GUILayoutOption widthOption = GUILayout.MaxWidth( 150 );
		protected static object GetTypeGUI( object t, Type type, string valueName, List<object> nextShow )
        {
            if( t == null )
                t = GetDefaultValue( type );
			if (t is int || t is System.Int32 || type == typeof(int)) {
				t = CreateIntField (valueName, Convert.ToInt32 (t));
			} else if (t is System.Int16) {
				t = (short)CreateIntField (valueName, Convert.ToInt16 (t));
			} else if (t is System.Int64) {
				t = CreateLongField (valueName, Convert.ToInt64 (t));
			}else if (t is uint || type ==typeof(UInt32) || t is ushort)
			{
				t = (uint)CreateIntField (valueName, Convert.ToInt32 (t));
			} 
			else if (t is byte ||  type ==typeof(Byte)) {
				int value = Convert.ToInt32 (t);
				t = Convert.ToByte (CreateIntField (valueName, value));
            }
            else if (t is byte[])
            {
                byte[] memory = (byte[])t;
                string str = System.Text.Encoding.Default.GetString(memory);
                string str1 = CreateStringField( valueName, str);
                if (str != str1)
                {
                    t = System.Text.Encoding.Default.GetBytes(str1);
                }
            }
            else if (type == typeof(String)) {
				t = CreateStringField (valueName, (string)t);
			} else if (type == typeof(Single)) {
				t = CreateFloatField (valueName, Convert.ToSingle (t));
			} else if (type == typeof(Boolean)) {
				t = CreateCheckBox (valueName, Convert.ToBoolean (t));
			} else if (type.BaseType == typeof(Enum)) {
                if(type.GetAttribute<FlagsAttribute>() != null)
                {
                    t = CreateEnumFlagsField(valueName, (Enum)t ?? (Enum)Enum.ToObject(type, 0));
                }
                else
                {
                    t = CreateEnumPopup(valueName, (Enum)t ?? (Enum)Enum.ToObject(type, 0));
                }
				
			} else if (type.IsSubclassOf (typeof(Object))) {
				t = CreateObjectField (valueName, (Object)t, type);
			}else if (t is Rect) {
				t = EditorGUILayout.RectField(valueName, (Rect)t);
			}else if (t is Color || t is Color32) {
				t = EditorGUILayout.ColorField (valueName, (Color)t);
			} else if (t is Vector2) {
				Vector2 v = (Vector2)t;
				t = CreateVector2Field (valueName, v);
			}else if (t is Vector2Int) {
				Vector2Int v = (Vector2Int)t;
				t = CreateVector2IntField (valueName, v);
			}  
			else if (t is Vector3) {
				Vector3 v = (Vector3)t;
				t = CreateVector3Field (valueName, v);
			}else if (t is Vector3Int) {
				Vector3Int v = (Vector3Int)t;
				t = CreateVector3IntField(valueName, v);
			} 
			else if (t is Vector4) {
				Vector4 v = (Vector4)t;
				t = CreateVector4Field (valueName, v);
			}
			else if (t is Quaternion) {
				Quaternion q = (Quaternion)t;
				Vector4 v = new Vector4 (q.x, q.y, q.z, q.w);
				v = CreateVector4Field (valueName, v);
				q.x = v.x;
				q.y = v.y;
				q.z = v.z;
				q.w = v.w;
				t = q;
			} else if (t is Matrix4x4) {
				Matrix4x4 m = (Matrix4x4)t;
				CreateLabel (valueName);
				for (int i = 0; i < 4; i++) {
					//BeginVertical ();
					CreateVector4Field ("", m.GetRow(i));
					//EndVertical ();
				}
			}
			else if( t is DateTime )
			{
				DateTime dateTime = ( DateTime )t;
				
//				string toString = CreateStringField( dateTime.ToString(XEditorSetting.CultureInfo));
//				t = DateTime.Parse(toString, XEditorSetting.CultureInfo);
				
			}
            else if( typeof( IList ).IsAssignableFrom( type ) )
            {
				IList list = t as IList;
				if (list == null)
					XLogger.Log (t.GetType ().ToString ());
				var name = valueName;
				bool toggle = DrawHeader(name + " : " + list.Count , name, false, false );
				if( list == null || !toggle)
                    return t;
				DoButton ("Clear", () => list.Clear ());
                var newList = new List<object>();

                BeginVertical();
                for( int pos = 0; pos < list.Count; pos++ )
                {
                    //  TODO loop in list.Count
                    var o = list[pos];
					GetTypeGUI( o, o.GetType(), valueName + "_" + pos, newList );
                }
                //bool isShow = DrawHeader( type.Name, type.Name, false, false );
                EndVertical();

                DrawListType( newList );


            }
			else if( type.GetGenericTypeDefinition() == typeof(Queue<>))
			{
				Queue queue = ( Queue )t;
				
				var name = valueName;
				bool toggle = DrawHeader(name + " : " + queue.Count , name, false, false );
				if( queue == null || !toggle)
					return t;
				
				using( new EditorGUILayout.HorizontalScope() )
				{
					DoButton ("Clear", () => queue.Clear());
					DoButton( "Dequeue",()=>queue.Dequeue() );
				}
				
				var newList = new List<object>();

				BeginVertical();

				var array = queue.ToArray();
				for( int pos = 0; pos < array.Length; pos++ )
				{
					//  TODO loop in list.Count
					var o = array[pos];
					GetTypeGUIOpt( o, o.GetType(), valueName + "_" + pos, newList );
				}
				//bool isShow = DrawHeader( type.Name, type.Name, false, false );
				EndVertical();

				DrawListType( newList );
				
			}
			else if( type.GetGenericTypeDefinition() == typeof(Stack<>) )
			{
				Stack stack = ( Stack )t;
				
				var name = valueName;
				bool toggle = DrawHeader(name + " : " + stack.Count , name, false, false );
				if( stack == null || !toggle)
					return t;
				
				using( new EditorGUILayout.HorizontalScope() )
				{
					DoButton ("Clear", () => stack.Clear());
					DoButton( "Pop",()=>stack.Pop() );
				}
				
				var newList = new List<object>();

				BeginVertical();

				var array = stack.ToArray();
				for( int pos = 0; pos < array.Length; pos++ )
				{
					//  TODO loop in list.Count
					var o = array[pos];
					GetTypeGUIOpt( o, o.GetType(), valueName + "_" + pos, newList );
				}
				//bool isShow = DrawHeader( type.Name, type.Name, false, false );
				EndVertical();

				DrawListType( newList );
				
			}
            else if( typeof( IDictionary ).IsAssignableFrom( type ) )
            {
                IDictionary dictionary = ( IDictionary )t;
                IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
                IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
                ICollection collection = dictionary.Values;
				var name = valueName;
				bool toggle = DrawHeader( name+ " : " + dictionary.Count, name, false, false );
				if(!toggle)
					return t;
				DoButton ("Clear", () => dictionary.Clear());
                while( iteratorKey.MoveNext() && iteratorValue.MoveNext() )
                {
                    var newList = new List<object>();
                    BeginHorizontal();
					var keyType = iteratorKey.Current.GetType ();
					var valueType = dictionary [iteratorKey.Current].GetType ();
					GetTypeGUI( iteratorKey.Current, keyType , keyType.Name, newList );
					GetTypeGUI( dictionary[iteratorKey.Current], valueType,valueType.Name, newList );
                    EndHorizontal();
                    DrawListType( newList );
                }

            }

//            else if( typeof( IEnumerable ).IsAssignableFrom( type ) )
//            {
//				bool toggle = DrawHeader( valueName, valueName, false, false );
//				if(!toggle)
//					return t;
//				
//                IEnumerable collection = ( IEnumerable )t;
//				IEnumerator iteratorValue = collection.GetEnumerator();
//	            
//                int index = 0;
//                var newList = new List<object>();
//                while( iteratorValue.MoveNext() )
//                {
//					var valueType = iteratorValue.Current.GetType();
//                    if( iteratorValue.Current != null )
//						GetTypeGUI( iteratorValue.Current, valueType, valueType.Name + "_" + index, newList );
//                    index++;
//                }
//            }
            else if( t != null )
            {
                if( !nextShow.Contains( t ) )
                    nextShow.Add( t );

//                EditorGUILayout.Space();
//                DrawHeader( type.Name, type.Name, false, false );

            }
            else
            {
                CreateLabel( "NULL" );
            }

            return t;

        }

		protected static object GetTypeGUIOpt( object t, Type type, string valueName, List<object> nextShow )
		{
			var controlRect = EditorGUILayout.GetControlRect();
			if( t == null )
				t = GetDefaultValue( type );
			if (t is int || t is System.Int32 || type == typeof(int)) {
				t = EditorGUI.IntField (controlRect,valueName, Convert.ToInt32 (t));
			} else if (t is System.Int16) {
				t = (short)EditorGUI.IntField (controlRect, valueName, Convert.ToInt16 (t));
			} else if (t is System.Int64) {
				t = EditorGUI.IntField (controlRect, valueName, (int)Convert.ToInt64 (t));
			}else if (t is uint || type ==typeof(UInt32) || t is ushort)
			{
				t = (uint)EditorGUI.IntField (controlRect, valueName, Convert.ToInt32(t));
			}  
			else if (t is byte) {
				int value = Convert.ToInt32 (t);
				t = Convert.ToByte (EditorGUI.IntField (controlRect, valueName, value));
			}
            else if (t is byte[])
            {
                byte[] memory = (byte[])t;
                string str = System.Text.Encoding.Default.GetString(memory);
                string str1 = EditorGUI.TextField(controlRect, valueName, str);
                if(str != str1)
                {
                    t = System.Text.Encoding.Default.GetBytes(str1);
                }
            }
            else if (type == typeof(String)) {
				t = EditorGUI.TextField (controlRect, valueName, (string)t);
			} else if (type == typeof(Single)) {
				t = EditorGUI.FloatField (controlRect, valueName, Convert.ToSingle (t));
			} else if (type == typeof(Boolean)) {
				t = EditorGUI.Toggle (controlRect, valueName, Convert.ToBoolean (t));
			} else if (type.BaseType == typeof(Enum)) {
				t = EditorGUI.EnumPopup (controlRect, valueName, (Enum)t ?? (Enum)Enum.ToObject (type, 0));
			} else if (type.IsSubclassOf (typeof(Object))) {
				t = EditorGUI.ObjectField (controlRect, valueName, (Object)t, type);
			} else if (t is Color || t is Color32) {
				t = EditorGUI.ColorField(controlRect, valueName, (Color)t);
			}else if (t is Rect) {
				t = EditorGUI.RectField(controlRect, valueName, (Rect)t);
			} else if (t is Vector2) {
				Vector2 v = (Vector2)t;
				t = EditorGUI.Vector2Field (controlRect, valueName, v);
			}else if (t is Vector2Int) {
				Vector2Int v = (Vector2Int)t;
				t = EditorGUI.Vector2IntField (controlRect, valueName, v);
			}
			else if (t is Vector3Int) {
				Vector3Int v = (Vector3Int)t;
				t = EditorGUI.Vector3IntField (controlRect, valueName, v);
			}
			else if (t is Vector3) {
				Vector3 v = (Vector3)t;
				t = EditorGUI.Vector3Field (controlRect, valueName, v);
			}
			else if (t is Vector4) {
				Vector4 v = (Vector4)t;
				t = EditorGUI.Vector4Field (controlRect, valueName, v);
			} else if (t is Quaternion) {
				Quaternion q = (Quaternion)t;
				Vector4 v = new Vector4 (q.x, q.y, q.z, q.w);
				v = EditorGUI.Vector4Field (controlRect, valueName, v);
				q.x = v.x;
				q.y = v.y;
				q.z = v.z;
				q.w = v.w;
				t = q;
			} else if (t is Matrix4x4) {
				Matrix4x4 m = (Matrix4x4)t;
				CreateLabel (valueName);
				for (int i = 0; i < 4; i++) {
					//BeginVertical ();
					EditorGUI.Vector2Field (controlRect, "", m.GetRow(i));
					//EndVertical ();
				}
			}
            else if( t is DateTime )
			{
				DateTime dateTime = ( DateTime )t;
				
//				string toString = CreateStringField( dateTime.ToString(XEditorSetting.CultureInfo));
//				t = DateTime.Parse(toString, XEditorSetting.CultureInfo);
				
			}
			else if( typeof( IList ).IsAssignableFrom( type ) )
			{
				IList list = t as IList;
				if (list == null)
					XLogger.Log (t.GetType ().ToString ());
				var name = valueName;
				bool toggle = DrawHeader(name + " : " + list.Count , name, false, false );
				if( list == null || !toggle)
					return t;
				DoButton ("Clear", () => list.Clear ());
				
				var newList = new List<object>();

				BeginVertical();
				for( int pos = 0; pos < list.Count; pos++ )
				{
					//  TODO loop in list.Count
					var o = list[pos];
					using( new EditorGUILayout.HorizontalScope() )
					{
						GetTypeGUIOpt( o, o.GetType(), valueName + "_" + pos, newList );
						DoButton( "Delete", () =>
						{
							list.Remove( o );
						} );
					}
				}
				//bool isShow = DrawHeader( type.Name, type.Name, false, false );
				EndVertical();

				DrawListType( newList );


			}
//			else if( typeof(Queue<>).IsAssignableFrom( type ) )
//			{
//				Queue<> queue = ( Queue<> )t;
//				
//				var name = valueName;
//				bool toggle = DrawHeader(name + " : " + queue.Count , name, false, false );
//				if( queue == null || !toggle)
//					return t;
//				
//				using( new EditorGUILayout.HorizontalScope() )
//				{
//					DoButton ("Clear", () => queue.Clear());
//					DoButton( "Dequeue",()=>queue.Dequeue() );
//				}
//				
//				var newList = new List<object>();
//
//				BeginVertical();
//
//				var array = queue.ToArray();
//				for( int pos = 0; pos < array.Length; pos++ )
//				{
//					//  TODO loop in list.Count
//					var o = array[pos];
//					GetTypeGUIOpt( o, o.GetType(), valueName + "_" + pos, newList );
//				}
//				//bool isShow = DrawHeader( type.Name, type.Name, false, false );
//				EndVertical();
//
//				DrawListType( newList );
//				
//			}
//			else if( typeof(Stack<>).IsAssignableFrom( type )  )
//			{
//				var stack = ( Stack<> )t;
//				
//				var name = valueName;
//				bool toggle = DrawHeader(name + " : " + stack.Count , name, false, false );
//				if( stack == null || !toggle)
//					return t;
//				
//				using( new EditorGUILayout.HorizontalScope() )
//				{
//					DoButton ("Clear", () => stack.Clear());
//					DoButton( "Pop",()=>stack.Pop() );
//				}
//				
//				var newList = new List<object>();
//
//				BeginVertical();
//
//				var array = stack.ToArray();
//				for( int pos = 0; pos < array.Length; pos++ )
//				{
//					//  TODO loop in list.Count
//					var o = array[pos];
//					GetTypeGUIOpt( o, o.GetType(), valueName + "_" + pos, newList );
//				}
//				//bool isShow = DrawHeader( type.Name, type.Name, false, false );
//				EndVertical();
//
//				DrawListType( newList );
//				
//			}
			else if( typeof( IDictionary ).IsAssignableFrom( type ) )
			{
				IDictionary dictionary = ( IDictionary )t;
				IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
				IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
				ICollection collection = dictionary.Values;
				var name = valueName;
				bool toggle = DrawHeader( name + " : " + dictionary.Count, name, false, false );
				if(!toggle)
					return t;
				DoButton ("Clear", () => dictionary.Clear());
                while( iteratorKey.MoveNext() && iteratorValue.MoveNext() )
                {
                    var newList = new List<object>();
                    //BeginHorizontal();
					var keyType = iteratorKey.Current.GetType ();
					var valueType = dictionary [iteratorKey.Current].GetType ();
					GetTypeGUI( iteratorKey.Current, keyType , keyType.Name, newList );
					GetTypeGUI( dictionary[iteratorKey.Current], valueType,valueType.Name, newList );
                    //EndHorizontal();
                    DrawListType( newList );
                }

            }

//            else if( typeof( IEnumerable ).IsAssignableFrom( type ) )
//            {
//				bool toggle = DrawHeader( valueName, valueName, false, false );
//				if(!toggle)
//					return t;
//				
//                IEnumerable collection = ( IEnumerable )t;
//				IEnumerator iteratorValue = collection.GetEnumerator();
//	            
//                int index = 0;
//                var newList = new List<object>();
//                while( iteratorValue.MoveNext() )
//                {
//					var valueType = iteratorValue.Current.GetType();
//                    if( iteratorValue.Current != null )
//						GetTypeGUI( iteratorValue.Current, valueType, valueType.Name + "_" + index, newList );
//                    index++;
//                }
//            }
            else if( t != null )
            {
                if( !nextShow.Contains( t ) )
                    nextShow.Add( t );

//                EditorGUILayout.Space();
//                DrawHeader( type.Name, type.Name, false, false );

            }
            else
            {
                CreateLabel( "NULL" );
            }

            return t;

		}

		static void DrawListType(List<object> totalObject)
        {
            foreach( var entry in totalObject )
            {
                
                var t = entry.GetType();
                bool isShow = DrawHeader( t.Name, t.Name, false, false );
                if( isShow )
                {
                    ShowXAttributeMember( entry );
                }
            }
        }

		static object GetDefaultValue( Type t )
        {
            if( t.IsValueType )
                return Activator.CreateInstance( t );

            return null;
        }

		private static void DrawFieldHeader(Type type, string fieldName)
        {
            if( type.IsGenericType )
            {
                var args = type.GetGenericArguments();
                // string result = "";
                // for( int i = 0; i < args.Length; i++ )
                // {
                //     if( i == 0 )
                //     {
                //         result = args[i].Name;
                //     }
                //     else
                //     {
                //         result += "," + args[i].Name;
                //     }
                    
                // }
                var s = tools.StringUtils.Substring( type.Name, "`" );
                //CreateLabel( string.Format( "{0}<{1}> : {2}", s, result, fieldName ) );
            }
            else
            {
                //CreateLabel( type.Name + " : " + fieldName );
            }
        }

        private void OpenInMethod( object target )
        {
            XReflectionWindow method = XBaseWindow.InitWindow<XReflectionWindow>();
            method.Target = target;
        }
    }
}