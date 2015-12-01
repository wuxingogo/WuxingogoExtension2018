using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

public class XMethodExtension : XBaseWindow 
{
	Object scrObj = null;
    string targetType = "";
	int paraInt = 0;
	float paraFloat = 0.0f;
	bool paraBool = false;
	string paraString = "default";
	System.Enum paraEnum = null;

	public override void OnXGUI(){
        scrObj = CreateObjectField(targetType, scrObj);
		if( null != scrObj ){
            targetType = scrObj.GetType().ToString();

			MethodInfo[] methods = scrObj.GetType().GetMethods();
			
			
			foreach( MethodInfo method in methods ){
				if(method.DeclaringType == scrObj.GetType()){
					CreateStringField(method.ReturnType.ToString() , method.Name);
					ParameterInfo[] paras = method.GetParameters();
					object[] arr = new object[paras.Length];
					EditorGUILayout.BeginVertical();
					{
						for(int pos = 0; pos < paras.Length; pos++){
							
							if(paras[pos].ParameterType == typeof(System.Int32)){
								paraInt = CreateIntField(paras[pos].Name + ": int" , paraInt);
								arr[pos] = paraInt;
							}else if(paras[pos].ParameterType == typeof(System.String)){
								paraString = CreateStringField(paras[pos].Name + ": string " , paraString);
								arr[pos] = paraString;
							}else if(paras[pos].ParameterType == typeof(System.Single)){
								paraFloat = CreateFloatField(paras[pos].Name + ": float " , paraFloat);
								arr[pos] = paraFloat;
							}else if(paras[pos].ParameterType == typeof(System.Boolean)){
								paraBool = CreateCheckBox(paras[pos].Name + ": bool ",  paraBool);
								arr[pos] = paraBool;
							}
							else if(paras[pos].ParameterType.BaseType == typeof(System.Enum)){
								 if(paraEnum == null || paraEnum.GetType() != paras[pos].ParameterType){
                                    // paraEnum = paras[pos];
//                                    System.Enum.Parse (paras[pos].ParameterType, "a");
									paraEnum = (System.Enum)System.Enum.ToObject(paras[pos].ParameterType,0);

                                }
                                paraEnum = CreateEnumSelectable(paras[pos].Name + ": enum ", paraEnum);
                                arr[pos] = paraEnum;
							}
							
						}
						
						if(CreateSpaceButton("Invoke")){
							method.Invoke(scrObj,arr);
						}
					}
					EditorGUILayout.EndVertical();
				}
				
			}
		}
	}

	void OnSelectionChange(){
		//TODO List

	}
}