namespace wuxingogo.Reflection
{
    using UnityEngine;
    using UnityEditor;
    using System.Reflection;
    using wuxingogo.Reflection;
    using wuxingogo.tools;


    /**
     * [XCommandTools 模仿LLDB 命令行]
     * @type {[┏(｀ー´)┛]}
     * 其实就是字符串分割 + 反射啦
     * 本来想可以设置变量也用这个的,可以到手机上来射ᕙ(⇀‸↼‵‵)ᕗ
     */
    using System.Collections.Generic;
    using System;
    using Object = UnityEngine.Object;


    public class XCommandTools : XBaseWindow
    {
        //	bool isEditorAssembly = false;
        string command = "~";

        bool isDirty = false;
        List<string> searchCollection = new List<string>();

        bool isClassIntent = false;
        bool isMethodIntent = false;
        bool isFieldIntent = false;

        Type type;
        object currValue = null;

        Object draggedObject = null;
        int intentIndex = 0;

        [MenuItem( "Wuxingogo/Reflection/Wuxingogo XCommandTools" )]
        static void init()
        {
            InitWindow<XCommandTools>();

        }

        public override void OnXGUI()
        {
            GUI.SetNextControlName( "CommandControl" );
            command = CreateStringField( "Input ur command : ", command );

            if( Event.current != null )
            {
                HandleInput( Event.current );
            }

            DisableFragment( true, () =>
            {
                CreateLabel( string.Format( "Type : {0}", type == null ? "None" : type.FullName ) );
            } );
            draggedObject = CreateObjectField( draggedObject );

            for( int pos = 0; pos < searchCollection.Count; pos++ )
            {
                GUI.SetNextControlName( searchCollection[pos] );

                GUIStyle style = XStyles.GetInstance().button;

                DoButton( searchCollection[pos], () =>
                {
                    OnSelectionButton();
                    return;
                }, style );
            }
        }



        static StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;

        public static List<string> TryGetClass( string className )
        {
            List<string> result = new List<string>();
            if( string.IsNullOrEmpty( className ) )
                return result;

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for( int pos = 0; pos < assemblies.Length; pos++ )
            {
                //  TODO loop in Length
                var types = assemblies[pos].GetTypes();
                for( int idx = 0; idx < types.Length; idx++ )
                {
                    //  TODO loop in types.Length
                    if( types[idx].Name.Contains( className ) || className.Contains( types[idx].Name ) )
                    {
                        string entry = types[idx].Name;
                        entry.Replace( "<", "" )
                        .Replace( ">", "" )
                        .Replace( "\t", "" )
                        .Replace( "\n", "" )
                        .Replace( "(", "" )
                        .Replace( ")", "" )
                        .Replace( "$", "" );
                        result.Add( types[idx].Name );
                    }

                }
            }
            return result;
        }

        public List<string> TryGetMember( string memberName, bool isStatic )
        {
            string[] array = memberName.Split( '(', ')' );

            List<string> result = new List<string>();
            if( type == null )
                return result;
            MemberInfo[] memberCollection = type.MemberMatch( array[0], isStatic );
            for( int pos = 0; pos < memberCollection.Length; pos++ )
            {
                //  TODO loop in memberCollection.Length
                result.Add( memberCollection[pos].Name );
            }
            return result;
        }

        void HandleInput( Event e )
        {
            if( Event.current.isKey )
            {
                Listen();
                string[] paras = command.Split( '.' );


                switch( paras.Length )
                {
                    case 1:
                        isClassIntent = false;
                        isMethodIntent = false;
                        isFieldIntent = false;
                        searchCollection = TryGetClass( paras[0] );
                        break;
                    case 2:
                        if( !isClassIntent )
                        {
                            type = XReflectionUtils.TryGetClass( paras[0] );
                            isClassIntent = true;
                            isMethodIntent = false;
                            isFieldIntent = false;
                        }
                        searchCollection = TryGetMember( paras[1], true );
                        break;
                    case 3:
                        if( !isMethodIntent )
                        {
                            //						object instance = TryInvokeGlobalFunction( paras[1] );
                            //						type = instance.GetType();
                            //						isMethodIntent = true;
                        }
                        searchCollection = TryGetMember( paras[2], false );
                        break;
                    default:
                        break;
                }

            }
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            if( Event.current.type == EventType.DragPerform )
            {

                DragAndDrop.AcceptDrag();
                foreach( var draggedObject in DragAndDrop.objectReferences )
                {
                    this.draggedObject = draggedObject;
                }
            }

            Repaint();
        }

