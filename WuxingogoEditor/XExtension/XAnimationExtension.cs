using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
#if TMP
using TMPro;
#endif
public class XAnimationExtension : XBaseWindow 
{

	List<AnimationClip> anims = new List<AnimationClip>();
	[MenuItem ("Wuxingogo/Wuxingogo XAnimationExtension")]
	static void init () {
		InitWindow<XAnimationExtension>();
	}



	public override void OnXGUI(){
		//TODO List
		CreateSpaceBox();

		
		for( int pos = 0; pos < anims.Count; pos++ ){
			EditorCurveBinding[] edicurves = AnimationUtility.GetCurveBindings(anims[pos]);
			
			// AnimationEvent[] events = AnimationUtility.GetAnimationEvents(anims[pos]);
			for( int foot = 0; foot < edicurves.Length; foot++ ){
				AnimationCurve curve = AnimationUtility.GetEditorCurve(anims[pos], edicurves[foot]);
				EditorGUILayout.BeginHorizontal();
				CreateLabel(edicurves[foot].path);
				
				CreateLabel(edicurves[foot].type.ToString());
				if(CreateSpaceButton("change")){
					Undo.RecordObject(anims[pos], "change curves");
#if TMP
					edicurves[foot].type = typeof(TextMeshProUGUI);
					string path = edicurves[foot].path;
					// change ugui text color to textmeshpro 's font color.
					anims[pos].SetCurve(path,edicurves[foot].type,"m_fontColor.a",curve);
					
//					AnimationUtility.set
//					AnimationUtility.SetEditorCurve(anims[pos], edicurves[pos], curve);
//					AssetDatabase.StopAssetEditing();
//					EditorUtility.ClearProgressBar();
//					EditorUtility.SetDirty(anims[0]);
					this.Repaint();
#endif
				}

				if( CreateSpaceButton("copy") ){
					ShowNotification(new GUIContent( "selected key has "+ curve.keys.Length + " frames"));
					// anims[pos].
					AnimationUtility.SetObjectReferenceCurve(anims[pos],edicurves[0],null);
				}
				if( CreateSpaceButton("paste") ){
					Undo.RecordObject(anims[pos], "clear curves");
					anims[pos].ClearCurves();

				}
				
				EditorGUILayout.EndHorizontal();
			}
		}

		if(CreateSpaceButton("Copy")){
//			AnimationUtility.GetAnimatedObject(Selection.objects[0],

			for( int i = 0; i < anims.Count; i++){
				ShowNotification(new GUIContent("wuxingogo"));

				EditorCurveBinding[] edicurves = AnimationUtility.GetCurveBindings(anims[i]);
				AnimationEvent[] events = AnimationUtility.GetAnimationEvents(anims[i]);
				AnimationCurve curves = AnimationUtility.GetEditorCurve(anims[i], edicurves[i]);
//				AnimationUtility.GetObjectReferenceCurveBindings (anims[i])
				AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(anims[i]);
				 for( int pos = 0; pos < edicurves.Length; pos++){
				 	Debug.Log("pos is : " + edicurves[i].propertyName);
					
				 }
				// AnimationUtility.
				foreach (var binding in AnimationUtility.GetObjectReferenceCurveBindings (anims[i]))
				{
					ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve (anims[i], edicurves[i]);
					Debug.Log(binding.path + "/" + binding.propertyName + ", Keys: " + keyframes.Length);
				}
				
			}		
		}
	}
	
	void OnInspectorUpdate(){
		Repaint();
	}

	void OnSelectionChange(){
		anims.Clear();
		foreach(var item in Selection.objects){
			AnimationClip anim = item as AnimationClip;
			if( null != anim){
				anims.Add(anim);
			}
			
		}
		Repaint();
		
	}
}