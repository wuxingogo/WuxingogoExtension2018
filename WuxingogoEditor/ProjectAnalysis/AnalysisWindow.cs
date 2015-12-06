//ProjectAnalysis.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 12/01/2015 11:19:51 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using UnityEditor;
using wuxingogo.analysis;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System;
using Object = UnityEngine.Object;
using System.Linq;

public class AnalysisWindow : XBaseWindow {
	[MenuItem ("Wuxingogo/Wuxingogo AnalysisWindow ")]
	static void init () {
		Init<AnalysisWindow>();
	}
	
	List<string> allResult = new List<string>();
//	List<Object> allObject = new List<Object>();
	Object[] allObject = new Object[0];
	string strBehaviour = string.Empty;
	
	const string STR_PROCESS = "Process";
	const string STR_ASSEMBLY = "Assembly";
	const string STR_OBJECT = "Object";
	const string STR_SHOWALL = "ShowHideGameObject";
	
	int showID = -1;
	SerializedObject serializedObject = null;
	
	List<string> allTags = new List<string>();
	
	public override void OnXGUI()
	{
		AddButton<string>(STR_PROCESS, OnClick, STR_PROCESS);
		AddButton<string>(STR_ASSEMBLY, OnClick, STR_ASSEMBLY);
		AddButton<string>(STR_OBJECT, OnClick, STR_OBJECT);
		AddButton<string>(STR_SHOWALL, OnClick, STR_SHOWALL);
		
		
		for( int pos = 0; pos < allResult.Count; pos++ ) {
			//  TODO loop in allResult.Count
			CreateSpaceButton(allResult[pos]);
		}
		
		for( int pos = 0; pos < allObject.Length; pos++ ) {
			//  TODO loop in allObject.Count
			BeginHorizontal();
			if(CreateSpaceButton(allObject[pos].GetType().ToString())){
				if(pos == showID){
					showID = -1;
				}else{
				
					showID = pos;
					serializedObject = new SerializedObject(allObject[pos]);
					
				}
			}
			allObject[pos] = CreateObjectField(allObject[pos]);
			EndHorizontal();
			if(pos == showID && null != serializedObject){
				DrawProperty(serializedObject);
			}
		}
	}
	
	void OnClick(string beh){
		allResult.Clear();
		strBehaviour = beh;
		switch(beh){
			case STR_PROCESS:
				Process[] processes = Process.GetProcesses();	
				foreach(var process in processes) 
				{
					try{
						if(process != null && !process.HasExited)
							allResult.Add(process.ToString());
					}catch(InvalidOperationException e){
						UnityEngine.Debug.Log("Fuck InvalidOperationException");
					}
				}
				
				
				break;
			case STR_ASSEMBLY:
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach(Assembly assembly in assemblies) 
				{
					allResult.Add(assembly.FullName);
				}
				break;
			
			case STR_OBJECT:
//				allObject = Object.FindObjectsOfType(typeof(MonoBehaviour));
				Transform[] transfroms = FindObjectsOfType<Transform>();
				var tag = from tran in transfroms
					where !allTags.Contains(tran.tag) select tran.tag;
				allTags.AddRange(tag);
				
				for (int pos = 0; pos < allTags.Count; pos++) {
					//  TODO loop in allTags.Count
					UnityEngine.Debug.Log("pos is : " + allTags[pos]);
				}
				break;
			case STR_SHOWALL:
				GameObject[] gos = Object.FindObjectsOfType<GameObject>();
				foreach( var item in gos ) {
					if((item.hideFlags & HideFlags.HideInHierarchy) == HideFlags.HideInHierarchy){
						item.hideFlags = item.hideFlags ^ HideFlags.HideInHierarchy ^ HideFlags.DontSaveInEditor;
						UnityEngine.Debug.Log(string.Format("{0} is hideFlags", item.name));
					}
				}
				break;
				
		}	
		
	}
	
	public T[] FindObjectsOfType<T>() where T : Object{
		return Object.FindObjectsOfType<T>();
	}
	
	
	public void DrawProperty(SerializedObject obj){
		SerializedProperty property = null;
		property = obj.GetIterator();
		while(property.NextVisible(true)){
			EditorGUILayout.PropertyField(property, new GUILayoutOption[0]);
		}
		obj.ApplyModifiedProperties();
	}
}