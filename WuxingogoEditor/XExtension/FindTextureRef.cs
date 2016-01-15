using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
public class FindTextureRef : Editor {


	static List<Object> selection = new List<Object>();
	static List<Texture> SelectedAsset = new List<Texture>();
	static List<GameObject> assetObjs = new List<GameObject>();
	static List<GameObject> allChilds = new List<GameObject>();
	static List<Texture> usedAsset = new List<Texture>();
	
	[MenuItem("Wuxingogo/Find Texture All GameObjects")]
	static void Execute()
	{
	
		SelectedAsset.Clear();
		selection.Clear();
		assetObjs.Clear();
		usedAsset.Clear();
		allChilds.Clear();
		
//		SelectedAsset = Selection.GetFiltered(typeof(Sprite), SelectionMode.Assets) as Sprite[];
		
		Object[] objs = Selection.objects ;
		foreach( Object o in objs){
			if(o as Texture != null )
			{
				SelectedAsset.Add( o as Texture );
				assetObjs.Add(o as GameObject);
//				selection.Add(o);
			}	
		}
		GetAllChilds(GameObject.Find("UI"));

		GetChildsImg(allChilds);
		
		GetUsedAsset(usedAsset);
		
		Selection.objects = selection.ToArray();
		
		
		
		//		Object[] depndencies = EditorUtility.CollectDependencies(objs);
		//		Debug.Log(depndencies.Length);
		
	}
	private static void GetChildsImg(List<GameObject> allObjs){
	
		for(int i = 0; i < allObjs.Count; i++){
			Transform trans = allObjs[i].transform;
			if(trans.GetComponent<Image>() != null){
				//					Debug.Log(trans.GetComponent<Image>().sprite.ToString());
				Texture tex = null;
				if(trans.GetComponent<Image>().sprite == null)
					continue;
				tex = trans.GetComponent<Image>().sprite.texture;
				//					Debug.Log(trans.GetComponent<Image>().sprite.texture.ToString());
				foreach( Texture assetTex in SelectedAsset ){
					if(tex.GetHashCode().Equals(assetTex.GetHashCode())){
						
						selection.Add(trans.gameObject);
						
						if(usedAsset.IndexOf(assetTex) == -1){
							Debug.Log("add used asset");
							usedAsset.Add(assetTex);
						}
					}
				}
			}
		}
	
	}
	
	
	private static void GetAllChilds(GameObject transformForSearch) {
//		foreach (Transform trans in transformForSearch.transform) {
//			if (trans.name.StartsWith("wanfa")) {
//				generateWanfaConf(trans);
//			}
//			else {
//				GetAllChilds(trans.gameObject);
//				if (Resources.Load("Scene_shanghai/" + trans.name) != null || 
//					Resources.Load("Scene_ymx/" + trans.name) != null || 
//					Resources.Load("Scene_mt/" + trans.name) != null ||
//				    Resources.Load("SH_RoofProp_Single/" + trans.name)) {
//					displayObjects.Add(trans.gameObject);
//				}
//			}
//		}
		
		foreach (Transform trans in transformForSearch.transform) {
			allChilds.Add(trans.gameObject);
			
			if(trans.childCount > 0){
				GetAllChilds(trans.gameObject);
			}	
		}
//		Debug.Log( "selection size is :" + selection.Count);
//		Object[] gos = new Object[selection.Count + SelectedAsset .Count] ; 
//		selection.AddRange(assetObjs);
//		if(selection.Count > 0)
//		assetObjs.CopyTo(gos, selection.Count - 1);
		
	}
	
	
	private static void GetUsedAsset(List<Texture> usedAssets){
		for(int i = 0 ; i < usedAssets.Count; i++ ){
		
			Object obj = usedAssets[i] as Object;
			
			selection.Add(obj);
		}
	}
		

}
