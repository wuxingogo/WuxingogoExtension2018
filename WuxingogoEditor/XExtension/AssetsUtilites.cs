using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace wuxingogo.Editor
{
	public class AssetsUtilites
	{
		public static string SaveFilePanel(string title, string directory, string defaultName, string extension, bool needSaveFilePath)
		{
			string oldPath = "";
			string savePathKey = "XAssetUtilites_Path_" + title;
			if (needSaveFilePath) {
				oldPath = EditorPrefs.GetString (savePathKey, directory);
			}
			string path = EditorUtility.SaveFilePanel( title, oldPath ?? directory, defaultName, extension );

			if (needSaveFilePath) {
				EditorPrefs.SetString (savePathKey, path);
			}

			return path;
		}
		
		public static string GetPrefabType(Object @object)
		{
			return PrefabUtility.GetPrefabType(@object).ToString();
		}
		
		public static Object GetPrefabObject(Object @object)
		{
			return PrefabUtility.GetPrefabObject(@object);
		}
		public static void AddObjectToAsset(Object @object, Object parent)
		{
			AssetDatabase.AddObjectToAsset( @object, parent );
		}

		public static void SetDirty(Object @object)
		{
			EditorUtility.SetDirty(@object);
			AssetDatabase.Refresh();
		}
	
		public static T[] FindAssetsByType<T>() where T : Object
		{

			var totalAssetGuid = AssetDatabase.FindAssets( string.Format( "t:{0}", typeof( T ).Name ) );
			List<T> totalAssets = new List<T>();
			for( int i = 0; i < totalAssetGuid.Length; i++ )
			{
				var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
				var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
				for( int j = 0; j < allAssets.Length; j++ )
				{
					if( allAssets[j] is T && !totalAssets.Contains(allAssets[j] as T))
						totalAssets.Add( allAssets[j] as T );
				}
			}
			return totalAssets.ToArray();
		}

		public static T[] FindAssetsByTags<T>( string tag ) where T : Object
		{
			var totalAssetGuid = AssetDatabase.FindAssets( string.Format( "l:{0}", tag ) );
			List<T> totalAssets = new List<T>();
			for( int i = 0; i < totalAssetGuid.Length; i++ )
			{
				var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
				var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
				for( int j = 0; j < allAssets.Length; j++ )
				{
					if( allAssets[j] is T && !totalAssets.Contains( allAssets[j] as T ) )
						totalAssets.Add( allAssets[j] as T );
				}
			}
			return totalAssets.ToArray();
		}

		public static T[] FindGameObjectsByComponents<T>(params string[] searchInFolders) where T : Component
		{
			var totalAssetGuid = AssetDatabase.FindAssets( "t:GameObject", searchInFolders );
			List<T> totalAssets = new List<T>();
			for( int i = 0; i < totalAssetGuid.Length; i++ )
			{
				var path = AssetDatabase.GUIDToAssetPath( totalAssetGuid[i] );
				var allAssets = AssetDatabase.LoadAllAssetsAtPath( path );
				for( int j = 0; j < allAssets.Length; j++ )
				{
					if( allAssets[j] is GameObject )
					{
						var tGameObject = allAssets[j] as GameObject;
						var t = tGameObject.GetComponent<T>();
						if( t != null )
							totalAssets.Add( t );
					}
				}
			}
			return totalAssets.ToArray();
		}
	}
}

