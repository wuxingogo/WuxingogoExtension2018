using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class QuickCreatePrefabs : XBaseWindow {

	
	GameObject myPrefab = null;
	int copyNum = 4;
	float radio = 20;
	List<GameObject> myPrefablist = new List<GameObject>();
	[MenuItem ("Wuxingogo/Quick Create Prefabs Window")]
	static void init () {
		
		QuickCreatePrefabs window = (QuickCreatePrefabs)QuickCreatePrefabs.GetWindow (typeof (QuickCreatePrefabs));
	}
	public override void OnXGUI(){
		
		CreateSpaceBox();
		
		myPrefab = ( GameObject )CreateObjectField( "Instantiate Prefab", myPrefab );
		
		radio = CreateFloatField("Radio",radio);
		
		copyNum = CreateIntField("Copy Num", copyNum);
		if(GUI.changed){
			changeGameObjecjByRadio();
		}
		
		if(CreateSpaceButton( "Create Prefabs" ) && null != myPrefab ){
			myPrefablist.Clear();
			Debug.Log( myPrefab.name );
			
			for( int i = 0; i < copyNum; i++ ){
				GameObject obj = (Instantiate(myPrefab) as GameObject);
				myPrefablist.Add(obj);
				
			}
			Selection.objects = myPrefablist.ToArray();
		}
		
		CreateSpaceBox();
		
		if(CreateSpaceButton( "Clean Array") ){
			myPrefablist.Clear();
		}
		
		CreateSpaceBox();
	}
	

	
	public void changeGameObjecjByRadio(){
		if(myPrefablist.Count > 0 ){
			int count = myPrefablist.Count;
			float angle = 360.0f / count ;
			for(int i = 0; i < count ; i++ ){
				float Eangle = (i + 1) * angle;
//				float y = radio * Mathf.Cos(Eangle);
//				float x = Mathf.Sqrt( radio * radio - y * y );
				
				
				float x = radio * Mathf.Cos(Mathf.Deg2Rad*Eangle);
				float y = radio * Mathf.Sin(Mathf.Deg2Rad*Eangle);
//				if(Eangle >= 180){
//					
//					y = radio * Mathf.Sin(Eangle);
//					x = Mathf.Sqrt( radio * radio - y * y ) ;
//				}
					
				
				myPrefablist[i].transform.position = new Vector3( x, y, 1);
			}
		}else{
			for( int i = 0; i < Selection.gameObjects.Length; i++ ){
				myPrefablist.Add(Selection.gameObjects[i]);
			}
			
		}
	}
	
	
}
