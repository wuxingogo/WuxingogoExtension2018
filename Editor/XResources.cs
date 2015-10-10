using UnityEngine;
using UnityEditor;
using System.Collections;

public class XResources {

	public static Texture LogoTexture = EditorGUIUtility.LoadRequired("Texture/wuxingogo.psd") as Texture;
    public static XStyles Sty = EditorGUIUtility.LoadRequired( "Texture/XStyles.asset" ) as XStyles;

    public static void ReimportAll()
    {
        LogoTexture = EditorGUIUtility.LoadRequired( "Texture/wuxingogo.psd" ) as Texture;
        Sty = EditorGUIUtility.LoadRequired( "Texture/XStyles.asset" ) as XStyles;
    }
}
