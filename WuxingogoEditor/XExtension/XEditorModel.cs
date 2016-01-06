using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleModel {
	public List<Object> Assets = new List<Object>();
	public string ModelName = "";
	public string ParentName = "";
	public List<AssetBundleModel> Childs = new List<AssetBundleModel>();
	public void AddAssets(Object[] objects){
		Assets.AddRange(objects);
	}
	public void PackingSelf(){
		BuildPipeline.BuildAssetBundle(
			null, Assets.ToArray(),
			XCreateAssetBundle.Instance.BundlePath + ModelName + ".assetbundle" ,
			BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets
			| BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.Android);
		if(Childs.Count > 0){
			BuildPipeline.PushAssetDependencies();
			for(int pos = 0; pos < Childs.Count; pos++)
				Childs[pos].PackingSelf();
			BuildPipeline.PopAssetDependencies();
		}
	}
}
