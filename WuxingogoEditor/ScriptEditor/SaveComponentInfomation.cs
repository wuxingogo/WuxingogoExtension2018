/// <summary>
/// https://github.com/tsubaki/SerializedParameter_Unity
/// </summary>

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Save component infomation.
/// </summary>
[InitializeOnLoad]
public class SaveComponentInfomation
{
	static bool isRecording = false;
	static Dictionary<int, Dictionary<string, object>> saveObjectDic = new Dictionary<int, Dictionary<string, object>> ();
	
	static SaveComponentInfomation ()
	{
		EditorApplication.playmodeStateChanged = () => 
		{
			if (isRecording) {
				foreach (MonoBehaviour component  in GameObject.FindObjectsOfType(typeof(MonoBehaviour))) {
					if (EditorApplication.isPlaying) {
						SerializeComponent (component);
					} else {
						DeserializeComponent (component);
					}
				}
			}
			
			isRecording = EditorApplication.isPlaying;
			if (isRecording == false && !EditorApplication.isPlaying) {
				saveObjectDic.Clear ();
			}
		};
	}
	
	private static void SerializeComponent (MonoBehaviour component)
	{
		var dic = new Dictionary<string, object> ();
		var type = component.GetType ();
		foreach (var field in type.GetFields()) {
			if (field.GetCustomAttributes (typeof(PersistentAmongPlayModeAttribute), true).Length != 0) {
				dic.Add (field.Name, field.GetValue (component));
			}
		}
		var instanceID = component.GetInstanceID ();
		if(!saveObjectDic.ContainsKey(instanceID))
			saveObjectDic.Add (instanceID, dic);
	}

	private static void DeserializeComponent (MonoBehaviour component)
	{
		var type = component.GetType ();
		var dict = saveObjectDic [component.GetInstanceID ()];
		foreach (var field in type.GetFields()) {
			if (dict.ContainsKey(field.Name) && field.GetCustomAttributes (typeof(PersistentAmongPlayModeAttribute), true).Length != 0) {
				field.SetValue (component, dict [field.Name]);
			}
		}
	}
}