namespace wuxingogo.Editor
{
    using UnityEditor;
    using UnityEngine;
    using System;
    using System.Reflection;

    public class ToolsSupport {
 
        public static bool Hidden {
            get {
                Type type = typeof (Tools);
                FieldInfo field = type.GetField ("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
                return ((bool) field.GetValue (null));
            }
            set {
                Type type = typeof (Tools);
                FieldInfo field = type.GetField ("s_Hidden", BindingFlags.NonPublic | BindingFlags.Static);
                field.SetValue (null, value);
            }
        }
    }

}