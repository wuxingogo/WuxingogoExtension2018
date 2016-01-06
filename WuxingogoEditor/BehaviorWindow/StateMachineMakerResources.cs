using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;


namespace FsmEditor
{

	public class StateMachineMakerResources
	{
		public static Object[] LoadAssetBundleResources(Type type)
		{
			return LoadAssetBundleResources( "", type );
		}

		public static Object[] LoadAssetBundleResources(string path, Type type)
		{
			List<Object> assets = new List<Object>();
			if( assets.Count == 0 ) {
				GetAssets( "Assets/WuxingogoExtension/Editor/BehaviorWindow/" + path, assets );
			}
			return assets.Where( obj => obj.GetType() == type ).ToArray();
		}

		private static Object[] GetAssets(string path, List<Object> assets)
		{
			foreach( string dirPath in Directory.GetDirectories(path) ) {
				GetAssets( dirPath, assets );
			}

			foreach( string filePath in Directory.GetFiles(path) ) {
				var asset = AssetDatabase.LoadAssetAtPath( filePath, typeof( Object ) );
				if( asset && !assets.Contains( asset ) )
					assets.Add( asset );
			}
			return assets.ToArray();
		}
	}
}