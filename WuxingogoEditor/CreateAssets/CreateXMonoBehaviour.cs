using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using wuxingogo.Code;
namespace CreateAssets
{
    public class CreateXMonoBehaviour : CreateUnityScript
    {

       [MenuItem("Assets/Create/Wuxingogo/XMonoBehaviour", false, 100)]
        public static void CreateBehaviour()
        {
            string path = GetPath("Create XMonoBehaviour Class", "NewXMonoBehaviour.cs");
            if (path == "")
                return;
            string dictionary = path.Substring(0, path.LastIndexOf('/'));


            string[] strArray = path.Split('/');
            string suffix = strArray[strArray.Length - 1];
            int suffixIndex = suffix.IndexOf('.');
            string fileName = suffix.Substring(0, suffixIndex);
            path = FileUtil.GetProjectRelativePath(path);

            string assetPath = XEditorSetting.TemplatesPath + "/" + "NewXMonoBehaviour.asset";
            assetPath = FileUtil.GetProjectRelativePath(assetPath);
            XCodeObject co = AssetDatabase.LoadAssetAtPath<XCodeObject>(assetPath);
            co.className = fileName;
            co.Compile(dictionary + "/" + suffix);
        }



    }
    
}
