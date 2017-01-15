

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
        public static void Create( XScriptableObject @object, Object parent )
        {
            XLogger.Log( "OnCreate in XScriptableEditor" );
            AssetDatabase.SetLabels( @object, new string[] { "XScriptObject" } );
			AssetsUtilites.AddObjectToAsset( @object, parent );
        }
		public static void Save(XScriptableObject @object)
		{
			if (@object.hasFile) {
				EditorUtility.SetDirty (@object);
			} else {
				var type = @object.GetType ();
				string path = AssetsUtilites.SaveFilePanel( type.Name, XEditorSetting.PluginPath, @object.name + ".asset", "asset", true );
				if( path == "" )
					return;

				path = FileUtil.GetProjectRelativePath( path ); 

				@object.hasFile = true;

				AssetDatabase.CreateAsset( @object, path );
				AssetDatabase.SaveAssets();
			}
		}
    }

}