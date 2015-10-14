using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class ModelNode<T> : BaseNode{
    T t;
    public List<T> models = new List<T>();
    public List<LinkJoint> joints = new List<LinkJoint>();
    
    public ModelNode(T t) : base(){
        this.t = t;
        //models.Add( t );
        GraphTitle = "ModelNode";
        GraphType = NodeType.Model;
    }

    
    public override void DrawGraph( int id )
    {
        //base.DrawGraph( id );
        GraphTitle = EditorGUILayout.TextField( GraphTitle );

        EditorGUILayout.BeginHorizontal();
        if( GUILayout.Button( "Add " + t.GetType() ) ){
            models.Add(t);
            LinkJoint ljoint = new LinkJoint( this, ( joints.Count + 4 ) * EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight );
            ljoint.OnClickInput = OnClickInput;
            ljoint.OnClickOutput = OnClickOutput;
            joints.Add( ljoint );
        }
        if( GUILayout.Button( "Sub" ) )
        {
            models.RemoveAt( models.Count - 1 );
            models.TrimExcess();
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < models.Count; i++){
            if( t is string ){
                List<string> std = models as List<string>;
                std[i] = joints[i].DrawString( std[i] );
            }
            else if( t is int ){
                List<int> std = models as List<int>;
                std[i] = joints[i].DrawInt( std[i] );
            }
            else if( t is float ){
                List<float> std = models as List<float>;
                std[i] = joints[i].DrawFloat( std[i] );
            }
            else if( t is Object){
                List<Object> std = models as List<Object>;
                //std[i] = joints[i].DrawFloat( std[i] );
                //std[i] = EditorGUI.ObjectField( new Rect( 5, ( i + 4 ) * EditorGUIUtility.singleLineHeight, 200 - 30, EditorGUIUtility.singleLineHeight ), std[i] );  
            }
            joints[i].DrawJoint();
            joints[i].DrawCurves();
		}
    }

    void OnClickInput(){
        if( GraphWindow.GetInstance().IsTransition )
            currJoint.SetInputJoint( GraphWindow.GetInstance().InputNode.currJoint );
        else
            GraphWindow.GetInstance().SetTransition( this );
    }
    void OnClickOutput(){
        if( GraphWindow.GetInstance().IsTransition )
            currJoint.SetInputJoint( GraphWindow.GetInstance().InputNode.currJoint );
        else
            GraphWindow.GetInstance().SetTransition( this );
    }



    

}
