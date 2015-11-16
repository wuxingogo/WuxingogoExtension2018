using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using XRuntime;




[CustomEditor( typeof( XMonoBehaviour ), true )]
public class XMonoBehaviourEditor : XBaseEditor
{
    private Dictionary<string, object[]> methodParameters = new Dictionary<string, object[]>();
    public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		foreach( var info in target.GetType().GetMethods() ){
			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
                CreateSpaceBox();
                CreateLabel( "XMethod : " + info.Name );
                ParameterInfo[] paras = info.GetParameters();
                if( !methodParameters.ContainsKey( info.Name ) )
                {
                    object[] o = new object[paras.Length];
                    methodParameters.Add(info.Name, o);
                }
                object[] objects = methodParameters[info.Name];
                for( int pos = 0; pos < paras.Length; pos++ )
                {
                    if( paras[pos].ParameterType == typeof( System.Int32 ) )
                    {
                        if( null == objects[pos] )
                            objects[pos] = 0;
                        objects[pos] = ( int )CreateIntField( "parameter" + pos + ": Int ", ( int )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( System.String ) )
                    {
                        if( null == objects[pos] )
                            objects[pos] = "";
                        objects[pos] = ( string )CreateStringField( "parameter" + pos + ": String ", ( string )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( System.Single ) )
                    {
                        if( null == objects[pos] )
                            objects[pos] = 0.0f;
                        objects[pos] = ( float )CreateFloatField( "parameter" + pos + ": Float ", ( float )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( UnityEngine.Object ) )
                    {
                        objects[pos] = ( Object )CreateObjectField( "parameter" + pos + ": Object ", ( Object )objects[pos] );
                    }
                }   
				if(CreateSpaceButton(info.Name +"  "+ (att as XAttribute).title)){
                    info.Invoke( target, objects );
				}
                CreateSpaceBox();
			}
		}
	}
}