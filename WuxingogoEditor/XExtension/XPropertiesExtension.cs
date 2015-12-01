using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
public class XPropertiesExtension : XBaseWindow 
{

	Object scrObj = null;

	public override void OnXGUI(){
			
		scrObj = CreateObjectField("Script", scrObj);
		if( null != scrObj ){
		
			PropertyInfo[] fields = scrObj.GetType().GetProperties();
			foreach( PropertyInfo field in fields ){
				if( scrObj.GetType() == field.DeclaringType ) {
//					CreateStringField("method name " , field.Name);
					
					EditorGUILayout.BeginVertical();{
						
							
						
						object value = field.GetValue(scrObj, null);
						object changeValue = null;
						if(field.PropertyType == typeof(System.Int32)){
							changeValue = CreateIntField(field.Name + ": int" , (int)value);
						}else if(field.PropertyType == typeof(System.Int64)){
//							CreateIntField(field.Name + ": int" , (int)value);
						}else if(field.PropertyType == typeof(System.Single)){
							changeValue = CreateFloatField(field.Name + ": float" , (float)value);
						}else if(field.PropertyType.BaseType == typeof(System.Array)){
//							for( int pos = 0; pos < System.Array.) {
//								
//							}
							Object[] array = value as Object[];
							if(null != array){
								for(int i = 0; i < array.Length; i++){
									array[i] = CreateObjectField(field.Name + "[" + i +"]" , array[i]);
								}
								changeValue = array	;
							}
							else
								changeValue = value;
//							changeValue = CreateStringField(field.Name + ": array" , value.ToString());
						}else if(field.PropertyType == typeof(System.Boolean)){
							changeValue = CreateCheckBox(field.Name + ": bool", (bool)value);
						}else if(field.PropertyType == typeof(System.String)){
							changeValue = CreateStringField(field.Name + ": string", (string)value);
						}else if(field.PropertyType.BaseType == typeof(System.Enum)){
							changeValue = CreateEnumSelectable(field.Name + ": string", (System.Enum)value);
						}
//						else if(field.FieldType.BaseType == ))
////							Debug.Log(field.FieldType.ToString());
////							CreateObjectField(field.Name + ": object", (Object)value);
//						}
						else if(field.PropertyType.BaseType == typeof(UnityEngine.Object)){
							
							changeValue = CreateObjectField(field.Name + ": " + field.PropertyType, (UnityEngine.Object)value);
						}else if(field.PropertyType.BaseType == typeof(UnityEngine.Behaviour)){
							
							changeValue = CreateObjectField(field.Name + ": " + field.PropertyType, (UnityEngine.Behaviour)value);
						}
						else if(field.PropertyType.BaseType == typeof(UnityEngine.MonoBehaviour)){
							
							changeValue = CreateObjectField(field.Name + ": " + field.PropertyType, (UnityEngine.Behaviour)value);
						}
						else if(field.PropertyType.BaseType == typeof(System.Object)){
							
							changeValue = value;
//							CreateObjectField(field.Name + ": " + field.FieldType, (UnityEngine.Behaviour)value);
						}
						else if(field.PropertyType.BaseType == typeof(System.ValueType)){
							changeValue = value;
						}
						else{
							changeValue = CreateObjectField(field.Name + ": " + field.PropertyType, (Object)value);
						}
						if(GUI.changed && changeValue != value){
//							Debug.Log("gui change");
							Undo.RecordObject(scrObj, "Record ScrObject");
							try{
								field.SetValue(scrObj,changeValue,null);
							}catch(System.ArgumentException e){
								
							}
							catch(System.FieldAccessException e){
								
							}
							
							Repaint();
						}
					}EditorGUILayout.EndVertical();
					
				}
			}
		}
		
	}
}