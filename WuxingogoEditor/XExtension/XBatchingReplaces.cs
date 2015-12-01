using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Collections.Generic;
/**
 * [XBatchingReplaces 鏇挎崲psd鎴恜ng]
 * @type {(锟?锟⑦)}
 */
public class XBatchingReplaces : XBaseWindow 
{
	object selectedFolder = null;
	Texture2D selectedTexture = null;
	Texture2D replaceTexture = null;
	
	ParticleSystem[] particles = null;
	bool isShowFindParticles = false;
	
	string filterName = "";
	[MenuItem ("Wuxingogo/Wuxingogo XBatchingReplaces ")]
	static void init () {
		Init<XBatchingReplaces>();
	}

	public override void OnXGUI(){
		if( isShowFindParticles ){
			if( CreateSpaceButton("Find Material", 150) ){
			
				Object[] go = new Object[particles.Length];
				for (int pos = 0; pos < particles.Length; pos++) {
					//  TODO loop in particles.Length
					if(particles[pos].GetComponent<Renderer>().material != null){
						go[pos] = particles[pos].GetComponent<Renderer>().material.mainTexture;
						Debug.Log( AssetDatabase.GetAssetPath(particles[pos].GetComponent<Renderer>().sharedMaterial) );
					}
				}
				
				Selection.objects = go;
			}
		}

		filterName = CreateStringField("filter", filterName);

		if( CreateSpaceButton("filter") && filterName.Length > 0 ){
			List<Object> dds = new List<Object>();
			Object[] assets = Selection.objects;
			for( int pos = 0; pos < assets.Length; pos++ ){
				if( ContainerName(assets[pos], filterName) ){
					dds.Add(assets[pos]);
				}
			}
			Selection.objects = dds.ToArray();
			dds.Clear();
			dds = null;
		}
	}


	bool isDDSTexture(Object asset){
		string path = AssetDatabase.GetAssetPath(asset);
		// Debug.Log(path);
		if( path.Contains(".dds")){
			return true;
		}
		return false;
		
	}

	bool ContainerName(Object obj, string str){
		string path = AssetDatabase.GetAssetPath(obj);
		// Debug.Log(path);
		if( path.Contains(str) ){
			return true;
		}
		return false;
	}

	void OnSelectionChange(){
		//TODO List
		isShowFindParticles = false;
		if( Selection.objects.Length > 0 ){
			object selected = Selection.objects[0];
			
			if(selected == null) return;
			
			if(selected.GetType() == typeof(Texture2D)){
				selectedTexture = selected as Texture2D;
				string path = AssetDatabase.GetAssetPath(selectedTexture.GetInstanceID());
				string file = path.Replace(".psd",".png");
				replaceTexture = AssetDatabase.LoadAssetAtPath(file, typeof(Texture2D)) as Texture2D;
				
			}else if(selected.GetType() == typeof(GameObject)){
				GameObject selectedGO = (GameObject)selected;
				ComponentToParticle(XSelectionExtension.GetAllChildComponents(selectedGO.transform, typeof(ParticleSystem)));
				if( particles.Length > 0)
					isShowFindParticles = true;
//				selectedTexture = (Texture2D)particle.renderer.material.mainTexture;		
			}	

		}
		Repaint();
	}
	
	public void ComponentToParticle(Component[] selected){
		particles = new ParticleSystem[selected.Length];
		
		for (int pos = 0; pos < particles.Length; pos++) {
			//  TODO loop in particles.Length
			particles[pos] = selected[pos] as ParticleSystem;
		}
		
	}
	
	void CopyTextureImportProperties(Texture2D copied, Texture2D replace){

		string copyPath = AssetDatabase.GetAssetPath(copied);
		string replacePath = AssetDatabase.GetAssetPath(replace);
		TextureImporter copyImporter = TextureImporter.GetAtPath(copyPath) as TextureImporter;
		TextureImporter replaceyImporter = TextureImporter.GetAtPath(replacePath) as TextureImporter;
		Undo.RecordObject(copyImporter,"copyImporter");

		replaceyImporter.textureType = copyImporter.textureType;
		replaceyImporter.generateCubemap = copyImporter.generateCubemap;
		replaceyImporter.generateMipsInLinearSpace = copyImporter.generateMipsInLinearSpace;
		replaceyImporter.maxTextureSize = copyImporter.maxTextureSize;
		replaceyImporter.filterMode = copyImporter.filterMode;
		replaceyImporter.spritePackingTag = copyImporter.spritePackingTag;
		replaceyImporter.textureFormat = copyImporter.textureFormat;
		replaceyImporter.spritePivot = copyImporter.spritePivot;
		replaceyImporter.spriteImportMode = copyImporter.spriteImportMode;
		replaceyImporter.spritePixelsPerUnit = copyImporter.spritePixelsPerUnit;
		replaceyImporter.mipmapEnabled = copyImporter.mipmapEnabled;
		
		AssetDatabase.ImportAsset(replacePath);

	}
}