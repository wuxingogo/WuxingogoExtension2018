using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

namespace wuxingogo.tools
{
    public class GameObjectUtilities
    {
		public static string DefaultNewGameObjectName = "BoxCollider";
        private static string FullPath( GameObject go )
        {
            return go.transform.parent == null
                    ? go.name
                    : FullPath( go.transform.parent.gameObject ) + "/" + go.name;
        }

		public static BoxCollider NewBoxCollider(Vector3 size, bool isTrigger)
		{
			GameObject newGo = new GameObject(DefaultNewGameObjectName);
			var boxCollider = newGo.AddComponent<BoxCollider>();
			boxCollider.size = size;
			boxCollider.isTrigger = isTrigger;
			return boxCollider;
		}

		public static BoxCollider AddBoxCollider(GameObject newGo, Vector3 size, bool isTrigger)
		{
			var boxCollider = newGo.AddComponent<BoxCollider>();
			boxCollider.size = size;
			boxCollider.isTrigger = isTrigger;
			return boxCollider;
		}
        public void Find()
        {
        }
    }
}
