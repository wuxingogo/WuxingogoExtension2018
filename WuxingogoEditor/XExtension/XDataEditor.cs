using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(XData)), CanEditMultipleObjects]
public class XDataEditor : XBaseEditor 
{

	XData model = null;
	int dataSize = 0;

	public override void OnXGUI()
	{
		Init();
		
		GUI.changed = false;
		if (Event.current.type == EventType.Layout)
		{
			return;
		}

        model = target as XData;

        dataSize = model.AllData.Count;
        dataSize = CreateGUIInt("Array Size", dataSize);
        if( dataSize != model.AllData.Count )
        {
            
        }
		
		
		if( GUI.changed ){
            EditorUtility.SetDirty( model );
			AssetDatabase.SaveAssets();
		}
		
	}
	
	public void ChangeArray(){
	}
	

}