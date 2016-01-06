#define  PlayMaker1
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

#if PlayMaker
using HutongGames.PlayMaker;
using wuxingogo.Runtime;
class XPlaymakerExtension : XBaseWindow
{
    Component component = null;
    string componentName = "";

    string seachEventByName = "";
    string seachActionByName = "";
    List<GameObject> seachObjects = new List<GameObject>();

    bool isPreciseSeach = true;
    [MenuItem( "Wuxingogo/Wuxingogo XPlaymakerExtension " )]
    static void init()
    {
        XPlaymakerExtension window = ( XPlaymakerExtension )EditorWindow.GetWindow( typeof( XPlaymakerExtension ) );
    }

    public override void OnXGUI()
    {
        //TODO List
        component = CreateObjectField( componentName, component ) as Component;
        if( null != component )
        {
            componentName = component.GetType().ToString();
            if( CreateSpaceButton( "Seach All" ) )
            {
                Component[] components = GameObject.FindObjectsOfType( component.GetType() ) as Component[];
                GameObject[] goArray = ArrayExtension.AllocArrayFormOther<Component, GameObject>( components );

                for( int pos = 0; pos < components.Length; pos++ )
                {
                    Debug.Log( "pos is " + components[pos].name );
                    goArray[pos] = components[pos].gameObject;
                }
                Selection.objects = goArray;
            }
        }

        isPreciseSeach = CreateCheckBox( "is Precise Seach", isPreciseSeach );

        BeginHorizontal();
        seachEventByName = CreateStringField( "Event Name", seachEventByName );
        if( CreateSpaceButton( "Seach Event" ) )
        {
            seachObjects.Clear();
            SeachPlaymakerByEventName();
            Selection.objects = seachObjects.ToArray();
        }
        EndHorizontal();

        BeginHorizontal();
        seachActionByName = CreateStringField( "Action Name", seachActionByName );
        if( CreateSpaceButton( "Seach Action" ) )
        {
            seachObjects.Clear();
            SeachPlaymakerByActionName();
            Selection.objects = seachObjects.ToArray();
        }
        EndHorizontal();



    }

    void SeachPlaymakerByEventName()
    {
        PlayMakerFSM[] components = GameObject.FindObjectsOfType( typeof( PlayMakerFSM ) ) as PlayMakerFSM[];
        //Debug.Log( "components length is " + components.Length );
        //Debug.Log( "seachName is " + seachName );
        for( int pos = 0; pos < components.Length; pos++ )
        {
            //Debug.Log( "components pos is " + components[pos].gameObject.name );
            FsmEvent[] events = components[pos].FsmEvents;
            //Debug.Log( "events length is " + events.Length );
            for( int idx = 0; idx < events.Length; idx++ )
            {
                //Debug.Log( "events length is " + events[idx].Name );
                if( seachEventByName.Equals( events[idx].Name ) && isPreciseSeach )
                {
                    seachObjects.Add( components[pos].gameObject );
                }
                else if( seachEventByName.Contains( events[idx].Name ) || events[idx].Name.Contains(seachEventByName) && !isPreciseSeach )
                {
                    seachObjects.Add( components[pos].gameObject );
                }
            }
        }

    }

    void SeachPlaymakerByActionName()
    {
        PlayMakerFSM[] components = GameObject.FindObjectsOfType( typeof( PlayMakerFSM ) ) as PlayMakerFSM[];
        //Debug.Log( "components length is " + components.Length );
        //Debug.Log( "seachName is " + seachName );
        for( int pos = 0; pos < components.Length; pos++ )
        {
            //Debug.Log( "components pos is " + components[pos].gameObject.name );
            FsmState[] states = components[pos].FsmStates;
            for( int idx = 0; idx < states.Length; idx++ )
            {
                FsmStateAction[] actions = states[idx].Actions;
                for( int i = 0; i < actions.Length; i++ )
                {
                    if( seachActionByName.Equals( actions[i].GetType().Name ) && isPreciseSeach )
                    {
                        seachObjects.Add( components[pos].gameObject );
                    }
                    else if( seachActionByName.Contains( actions[i].GetType().Name ) || actions[i].Name.Contains( seachEventByName ) && !isPreciseSeach )
                    {
                        seachObjects.Add( components[pos].gameObject );
                    }
                }
                
            }
        }
    }

    void OnSelectionChange()
    {
    }
}
#endif