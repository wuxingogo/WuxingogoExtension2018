using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

namespace wuxingogo.tools
{
    public class GameObjectUtilities
    {
        private static string FullPath( GameObject go )
        {
            return go.transform.parent == null
                    ? go.name
                    : FullPath( go.transform.parent.gameObject ) + "/" + go.name;
        }
        public void Find()
        {
        }
    }
}
