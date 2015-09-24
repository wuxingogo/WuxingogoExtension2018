using UnityEngine;
using UnityEditor;
using System.Collections;
public class XPlaymakerExtension : XBaseWindow 
{
    Component component = null;
    string componentName = "";
	[MenuItem ("Wuxingogo/Wuxingogo XPlaymakerExtension ")]
	static void init () {
		XPlaymakerExtension window = (XPlaymakerExtension)EditorWindow.GetWindow (typeof (XPlaymakerExtension ) );
	}

	public override void OnXGUI(){
		//TODO List
        component = CreateObjectField(componentName, component) as Component;
        if (null != component)
        {
            componentName = component.GetType().ToString();
            if (CreateSpaceButton("Seach All"))
            {
                Component[] components = GameObject.FindObjectsOfType(component.GetType()) as Component[];
                GameObject[] goArray = ArrayExtension.AllocArrayFormOther<Component, GameObject>(components);
                
                for (int pos = 0; pos < components.Length; pos++)
                {
                    Debug.Log("pos is " + components[pos].name);
                    goArray[pos] = components[pos].gameObject;
                }
                Selection.objects = goArray;
                
                
            }
        }
       
	}



	void OnSelectionChange(){
	}
}