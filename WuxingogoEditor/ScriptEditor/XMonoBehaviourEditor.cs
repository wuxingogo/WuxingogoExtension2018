using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using wuxingogo.Runtime;


[CustomEditor( typeof( XMonoBehaviour ), true )]
public class XMonoBehaviourEditor : XBaseEditor
{
	internal Vector2 _scrollPos = Vector2.zero;
    const int Xoffset = 5;
    const int XButtonWidth = 100;
    const int XButtonHeight = 20;

    private Dictionary<string, object[]> methodParameters = new Dictionary<string, object[]>();
    public override void OnInspectorGUI()
	{
		GUILayout.Box(XResources.LogoTexture, GUILayout.Width(Screen.width - Xoffset), GUILayout.Height(100));
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
						objects[pos] = ( int )CreateIntField( paras[pos].Name  + ": Int ", ( int )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( System.String ) )
                    {
                        if( null == objects[pos] )
                            objects[pos] = "";
						objects[pos] = ( string )CreateStringField( paras[pos].Name + ": String ", ( string )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( System.Single ) )
                    {
                        if( null == objects[pos] )
                            objects[pos] = 0.0f;
						objects[pos] = ( float )CreateFloatField( paras[pos].Name + ": Float ", ( float )objects[pos] );
                    }
                    else if( paras[pos].ParameterType == typeof( UnityEngine.Object ) )
                    {
						objects[pos] = ( Object )CreateObjectField( paras[pos].Name + ": Object ", ( Object )objects[pos] );
                    }
                }   
				if(CreateSpaceButton(info.Name +"  "+ (att as XAttribute).title)){
                    info.Invoke( target, objects );
				}
                CreateSpaceBox();
			}
		}



		foreach( var info in target.GetType().GetFields() ){
			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
                CreateSpaceBox();
                CreateLabel( "XField : " + info.Name + " || " + info.GetValue(target).ToString() );

				if(typeof(IDictionary).IsAssignableFrom(info.FieldType)){

					IDictionary dictionary = ( IDictionary )info.GetValue(target);

					IEnumerator iteratorKey = dictionary.Keys.GetEnumerator();
					IEnumerator iteratorValue = dictionary.Values.GetEnumerator();

					while(iteratorKey.MoveNext() && iteratorValue.MoveNext()){
						BeginHorizontal();
						if(iteratorKey.Current.GetType().IsSubclassOf(typeof(Object))){
							CreateObjectField((Object)iteratorKey.Current);
						}else{
							CreateLabel(iteratorKey.Current.ToString());
						}
						if(iteratorValue.Current.GetType().IsSubclassOf(typeof(Object))){
							CreateObjectField((Object)iteratorValue.Current);
						}else{
//							BeginHorizontal();
							CreateLabel(iteratorValue.Current.ToString());

//							EndHorizontal();
						}
						EndHorizontal();
					}
				}

				if(typeof(ICollection).IsAssignableFrom(info.FieldType)){

					ICollection collection = ( ICollection )info.GetValue(target);

					IEnumerator iteratorValue = collection.GetEnumerator();

					while( iteratorValue.MoveNext()){
						if(iteratorValue.Current.GetType().IsSubclassOf(typeof(Object))){
							CreateObjectField((Object)iteratorValue.Current);
						}else{
							BeginHorizontal();
							CreateLabel(iteratorValue.Current.ToString());
							DoButton<object>("Reflection", OpenInMethod, iteratorValue.Current);
							EndHorizontal();
						}
					}
				}
			}
		}


		foreach( var info in target.GetType().GetProperties() ){
			foreach(var att in info.GetCustomAttributes(typeof(XAttribute),true)){
                CreateSpaceBox();

				object result = info.GetValue(target, null);

				BeginHorizontal();

				string title = result == null ? "NULL" : result.ToString();

				CreateLabel( "XProperty : " + info.Name + " || " +  title);

				if(typeof(Object).IsAssignableFrom(info.PropertyType)){
					result = CreateObjectField((Object)result);
					if(GUI.changed)
						info.SetValue(target, result, null);
				}

				EndHorizontal();
			}
		}


	}

	private void OpenInMethod(object target){
		XReflectionWindow method = XBaseWindow.Init<XReflectionWindow>();
		method.Target = target;
	}
}