using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TouchScript;
using TouchScript.Gestures;

public class TestDrag : MonoBehaviour
{
    public GameObject go = null;

    public Transform pivot = null;
    public Transform camera = null;

    Vector3 mouseReference = Vector3.zero;

    private ITouchManager instance = null;
    void Start()
    {
        instance = TouchManager.Instance;
        instance.TouchesMoved += OnDrag;
    }
    public void OnDrag( object sender, TouchEventArgs e )
    {
        PanGesture gesture = ( PanGesture )sender;
        //Vector3 offset = ( Input.mousePosition - mouseReference );
        //go.transform.Rotate( new Vector3( 0, offset.x * 0.005f, 0 ) );


        float limitY = camera.rotation.eulerAngles.y + gesture.ScreenPosition.x - gesture.PreviousScreenPosition.x;
        if( limitY < 90 || limitY > 270 )
        {
            camera.rotation = Quaternion.Lerp( camera.transform.rotation,
            Quaternion.Euler( camera.rotation.eulerAngles.x, limitY, camera.rotation.eulerAngles.z ), Time.deltaTime * 15 );
        }

        float limitZ = pivot.localRotation.eulerAngles.z + gesture.ScreenPosition.y - gesture.PreviousScreenPosition.y;
        if( limitZ < 90 || limitZ > 270 )
        {
            pivot.localRotation = Quaternion.Lerp( pivot.localRotation,
                Quaternion.Euler( pivot.localRotation.eulerAngles.x, pivot.localRotation.eulerAngles.y, limitZ ),
                Time.deltaTime * 15 );
        }
    }
}
