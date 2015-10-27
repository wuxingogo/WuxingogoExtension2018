//  TUTORIALHELPER
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2015 wuxingogo
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ------------------------------------------------------------------------------
// 2015/10/16 
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialHelper : XMonoBehaviour {

    [HeaderAttribute( "所有教程" )]    
    public TutorialModel[] allTutorials = null;
    [HeaderAttribute( "当前教程" )]
    public TutorialModel currentTutorial = null;

    public Transform lightObject = null;
    public Button currentButton = null;
    public Button touchMask = null;

    private Transform lastParent = null;
    

    void Start()
    {
    }
    
    [XAttribute( "开始教程" )]
    public void BeginTutorial( int index )
    {
        Debug.Log( "index is : " + index );
        ID = 0;
        currentTutorial = allTutorials[index];
        touchMask.gameObject.SetActive( true );
        Step();
    }
    int ID = 0;

    public void Step()
    {
        if( ID < currentTutorial.orders.Length ){
            if(!IsInvoking("ExcuteAction"))
                Invoke( "ExcuteAction", currentTutorial.orders[ID].waitTime );
        }
        else{
            EndedTutorial();
        }
    }
    public void EndedTutorial(){
        Debug.Log( "EndedTutorial" );
        touchMask.gameObject.SetActive( false );

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
        lightObject.gameObject.SetActive( false );
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

        lastParent = currentButton.transform.parent;
        currentButton.transform.SetParent( touchMask.transform );
        Vector3 tarPos = currentButton.transform.position;

        lightObject.localPosition = tarPos;
        lightObject.gameObject.SetActive( true );

        
        currentTutorial.effectObjects.RemoveAt( 0 );
        currentTutorial.effectObjects.TrimExcess();
        
    }

    void ButtonClickCallback()
    {
        currentButton.onClick.RemoveListener( ButtonClickCallback );
        currentButton.transform.SetParent( lastParent );
        ID++;
        Step();
        //if( currentTutorial.events[0].action == XEvent.CallAction.Continue )
        //{
        //    Step();
        //}
    }
}
