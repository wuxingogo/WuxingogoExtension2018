
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
        public void ShowXAttributeMember(object target)
        {
            GetTargetMethod<XAttribute>( target );
            GetTargetField<XAttribute>( target );
            GetTargetProperty<XAttribute>( target );
        }

        void GetTargetMethod<T>( object target )
        {
            if( target == null )
                return;
            foreach( var info in target.GetType().GetMethods( bindFlags ) )
            {
                foreach( var att in info.GetCustomAttributes( typeof( T ), true ) )
                {
                    BeginVertical();

                    CreateSpaceBox();
                    CreateLabel( "XMethod : " + info.Name );
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
                        CreateLabel( paras[pos].ParameterType.Name );
                        objects[pos] = GetTypeGUI( objects[pos], paras[pos].ParameterType );
                        EndHorizontal();
                    }
                    if( CreateSpaceButton( info.Name ) )
                    {
                        info.Invoke( target, objects );
                    }
                    CreateSpaceBox();

                    EndVertical();
                }
            }
        }

        void GetTargetField<T>( object target )
        {
            if( target == null )
                return;
            foreach( var field in target.GetType().GetFields( bindFlags ) )
            {
                foreach( var att in field.GetCustomAttributes( typeof( T ), true ) )
                {
                    BeginVertical();

                    CreateSpaceBox();
                    CreateLabel( "XField : " + field.Name + " || " + field.GetValue( target ).ToString() );

                    if( typeof( IDictionary ).IsAssignableFrom( field.FieldType ) )
                    {
                        IDictionary dictionary = ( IDictionary )field.GetValue( target );
                        IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
                        IEnumerator iteratorValue = dictionary.Values.GetEnumerator();
                        ICollection collection = dictionary.Values;


                        while( iteratorKey.MoveNext() && iteratorValue.MoveNext() )
                        {
                            BeginHorizontal();
                            GetTypeGUI( iteratorKey.Current, iteratorKey.Current.GetType() );
                            var oldValue = GetTypeGUI( dictionary[iteratorKey.Current], dictionary[iteratorKey.Current].GetType() );
                            EndHorizontal();

                        }

                    }

                    else if( typeof( IEnumerable ).IsAssignableFrom( field.FieldType ) )
                    {

                        IEnumerable collection = ( IEnumerable )field.GetValue( target );

                        IEnumerator iteratorValue = collection.GetEnumerator();
                        int index = 0;
                        while( iteratorValue.MoveNext() )
                        {
                            if( iteratorValue.Current != null )
                                GetTypeGUI( iteratorValue.Current, iteratorValue.Current.GetType() );
                            index++;
                        }
                    }
                    EndVertical();
                }
            }

        }

        void GetTargetProperty<T>( object target )
        {
            if( target == null )
                return;
            foreach( var property in target.GetType().GetProperties( bindFlags ) )
            {
                foreach( var att in property.GetCustomAttributes( typeof( XAttribute ), true ) )
                {


                    CreateSpaceBox();

                    BeginVertical();

                    object result = property.GetValue( target, null );


                    CreateLabel( "XProperty : " + property.Name + " || " );

                    EditorGUI.BeginDisabledGroup( !property.CanWrite );
                    var newValue = GetTypeGUI( result, property.PropertyType );
                    EditorGUI.EndDisabledGroup();

                    if( null != newValue && !newValue.Equals( result ) )
                        property.SetValue( target, newValue, null );


                    EndVertical();
                }
            }
        }
        GUILayoutOption maxWidthOpt = GUILayout.MaxWidth( 150 );
        protected object GetTypeGUI( object t, Type type )
        {
            if( t == null )
                t = GetDefaultValue( type );
            if( t is int || t is System.Int32 || type == typeof( int ) )
            {
                t = CreateIntField( Convert.ToInt32( t ), maxWidthOpt );
            }
            else if( t is System.Int16 )
            {
                t = ( short )CreateIntField( Convert.ToInt16( t ), maxWidthOpt );
            }
            else if( t is System.Int64 )
            {
                t = CreateLongField( Convert.ToInt64( t ), maxWidthOpt );
            }
            else if( t is byte )
            {
                int value = Convert.ToInt32( t );
                t = Convert.ToByte( CreateIntField( value, maxWidthOpt ) );
            }
            else if( type == typeof( String ) )
            {
                t = CreateStringField( ( string )t );
            }
            else if( type == typeof( Single ) )
            {
                t = CreateFloatField( Convert.ToSingle( t ), maxWidthOpt );
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
                BeginVertical();
                for( int pos = 0; pos < list.Count; pos++ )
                {
                    //  TODO loop in list.Count
                    var o = list[pos];
                    GetTypeGUI( o, o.GetType() );
                }
                EndVertical();
            }
            else if( t != null )
            {
                if( !groupDict.ContainsKey( t ) )
                {
                    groupDict.Add( t, true );
                }

                EditorGUILayout.Space();
                groupDict[t] = EditorGUILayout.Foldout( groupDict[t], type.Name );
                if( groupDict[t] )
                {
                    ShowXAttributeMember( t );
                }
            }

            return t;

        }

        object GetDefaultValue( Type t )
        {
            if( t.IsValueType )
                return Activator.CreateInstance( t );

            return null;
        }




        private void OpenInMethod( object target )
        {
            XReflectionWindow method = XBaseWindow.InitWindow<XReflectionWindow>();
            method.Target = target;
        }
    }
}