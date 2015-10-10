using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialHelper : XMonoBehaviour {

    [HeaderAttribute( "所有教程" )]    
    public TutorialModel[] allTutorials = null;
    [HeaderAttribute( "当前教程" )]
    public TutorialModel currentTutorial = null;

    public GameObject lightObject = null;
    public Button currentButton = null;
    public GameObject characterObject = null;

    void Start()
    {
    }

    [XAttribute( "开始教程" )]
    public void BeginTutorial( int index )
    {
        Debug.Log( "index is : " + index );
        ID = 0;
        currentTutorial = allTutorials[index];
        Step();
    }
    int ID = 0;

    public void Step()
    {
        if( ID < currentTutorial.orders.Length )
        {
            if(!IsInvoking("ExcuteAction"))
                Invoke( "ExcuteAction", currentTutorial.orders[ID].waitTime );
        }
    }
    void ExcuteAction()
    {
        
        switch( currentTutorial.orders[ID].behaviour )
        {
            case TutorialBehaviour.Dialog:
                OnDisplayDialog();
                break;
            case TutorialBehaviour.ButtonClickEvent:
                OnShowButton();
                break;
            case TutorialBehaviour.Action:
                break;
            case TutorialBehaviour.None:
                break;
        }
        
    }
    
    void OnDisplayDialog()
    {
        Debug.Log( "content : " + currentTutorial.talkContent[0] );
        currentTutorial.talkContent.RemoveAt( 0 );
        currentTutorial.talkContent.TrimExcess();
        ID++;
        Step();
    }
    void OnShowButton()
    {
        GameObject lightGo = currentTutorial.effectObjects[0];
        currentButton = lightGo.GetComponent<Button>();
        currentButton.onClick.AddListener( ButtonClickCallback );
        Debug.Log("button : " + lightGo);
        
        currentTutorial.effectObjects.RemoveAt( 0 );
        currentTutorial.effectObjects.TrimExcess();
        
    }

    void ButtonClickCallback()
    {
        currentButton.onClick.RemoveListener( ButtonClickCallback );
        ID++;
        Step();
        //if( currentTutorial.events[0].action == XEvent.CallAction.Continue )
        //{
        //    Step();
        //}
    }
}
