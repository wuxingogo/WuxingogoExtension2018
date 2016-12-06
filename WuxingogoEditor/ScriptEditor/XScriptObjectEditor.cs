

namespace wuxingogo.Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;
    using wuxingogo.Runtime;
    using System.Reflection;
    using System.Collections.Generic;
    using System;
    using Object = UnityEngine.Object;
    [CustomEditor(typeof(XScriptableObject), true )]
    public class XScriptObjectEditor : XMonoBehaviourEditor
    {
        public static void Create( XScriptableObject @object, Object parent = null )
        {
            XLogger.Log( "OnCreate in XScriptableEditor" );
            AssetDatabase.SetLabels( @object, new string[] { "XScriptObject" } );
            AssetDatabase.AddObjectToAsset( @object, parent );
        }
    }

}