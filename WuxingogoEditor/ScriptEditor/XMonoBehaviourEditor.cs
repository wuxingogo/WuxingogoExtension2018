
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
    using Object = UnityEngine.Object;

    [CustomEditor( typeof( XMonoBehaviour ), true )]
    [CanEditMultipleObjects]
    public class XMonoBehaviourEditor : XBaseEditor
    {
        private Dictionary<MethodInfo, object[]> methodParameters = new Dictionary<MethodInfo, object[]>();
        private Dictionary<object, bool> groupDict = new Dictionary<object, bool>();
        BindingFlags bindFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        public override void OnXGUI()
        {
            base.OnXGUI();

            serializedObject.Update();
            serializedObject.UpdateIfDirtyOrScript();

            ShowXAttributeMember( target );
        }
        public void ShowXAttributeMember( object target )
        {
            try
            {
                ShowXMethods<XAttribute>( target );
                ShowXFields<XAttribute>( target );
                ShowProperties<XAttribute>( target );
            }
            catch
            {

            }

        }

        static public bool DrawHeader( string text, string key, bool forceOn, bool minimalistic )
        {
            bool state = EditorPrefs.GetBool( key, true );

            if( !minimalistic )
                GUILayout.Space( 3f );
            if( !forceOn && !state )
                GUI.backgroundColor = new Color( 0.8f, 0.8f, 0.8f );
            GUILayout.BeginHorizontal();
            GUI.changed = false;

            if( minimalistic )
            {
                if( state )
                    text = "\u25BC" + ( char )0x200a + text;
                else
                    text = "\u25BA" + ( char )0x200a + text;

                GUILayout.BeginHorizontal();
                GUI.contentColor = EditorGUIUtility.isProSkin ? new Color( 1f, 1f, 1f, 0.7f ) : new Color( 0f, 0f, 0f, 0.7f );
                if( !GUILayout.Toggle( true, text, "PreToolbar2", GUILayout.MinWidth( 20f ) ) )
                    state = !state;
                GUI.contentColor = Color.white;
                GUILayout.EndHorizontal();
            }
            else
            {
                text = "<b><size=11>" + text + "</size></b>";
                if( state )
                    text = "\u25BC " + text;
                else
                    text = "\u25BA " + text;
                if( !GUILayout.Toggle( true, text, "dragtab", GUILayout.MinWidth( 20f ) ) )
                    state = !state;
            }

            if( GUI.changed )
                EditorPrefs.SetBool( key, state );

            if( !minimalistic )
                GUILayout.Space( 2f );
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            if( !forceOn && !state )
                GUILayout.Space( 3f );
            return state;
        }

        void ShowXMethods<T>( object target )
        {
            if( target == null )
                return;

            var methods = target.GetType().GetMethods( bindFlags );
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
                foreach( var att in info.GetCustomAttributes( typeof( T ), true ) )
                {
                    BeginVertical();

                    DrawFieldHeader( info.ReturnType,  info.Name );
                    ParameterInfo[] paras = info.GetParameters();
                    if( !methodParameters.ContainsKey( info ) )
                    {
                        object[] o = new object[paras.Length];
                        methodParameters.Add( info, o );
                    }
                    object[] objects = methodParameters[info];
                    for( int pos = 0; pos < paras.Length; pos++ )
                    {
                        BeginHorizontal();
                        DrawFieldHeader( paras[pos].ParameterType, paras[pos].Name );
                        objects[pos] = GetTypeGUI( objects[pos], paras[pos].ParameterType, nextShow );
                        EndHorizontal();
                    }
                    if( CreateSpaceButton( info.Name ) )
                    {
                        info.Invoke( target, objects );
                    }

                    EndVertical();
                }
            }
        }

        void ShowXFields<T>( object target )
        {
            if( target == null )
                return;
            var Fields = target.GetType().GetFields( bindFlags );
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
                foreach( var att in field.GetCustomAttributes( typeof( T ), true ) )
                {
                    List<object> nextShow = new List<object>();

                    BeginVertical();

                    DrawFieldHeader( field.FieldType,  field.Name );

                    object result = field.GetValue( target );

                    var newValue = GetTypeGUI( result, field.FieldType, nextShow );

                    if( null != newValue && !newValue.Equals( result ) )
                        field.SetValue( target, newValue );

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

            

        }

        void ShowProperties<T>( object target )
        {
            if( target == null )
                return;

            var Properties = target.GetType().GetProperties( bindFlags );
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
                foreach( var att in property.GetCustomAttributes( typeof( XAttribute ), true ) )
                {
                    List<object> nextShow = new List<object>();


                    BeginVertical();

                    object result = property.GetValue( target, null );


                    DrawFieldHeader( property.PropertyType, property.Name );

                    EditorGUI.BeginDisabledGroup( !property.CanWrite );
                    var newValue = GetTypeGUI( result, property.PropertyType, nextShow );
                    EditorGUI.EndDisabledGroup();

                    if( null != newValue && !newValue.Equals( result ) )
                        property.SetValue( target, newValue, null );


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

            
        }



        GUILayoutOption widthOption = GUILayout.MaxWidth( 150 );
        protected object GetTypeGUI( object t, Type type, List<object> nextShow )
        {
            if( t == null )
                t = GetDefaultValue( type );
            if( t is int || t is System.Int32 || type == typeof( int ) )
            {
                t = CreateIntField( Convert.ToInt32( t ), widthOption );
            }
            else if( t is System.Int16 )
            {
                t = ( short )CreateIntField( Convert.ToInt16( t ), widthOption );
            }
            else if( t is System.Int64 )
            {
                t = CreateLongField( Convert.ToInt64( t ), widthOption );
            }
            else if( t is byte )
            {
                int value = Convert.ToInt32( t );
                t = Convert.ToByte( CreateIntField( value, widthOption ) );
            }
            else if( type == typeof( String ) )
            {
                t = CreateStringField( ( string )t );
            }
            else if( type == typeof( Single ) )
            {
                t = CreateFloatField( Convert.ToSingle( t ), widthOption );
            }
            else if( type == typeof( Boolean ) )
            {
                t = CreateCheckBox( Convert.ToBoolean( t ) );
            }
            else if( type.BaseType == typeof( Enum ) )
            {
                t = CreateEnumSelectable( "", ( Enum )t ?? ( Enum )Enum.ToObject( type, 0 ) );
            }
            else if( type.IsSubclassOf( typeof( Object ) ) )
            {
                t = CreateObjectField( ( Object )t, type );
            }
            else if( t is Color || t is Color32 )
            {
                t = EditorGUILayout.ColorField( ( Color )t, widthOption );
            }
            else if( t is Vector2 )
            {
                Vector2 v = ( Vector2 )t;
                t = CreateVector2Field( "", v );
            }
            else if( t is Vector3 )
            {
                Vector3 v = ( Vector3 )t;
                t = CreateVector3Field( "", v );
            }
            else if( t is Vector4 )
            {
                Vector4 v = ( Vector4 )t;
                t = CreateVector4Field( "", v );
            }
            else if( t is Quaternion )
            {
                Quaternion q = ( Quaternion )t;
                Vector4 v = new Vector4( q.x, q.y, q.z, q.w );
                v = CreateVector4Field( type.Name, v );
                q.x = v.x;
                q.y = v.y;
                q.z = v.z;
                q.w = v.w;
                t = q;
            }
            else if( typeof( IList ).IsAssignableFrom( type ) )
            {
                IList list = t as IList;
                if( list == null )
                    return t;
                var newList = new List<object>();

                BeginVertical();
                for( int pos = 0; pos < list.Count; pos++ )
                {
                    //  TODO loop in list.Count
                    var o = list[pos];
                    GetTypeGUI( o, o.GetType(), newList );
                }
                //bool isShow = DrawHeader( type.Name, type.Name, false, false );
                EndVertical();

                //DrawListType( newList, isShow );


            }
            else if( typeof( IDictionary ).IsAssignableFrom( type ) )
            {
                IDictionary dictionary = ( IDictionary )t;
                IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
                IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
                ICollection collection = dictionary.Values;


                while( iteratorKey.MoveNext() && iteratorValue.MoveNext() )
                {
                    var newList = new List<object>();
                    BeginHorizontal();
                    GetTypeGUI( iteratorKey.Current, iteratorKey.Current.GetType(), newList );
                    var oldValue = GetTypeGUI( dictionary[iteratorKey.Current], dictionary[iteratorKey.Current].GetType(), newList );
                    EndHorizontal();
                    DrawListType( newList );
                }

            }

            else if( typeof( IEnumerable ).IsAssignableFrom( type ) )
            {

                IEnumerable collection = ( IEnumerable )t;

                IEnumerator iteratorValue = collection.GetEnumerator();
                int index = 0;
                var newList = new List<object>();
                while( iteratorValue.MoveNext() )
                {
                    if( iteratorValue.Current != null )
                        GetTypeGUI( iteratorValue.Current, iteratorValue.Current.GetType(), newList );
                    index++;
                }
            }
            else if( t != null )
            {
                if( !nextShow.Contains( t ) )
                    nextShow.Add( t );

                EditorGUILayout.Space();
                DrawHeader( type.Name, type.Name, false, false );

            }
            else
            {
                CreateLabel( "NULL" );
            }

            return t;

        }

        void DrawListType(List<object> totalObject)
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

        object GetDefaultValue( Type t )
        {
            if( t.IsValueType )
                return Activator.CreateInstance( t );

            return null;
        }

        private void DrawFieldHeader(Type type, string fieldName)
        {
            if( type.IsGenericType )
            {
                var args = type.GetGenericArguments();
                string result = "";
                for( int i = 0; i < args.Length; i++ )
                {
                    if( i == 0 )
                    {
                        result = args[i].Name;
                    }
                    else
                    {
                        result += "," + args[i].Name;
                    }
                    
                }
                var s = tools.StringUtils.CutOnCharLeft( type.Name, "`" );
                CreateLabel( string.Format( "{0}<{1}> : {2}", s, result, fieldName ) );
            }
            else
            {
                CreateLabel( type.Name + " : " + fieldName );
            }
        }

        private void OpenInMethod( object target )
        {
            XReflectionWindow method = XBaseWindow.InitWindow<XReflectionWindow>();
            method.Target = target;
        }
    }
}