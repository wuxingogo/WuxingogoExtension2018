using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class LinkJoint : ScriptableObject {
    public LinkJoint inputJoint = null;
    public List<LinkJoint> outputJoints = new List<LinkJoint>();

    public BaseNode parent = null;

	public Rect inputRect;
	public Rect outputRect;
    public Rect middleRect;

    public Rect inputCurveRect;

    public Action OnClickInput = null;
    public Action OnClickOutput = null;
    public LinkJoint( BaseNode parent, float y, float height ){
        this.parent = parent;
        inputRect = outputRect = new Rect( 0, y, 10, height );
        inputRect.x = 5;
        outputRect.x = parent.GraphRect.width - 15;
        middleRect = new Rect( 20, y, parent.GraphRect.width - 40, height );
	}
    internal void DrawJoint(bool hasInput = true, bool hasOutput= true){
        if( hasInput ){
            if( GUI.Button( inputRect, "" )){
                
                parent.LinkRect = inputRect;
                if(null != OnClickInput)
                    OnClickInput();
                parent.SetCurrentLinkJoint( this );
            }
        }
        if( hasOutput ){
            if( GUI.Button( outputRect, "" )){
                
                parent.LinkRect = outputRect;
                if( null != OnClickOutput )
                    OnClickOutput();
                parent.SetCurrentLinkJoint( this );
            }
        }
    }

    public void SetInputJoint( LinkJoint inputJoint )
    {
        //this.inputJoint = inputJoint;
        if( null == this.inputJoint){
            this.inputJoint = inputJoint;
            this.inputCurveRect = inputJoint.outputRect;
        }else{

        }
    }
    public void DrawCurves(){
        if(inputJoint != null)
            GraphWindow.DrawNodeCurve( inputJoint.outputRect, inputRect );
    }
    
    internal int DrawInt(int value){
        return value = EditorGUI.IntField( middleRect, value );
    }
    internal float DrawFloat( float value ){
        return value = EditorGUI.FloatField( middleRect, value );
    }
    internal string DrawString( string value ) {
        return value = EditorGUI.TextField( middleRect, value );
    }
}
