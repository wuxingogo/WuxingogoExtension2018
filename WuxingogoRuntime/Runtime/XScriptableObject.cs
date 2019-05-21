using UnityEngine;
using System.Collections;
using System.Diagnostics;
using wuxingogo.Reflection;


namespace wuxingogo.Runtime
{

	public class XScriptableObject : ScriptableObject
	{
        public virtual void OnCtor()
        {

        }

		public bool hasFile = false;

        public static T Create<T>(Object parent) where T : XScriptableObject
        {
            T asset = CreateInstance<T>();
            asset.OnCtor();
			if (parent != null) {
				asset.hasFile = true;
				wuxingogo.Reflection.XReflectionUtils.AddObjectToObject (asset, parent);
			}

            return asset;
        }

		public static XScriptableObject Create( System.Type objectType, Object parent)
        {
            XScriptableObject asset = CreateInstance( objectType ) as XScriptableObject;
            asset.OnCtor();
			if (parent != null) {
				asset.hasFile = true;
				wuxingogo.Reflection.XReflectionUtils.AddObjectToObject (asset, parent);
			}
            return asset;
        }
		public void SaveInEditor()
		{
			wuxingogo.Reflection.XReflectionUtils.Save (this);
		}
		[X]
		public void DestroyObject()
		{
			DestroyImmediate (this, true);
		}

		[Conditional("UNITY_EDITOR")]
		// call in editor
		[X]
		public void SetDirty()
		{
			XReflectionUtils.SetDirty(this);
		}
    }
}
