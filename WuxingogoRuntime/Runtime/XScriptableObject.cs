using UnityEngine;
using System.Collections;


namespace wuxingogo.Runtime
{

	public class XScriptableObject : ScriptableObject
	{
        public virtual void OnCtor()
        {

        }

        public static T Create<T>(Object parent = null) where T : XScriptableObject
        {
            T asset = XScriptableObject.CreateInstance<T>();
            asset.OnCtor();
            if( Application.isEditor )
            {
                if( parent != null )
                {
                    System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.XScriptObjectEditor" );
                    var method = type.GetMethod( "Create" );
                    method.Invoke( null, new object[] { asset, parent } );
                }
            }
            return asset;
        }

        public static ScriptableObject Create( System.Type objectType, Object parent = null )
        {
            XScriptableObject asset = XScriptableObject.CreateInstance( objectType ) as XScriptableObject;
            asset.OnCtor();
            if( Application.isEditor )
            {
                if( parent != null )
                {
                    System.Type type = Reflection.XReflectionUtils.GetEditorType( "wuxingogo.Editor.XScriptObjectEditor" );
                    var method = type.GetMethod( "Create" );
                    method.Invoke( null, new object[] { asset, parent } );
                }
            }
            return asset;
        }
    }
}
