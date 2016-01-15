using UnityEngine;
using System.Reflection;
using System.Collections;
using wuxingogo.Reflection;
namespace wuxingogo.Runtime {
    public class XReflectionManager {
	
	    public static System.Type GetClass(string className){
			return XReflectionUtils.TryGetClass(className);
	    }
	
	    public static object CallMethod(object target, System.Type type, string methodCMD){
		    string method_str = "";
		    string parasCMD = "";
		    object[] paras = null;
		
		
		    method_str = methodCMD.Substring(0,methodCMD.IndexOf("("));
		    parasCMD =  methodCMD.Substring(methodCMD.IndexOf("("), methodCMD.Length - methodCMD.IndexOf("("));
		    parasCMD = parasCMD.Substring( 1, parasCMD.Length - 2);
		    if(!parasCMD .Equals( "" )){
			    if(parasCMD.Contains(",")){
				    string[] strParas = parasCMD.Split(',');
				    paras = new object[strParas.Length];
				    for (int pos = 0; pos < strParas.Length; pos++) {
					    //  TODO loop in strParas
    //					paras[pos] = int.Parse( strParas[pos] );
    //					if(strParas[pos].Contains("\"")){
    //						paras.SetValue(parasCMD.Replace("\"",""),pos);
    //					}
    //						
    //					else
    //						paras.SetValue(int.Parse(strParas[pos]),pos);
					    paras.SetValue(GetParaFromString(strParas[pos]),pos);
				    }	
			    }else{
				    paras = new object[1];
				    paras.SetValue(GetParaFromString(parasCMD),0);
				
    //				if(parasCMD.Contains("\"")){
    //					parasCMD = parasCMD.Replace("\"","");
    //					paras.SetValue(parasCMD,0);
    ////					paras.SetValue(parasCMD,0);
    //				}
    //				else
    //					paras.SetValue(int.Parse(parasCMD),0);
    //				paras[0] = int.Parse( parasCMD );
			    }
		    }
		    MethodInfo[] thods = type.GetMethods();
		
    //		MethodInfo method = type.GetMethod(method_str,System.Reflection.BindingFlags.);
		    MethodInfo method = type.GetMethod(method_str);
		    if( null == method){
			    Debug.Log(target + " not have a " + method_str + " method." );
			    return null;
		    }
		    object returnValue = method.Invoke(target,paras);
		    return returnValue;
	    }
	
	    public static object GetParaFromString(string para){
		    if(para == ""){
			    return null;
		    }else if(para.Contains("\"")){
			    para = para.Replace("\"","");
			    return para;
		    }else if(para.Contains("true")){
			    return true;
		    }else if(para.Contains("false")){
			    return false;
		    }else {
			    return int.Parse(para);
		    }
		    return null;
	    }
	
	
	    public static object GetField(object target, System.Type type, string fieldName){
		    PropertyInfo property = type.GetProperty(fieldName);
		    FieldInfo field = type.GetField(fieldName);
		
		    if( null == property && null == field){
			    Debug.Log(type.Name + "." + " not contain " + fieldName);
			    return null;
		    }
		    object returnValue = null;
		    if( null != property )
			    returnValue = property.GetValue(target, null);
		    else if( null != field)
			    returnValue = field.GetValue(target);
		    if( null == returnValue){
			    Debug.Log(type.Name + "." + property.Name + " is null.");
			    return null;
		    }
		    return returnValue;
		
	    }
	
	    public static object GetValue(string command){
		
		
		    string[] strs = command.Split('.');
		    bool isFirst = true;
		    if(strs.Length < 2) return null;
		    System.Type classType = GetClass( strs[0] );
		    object returnValue = classType;
    //		System.Type target = classType;
		    for (int pos = 1; pos < strs.Length; pos++){
			    if(strs[pos].Contains("(") && strs[pos].Contains(")")){
				
				    returnValue = CallMethod(returnValue, classType,strs[pos]);
				    if( null != returnValue )
					    classType = returnValue.GetType();
			    }else{

				    returnValue = GetField(returnValue, classType, strs[pos]);
				    if( null != returnValue )
					    classType = returnValue.GetType();
    //				target = returnValue.GetType();
			    }
			    if(pos == strs.Length - 1){
    //				returnValue.SetValue(command);
				    return returnValue;	
			    }
		    }
		    return null;
	    }
	
	    public static void SetValue(string command){
		
	    }
    }

}
