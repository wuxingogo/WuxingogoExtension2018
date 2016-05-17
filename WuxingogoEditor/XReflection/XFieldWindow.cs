
namespace wuxingogo.Reflection
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections;
    using System.Reflection;
    using System.Collections.Generic;


    public class XFieldWindow : XBaseWindow
    {
        Object uObject = null;

        private object _target = null;

        public object Target
        {
            get
            {
                return _target;
            }
            set
            {
                if( _target != value )
                {

                    _target = value;
                    OnChangeTarget();
                }
            }
        }

        public override object[] closeRecordArgs
        {
            get
            {
                return new object[] { Target };
            }
        }

        public override void OnInitialization( params object[] args )
        {
            if( args.Length > 0 )
                Target = args[0];
        }

        public override void OnXGUI()
        {

            if( CreateSpaceButton( "Clean" ) )
            {
                uObject = null;
                Target = null;
            }

            if( null == Target )
            {
                uObject = CreateObjectField( "", uObject );
                Target = uObject;
            }
            else
            {


                FieldInfo[] fields = Target.GetType().GetFields();
                foreach( FieldInfo field in fields )
                {
                    if( Target.GetType() == field.DeclaringType )
                    {
                        //					CreateStringField("method name " , field.Name);

                        EditorGUILayout.BeginVertical();

                        object value = field.GetValue( Target );
                        object changeValue = null;
                        if( field.FieldType == typeof( System.Int32 ) )
                        {
                            changeValue = CreateIntField( field.Name + ": int", ( int )value );
                        }
                        else if( field.FieldType == typeof( System.Int64 ) )
                        {
                            changeValue = CreateIntField( field.Name + ": int", ( int )value );
                        }
                        else if( field.FieldType == typeof( System.Byte ) )
                        {
                            changeValue = CreateIntField( field.Name + ": byte", ( int )value );
                        }
                        else if( field.FieldType == typeof( System.Single ) )
                        {
                            changeValue = CreateFloatField( field.Name + ": float", ( float )value );
                        }
                        else if( field.FieldType.BaseType == typeof( System.Array ) )
                        {
                            Object[] array = value as Object[];
                            if( null != array )
                            {
                                for( int i = 0; i < array.Length; i++ )
                                {
                                    array[i] = CreateObjectField( field.Name + "[" + i + "]", array[i] );
                                }
                                changeValue = array;
                            }
                            else
                                changeValue = value;
                        }
                        else if( field.FieldType == typeof( System.Boolean ) )
                        {
                            changeValue = CreateCheckBox( field.Name + ": bool", ( bool )value );
                        }
                        else if( field.FieldType == typeof( System.String ) )
                        {
                            changeValue = CreateStringField( field.Name + ": string", ( string )value );
                        }
                        else if( field.FieldType.BaseType == typeof( System.Enum ) )
                        {
                            changeValue = CreateEnumSelectable( field.Name + ": string", ( System.Enum )value );
                        }
                        else if( field.FieldType.BaseType == typeof( UnityEngine.Object ) )
                        {

                            changeValue = CreateObjectField( field.Name + ": " + field.FieldType, ( UnityEngine.Object )value );
                        }
                        else if( field.FieldType.BaseType == typeof( UnityEngine.Behaviour ) )
                        {

                            changeValue = CreateObjectField( field.Name + ": " + field.FieldType, ( UnityEngine.Behaviour )value );
                        }
                        else if( field.FieldType.BaseType == typeof( UnityEngine.MonoBehaviour ) )
                        {

                            changeValue = CreateObjectField( field.Name + ": " + field.FieldType, ( UnityEngine.Behaviour )value );
                        }
                        else if( field.FieldType.BaseType == typeof( System.Object ) )
                        {
                            changeValue = value;
                        }
                        else if( field.FieldType.BaseType == typeof( System.ValueType ) )
                        {
                            changeValue = value;
                        }
                        else
                        {
                            changeValue = CreateObjectField( field.Name + ": " + field.FieldType, ( Object )value );
                        }
                        if( GUI.changed && changeValue != value && uObject != null )
                        {
                            //							Debug.Log("gui change");
                            Undo.RecordObject( uObject, "Record ScrObject" );
                            try
                            {
                                field.SetValue( Target, changeValue );
                            }
                            catch( System.ArgumentException e )
                            {

                            }
                            catch( System.FieldAccessException e )
                            {

                            }

                            Repaint();
                        }

                        EditorGUILayout.EndVertical();

                    }
                }
            }

        }

        System.Type showType;

        string targetType;

        private Stack<object> storeTargets = new Stack<object>();

        public void OnChangeTarget()
        {

            if( Target != null )
            {
                storeTargets.Push( Target );

                targetType = Target.GetType().ToString();

                showType = Target.GetType();
            }


            this.Repaint();
        }
    }
}