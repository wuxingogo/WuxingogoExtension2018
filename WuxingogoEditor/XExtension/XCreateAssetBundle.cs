using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

[System.Obsolete]
public class XCreateAssetBundle : XBaseWindow 
{
	AssetBundle currBundle;
	Object[] currentBundleObjects = null;
	bool LoadingConfig = false;
	string _bundlepath = "";
	WWW asset;

	List<AssetBundleModel> allAssets = new List<AssetBundleModel>();
	Dictionary<string, AssetBundleModel> sortAssets = new Dictionary<string, AssetBundleModel>();
	
	Object selectedObject = null;
	public static XCreateAssetBundle Instance{
		get{
			return (XCreateAssetBundle)EditorWindow.GetWindow (typeof (XCreateAssetBundle ) );
		}
	}
	public string BundlePath{
		get;
		private set;
	}
	
	[MenuItem ("Wuxingogo/Wuxingogo XCreateAssetBundle ")]
	static void init () {
		InitWindow<XCreateAssetBundle>();
	}
	
	

	public override void OnXGUI(){
		//TODO List
		if(Selection.objects != null && Selection.objects.Length > 0){
			selectedObject = Selection.objects[0];
		}
		
		if(CreateSpaceButton("Packing Selected Objects")){
			Object[] objects = Selection.objects;
			
			string path = EditorUtility.SaveFilePanel("Create A Bundle", AssetDatabase.GetAssetPath(objects[0]), "newbundle.assetbundle", "assetbundle");
			if (path == "")
				return;
				
			
			CreateXMLWithDependencies(objects, path);
			
			BuildPipeline.BuildAssetBundle(
				null, objects,
				path ,
				BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle
				| BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
		}
		
		if(CreateSpaceButton("GetObject")){
			string path = EditorUtility.OpenFilePanel("Open A Bundle", Application.streamingAssetsPath, "" );
			if (path == "")
				return;
			_bundlepath = "file://" + path;
			LoadingConfig = true;
			asset = null;
//			AssetDatabase.Refresh();
			if(currBundle != null){
				currBundle.Unload(true);
			}
		}
		
		if(CreateSpaceButton("Clean Cache")){
			currentBundleObjects = null;
			Caching.CleanCache();
		}
		
		if( LoadingConfig ){
			Logger.Log("start loading");
			if( null == asset ) 
				asset = new WWW(_bundlepath);
			LoadingConfig = false;
		}
//		Logger.Log(string.Format("asset == null is {0}" , asset == null));
		if(asset != null ){
//			Logger.Log("asset.isDone is " + asset.isDone);
//			if(asset.isDone){
   
			Logger.Log("end loading");
			currBundle = asset.assetBundle;
			if(currBundle == null){
				CreateNotification("Selected the asset bundle 's format is error.");
				LoadingConfig = false;
				asset = null;
				return;
			}
			#if UNITY_5_0
			currentBundleObjects = currBundle.LoadAllAssets();
			#endif
			#if UNITY_4_6
			currentBundleObjects = currBundle.LoadAll();
			#endif
			LoadingConfig = false;
			asset = null;
//			}
		}
		
		
		
		if( null != currentBundleObjects ){
			for (int pos = 0; pos < currentBundleObjects.Length; pos++) {
				CreateObjectField(currentBundleObjects[pos].GetType().ToString(),currentBundleObjects[pos]);
			}
		}
		
		if(CreateSpaceButton("Add A AssetBundle")){
			allAssets.Add(new AssetBundleModel());
		}
		if(CreateSpaceButton("Clean All AssetBundle")){
			allAssets.Clear();
		}
		if(CreateSpaceButton("Collect") && allAssets.Count > 0){
			List<AssetBundleModel> AllChilds = new List<AssetBundleModel>();
			sortAssets.Clear();
			
			BundlePath = EditorUtility.SaveFolderPanel("Save Bundles", Application.streamingAssetsPath, "" );
			if(BundlePath == null){
				return;
			}
			BundlePath += "/";
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.CreateElement("root");
			for(int pos = 0; pos < allAssets.Count; pos++){
				if(allAssets[pos].ParentName.Equals("")){
					sortAssets.Add(allAssets[pos].ModelName, allAssets[pos]);	
					XmlElement child = xmlDoc.CreateElement(allAssets[pos].ModelName);
					root.AppendChild(child);
				}else{
					AllChilds.Add(allAssets[pos]);
				}
//				allAssets.Remove(allAssets[pos]);
			}
			for(int pos = 0; pos < AllChilds.Count; pos++){
				sortAssets[AllChilds[pos].ParentName].Childs.Add(AllChilds[pos]);
				XmlElement child = xmlDoc.CreateElement(AllChilds[pos].ModelName);
				root.SelectSingleNode(AllChilds[pos].ParentName).AppendChild(child);
//				allAssets.Remove(allAssets[pos]);
			}
			xmlDoc.AppendChild(root);
			xmlDoc.Save(BundlePath + "bundle.xml");
			foreach(var bundle in sortAssets){
				bundle.Value.PackingSelf();
			}
//			allAssets.Clear();
			AssetDatabase.Refresh();
			CreateNotification("Create asset bundle success!");
		}
		
		for(int pos = 0; pos < allAssets.Count; pos++){
			AssetBundleModel Item = allAssets[pos];
			BeginHorizontal();
			Item.ModelName = CreateStringField("Name", allAssets[pos].ModelName);
			Item.ParentName = CreateStringField("Dependencies", allAssets[pos].ParentName);
			if( CreateSpaceButton("Add Asset") && null != selectedObject ){
				Item.Assets.AddRange(Selection.objects);			
			}
			if( CreateSpaceButton("Remove Bundle") ){
				allAssets.RemoveAt(pos);
			}
			EndHorizontal();
			CreateSpaceBox();
			
			for(int idx = 0; idx < Item.Assets.Count; idx++){
				BeginHorizontal();
				CreateObjectField("child_" + idx, Item.Assets[idx]);
				if(CreateSpaceButton("Remove")){
					Item.Assets.RemoveAt(idx);
				}
				EndHorizontal();
			}
			
			CreateSpaceBox();
		}
	}

	void CreateXMLWithDependencies(Object[] objects, string path){
		string filepathxml = path.Substring(0, path.IndexOf(".") + 1);
		filepathxml += "xml";
		
		if (!File.Exists(filepathxml)) {
			File.Delete(filepathxml);
		}
		
		Object[] resources = EditorUtility.CollectDependencies( objects );
		
		XmlDocument xmlDoc = new XmlDocument();
		XmlElement root = xmlDoc.CreateElement ("bundle");
		
		for (int pos = 0; pos < resources.Length; pos++) {
			XmlElement child = xmlDoc.CreateElement ("element_" + pos);
			child.SetAttribute("name", resources[pos].name );
			child.SetAttribute("type", resources[pos].GetType().ToString() );
			root.AppendChild(child);
		}
		xmlDoc.AppendChild(root);
		xmlDoc.Save (filepathxml);
	}
	
	
	
	

	
}