        void EmptyCommand()
        {

            command = "";
            type = null;
            currValue = null;

        }

        void ExcuteCommand()
        {
            if( command.Contains( ";" ) )
            {
                command = command.Replace( ";", "" );
                EditorPrefs.SetString( "command_string", command );
                MatchPara();
                EmptyCommand();
            }
            else
            {
                OnSelectionButton();
            }

        }

        void OnSelectionButton()
        {
            int last = command.LastIndexOf( "." );
            command = command.CutString( 0, last == -1 ? 0 : last + 1 );
            command += searchCollection[intentIndex];
            EditorGUI.FocusTextInControl( "CommandControl" );

        }

        object[] paserPara( string para )
        {
            List<object> result = new List<object>();
            if( para != null )
            {
                string[] temp = para.Split( ',' );
                for( int i = 0; i < temp.Length; i++ )
                {

                }
            }
            return null;
        }

        void MatchPara()
        {
            var functions = command.RegexCutString( "(", ")" );
            for( int i = 0; i < functions.Length; i++ )
            {
                var objs = paserPara( functions[i] );

            }
            List<object[]> paras = new List<object[]>();

            var clear = command.RegexCutStringReverse( "(", ")" );

            string[] commandPara = clear.Split( '.' );
            if( commandPara.Length > 0 && type == null && currValue == null )
                type = XReflectionUtils.TryGetClass( commandPara[0] );

            currValue = draggedObject;

            int allLenght = commandPara.Length;
            int funCount = functions.Length;

            int startIndex = 1;
            if( currValue != null )
                startIndex = 0;
            for( int i = startIndex; i < startIndex + funCount; i++ )
            {
                //  TODO loop in funCount
                if( currValue == null )
                {
                    currValue = type.TryInvokeGlobalMethod( commandPara[i] );
                }
                else
                {
                    currValue = currValue.GetType().TryInvokeMethod( currValue, commandPara[i] );
                }
            }
            int fieldCount = commandPara.Length - funCount;

            int fieldIndex = 1;
            if( currValue != null )
                fieldIndex = 0;
            for( int pos = fieldIndex; pos < fieldCount; pos++ )
            {
                //  TODO loop in Length
                if( currValue == null )
                {
                    currValue = type.TrySearchGlobalMemberValue( commandPara[pos + startIndex] );
                }
                else
                {
                    currValue = currValue.TryGetFieldValue( commandPara[pos + startIndex] );
                }
            }
            Logger.Log( "currValue is : " + currValue.ToString() );
        }

        public bool Listen()
        {
            bool c = false;
            switch( Event.current.keyCode )
            {
                case KeyCode.Return:
                    ExcuteCommand();
                    break;
                case KeyCode.UpArrow:
                    command = EditorPrefs.GetString( "command_string", "" );
                    Repaint();
                    c = true;
                    break;
                case KeyCode.CapsLock:
                    break;
                case KeyCode.Tab:
                    break;
                case KeyCode.None:
                    break;
                case KeyCode.Backspace:
                    //			command.Substring(0, command.Length - 2);
                    //			Repaint();
                    break;

                default:
                    //			command += Event.current.keyCode;
                    //			Repaint();
                    //		    c = true;
                    break;
            }
            isDirty = true;
            Event.current.type = EventType.Layout;
            return c;
        }
    }
}