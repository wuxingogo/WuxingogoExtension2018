using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

//
//	Get a kind of properties with a component.
//
public class XPropertiesToSerializables : XBaseWindow 
{
	bool isDirtyConfig = true;

	Component targetComponent = null;
	List<Component> mComponents = new List<Component>();
	List<PropertyInfo> mProperties = new List<PropertyInfo>();
	PropertyInfo targetProperty = null;
    object propertyinstance = null;
	[MenuItem ("Wuxingogo/Wuxingogo XPropertiesToSerializables ")]
	static void init () {
		Init<XPropertiesToSerializables>();
	}

	public override void OnXGUI(){

		if(CreateSpaceButton("Clean")){
			isDirtyConfig = true;
		}
		//
		// Set a kind of Component.
		targetComponent = (Component)CreateObjectField("component", targetComponent, typeof(Component));


		if( targetComponent != null && isDirtyConfig ){
			isDirtyConfig = false;
			mProperties.Clear();
			foreach( var item in targetComponent.GetType().GetProperties() ){
				mProperties.Add(item);
			}
		}

		for(int pos = 0; pos < mProperties.Count; pos++ ){
            BeginHorizontal();
			if(CreateSpaceButton(mProperties[pos].PropertyType + " : " + mProperties[pos].Name, 150)){
                //targetProperty = mProperties[pos];
                //mProperties.Clear();
                propertyinstance = mProperties[pos].GetValue( targetComponent, null );
                PropertyInfo[] properties = mProperties[pos].PropertyType.GetProperties();
                mProperties.Clear();
                foreach( var item in properties ){
                    mProperties.Add( item );
                }
                
			}
            if( CreateSpaceButton( "ToString" ) ){
                if( null == propertyinstance ){
                    Debug.Log( mProperties[pos].GetValue( targetComponent, null ).ToString() );
                }
                else {
                    Debug.Log( mProperties[pos].GetValue( propertyinstance, null ).ToString() );
                }    
            }
            EndHorizontal();
		}

		if( null != targetProperty ){
			if(CreateSpaceButton(targetProperty.Name)){
				isDirtyConfig = true;
				targetProperty = null;
			}
		}


		//
		// Create A Button to get all component.
		

        //if( CreateSpaceButton("Get Component", 200) && targetProperty != null ){
        //    if(Selection.gameObjects.Length > 0){
        //        mComponents.Clear();
        //        isDirtyConfig = true;
        //        foreach( var item in Selection.gameObjects ){
        //            Component component = item.transform.GetComponent(targetComponent.GetType());
        //            if( null != component ){
        //                mComponents.Add(component);
        //            }
        //        }
				
        //        foreach( var item in mComponents ){
        //            Debug.Log( item.GetType().GetProperty(targetProperty.Name).GetValue(targetComponent, null) ); 
        //        }
        //    }
		
			
		}
#if TINYTIME
		if( CreateSpaceButton("Set Property", 200) ){
			XPetsManager manager = Selection.gameObjects[0].GetComponent<XPetsManager>();
			targetProperty.SetValue(targetComponent, manager.GetType().GetProperty("Get69Times"), null);
		}
#endif

}


