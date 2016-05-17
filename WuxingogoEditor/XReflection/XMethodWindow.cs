
namespace wuxingogo.Reflection
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections;
    using System.Reflection;
    using System.Collections.Generic;
    using System;
    using Object = UnityEngine.Object;

    public class XMethodWindow : XBaseWindow
    {

        public delegate object MethodInvoke( MethodInfo methodInfo, object[] paras );

        public static XMethodWindow Init()
        {
            return InitWindow<XMethodWindow>();
        }

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

        private Object uObject = null;

        private Stack<object> storeTargets = new Stack<object>();

        string targetType = "";

        System.Type showType;

        private Dictionary<MethodInfo, object[]> parameters = new Dictionary<MethodInfo, object[]>();

        private bool isDebugBreak = false;

        public event MethodInvoke MethodInvokeDelegate;

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
                if( storeTargets.Count > 0 )
                    DoButton( "Back To Last Object", BackToLastObject );

                DoButton( "Open Generate Code Window", OpenInGenerateCodeWindow );

                isDebugBreak = CreateCheckBox( "DebugBreak", isDebugBreak );

                BeginHorizontal();
                CreateLabel( "Filter Type : " + showType );
                if( showType.BaseType != null )
                    DoButton( showType.BaseType.ToString(), ConvertToBase );
                EndHorizontal();

                MethodInfo[] methods = _target.GetType().GetMethods( BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public );



                foreach( MethodInfo method in methods )
                {
                    if( method.DeclaringType == showType )
                    {

                        BeginHorizontal();
                        CreateLabel( method.ReturnType.ToString() );
                        CreateLabel( method.Name );
                        EndHorizontal();


                        ParameterInfo[] paras = method.GetParameters();
                        if( !parameters.ContainsKey( method ) )
                        {
                            object[] arr = new object[paras.Length];
                            parameters.Add( method, arr );
                        }

                        object[] myParameters = parameters[method];

                        BeginVertical();
                        #region Show all method Parameter Info
                        for( int pos = 0; pos < paras.Length; pos++ )
                        {
                            myParameters[pos] = GetTypeGUI( myParameters[pos], paras[pos].ParameterType );

                        }

                        if( CreateSpaceButton( "Invoke" ) )
                        {
                            MethodInvokeDelegate( method, myParameters );
                        }

                        #endregion
                        EndVertical();
                    }

                }
            }
        }

        object InvokeMethod( MethodInfo methodInfo, params object[] paras )
        {

            object result = methodInfo.Invoke( _target, paras );
            Debug.Log( result ?? "Void Method" );
            if( isDebugBreak )
                Debug.Break();
            return result;
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

            MethodInvokeDelegate += InvokeMethod;

        }

        object GetTypeGUI( object t, Type type )
        {
            string strType = type.ToString();

            BeginHorizontal();
            CreateLabel( strType );

            if( type == typeof( Int32 ) )
            {
                t = CreateIntField( Convert.ToInt32( t ) );
            }
            else if( type == typeof( String ) )
            {
                t = CreateStringField( ( string )t );
            }
            else if( type == typeof( Single ) )
            {
                t = CreateFloatField( Convert.ToSingle( t ) );
            }
            else if( type == typeof( Boolean ) )
            {
                t = CreateCheckBox( Convert.ToBoolean( t ) );
            }
            else if( type.BaseType == typeof( Enum ) )
            {
                t = Enum.ToObject( type, 0 );
            }
            else if( type.IsSubclassOf( typeof( Object ) ) )
            {
                t = CreateObjectField( ( Object )t, type );
            }
            else
            {
                CreateLabel( " are not support" );
            }
            EndHorizontal();
            return t;

        }

        void OpenInGenerateCodeWindow()
        {

            wuxingogo.Code.CodeGenerateEditor codeEditor = InitWindow<wuxingogo.Code.CodeGenerateEditor>();
            wuxingogo.Code.XCodeObject codeObject = codeEditor.GenerateNewCode();
            wuxingogo.Code.XCodeClass classUnit = codeObject.classUnit;
            classUnit.name = Target.GetType().Name;
            var type = Target.GetType();
            foreach( var item in type.GetMethods() )
            {
                classUnit.methods.Add( new wuxingogo.Code.XCodeMethod()
                {
                    name = item.Name,
                    type = new wuxingogo.Code.XCodeType( item.ReturnType ),
                } );
            }
            foreach( var item in type.GetProperties() )
            {
                classUnit.properties.Add( new wuxingogo.Code.XCodeProperty()
                {
                    name = item.Name,
                    type = new wuxingogo.Code.XCodeType( item.PropertyType )
                } );
            }
            foreach( var item in type.GetFields() )
            {
                classUnit.fields.Add( new wuxingogo.Code.XCodeField()
                {
                    name = item.Name,
                    type = new wuxingogo.Code.XCodeType( item.FieldType )
                } );
            }




        }

        void ConvertToBase()
        {
            showType = showType.BaseType;
            parameters.Clear();
        }

        void BackToLastObject()
        {

            Target = storeTargets.Pop();
            parameters.Clear();
        }

        void OnSelectionChange()
        {
            //TODO List

        }

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