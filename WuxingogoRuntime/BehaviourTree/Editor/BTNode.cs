using System;
using wuxingogo.Node;
using UnityEngine;
using UnityEditor;
using wuxingogo.btFsm;
using System.Collections;
using System.Collections.Generic;

namespace wuxingogo.BTNode
{
    public class BTNode : DragNode
    {
        public BTState BtState;
        public bool isCustomState = false;

        //public static BTNode<T> CreateNode( T state ) 
        //{
        //    var t = CreateInstance(typeof( BTNode<T> )) as BTNode<T>;
            
        //    if( typeof(T).GetCustomAttributes( typeof( StateTitleAttribute ), true ).Length > 0 )
        //    {
        //        t.isCustomState = true;
        //    }
        //    return t;
        //}

        public BTNode( BTState btState )
        {
            this.BtState = btState;
            if( btState.GetType().GetCustomAttributes( typeof( StateTitleAttribute ), true ).Length > 0 )
            {
                isCustomState = true;
            }
        }

        #region implemented abstract members of DragNode

        public virtual Texture ThumbnailTexture
        {
            get
            {
                Texture thumb = AssetPreview.GetAssetPreview( Asset() );
                if( thumb == null )
                {
                    thumb = AssetPreview.GetMiniTypeThumbnail( typeof( GameObject ) );
                }
                return thumb;
            }
        }

        public override UnityEngine.Object Asset()
        {
            return BtState;
        }

        public override Rect Bounds
        {
            get
            {
                return BtState.Bounds;
            }
            set
            {
                BtState.Bounds = value;
            }
        }

        private Rect _Bounds = new Rect( 0, 0, 100, 100 );



        public string Title
        {
            get
            {
                return BtState.Name;
            }
        }

        public void SetPosition( Vector2 position )
        {
            BtState.Bounds.position = position;
        }
        public void FireEvent(BTEvent targetEvent)
        {
            for( int i = 0; i < BtState.totalEvent.Count; i++ )
            {
                if( BtState.totalEvent[i] == targetEvent && targetEvent.TargetState != null )
                {
                    var key = BtState.totalEvent[i];
                    if( totalRunningEvent.ContainsKey( key ) )
                        totalRunningEvent.Remove( key );
                    totalRunningEvent.Add( key, 0 );
                }
            }
        }

        public Dictionary<BTEvent, int> totalRunningEvent = new Dictionary<BTEvent, int>();
        void DrawLine( int eventIndex, Rect rhs )
        {
            var lhs = GetEventRect( eventIndex );
            var dir = ( lhs.position - rhs.position ).normalized;
            var sign = Mathf.Sign( dir.x );

            Vector3 startPos = new Vector3( lhs.x + Mathf.Max( 0, lhs.width * -sign ), lhs.y + lhs.height / 2, 0 );
            Vector3 endPos = new Vector3( rhs.x + Mathf.Max( 0, rhs.width * sign ), rhs.y + rhs.height / 2, 0 );

            var distance = ( startPos - endPos ).magnitude * 0.5f;
            Vector3 startTan = startPos + Vector3.right * distance * -sign;
            Vector3 endTan = endPos + Vector3.left * distance * -sign;

            Color shadowCol = new Color( 0, 0, 0.5f, 0.06f );

            var allPoint = Handles.MakeBezierPoints( startPos, endPos, startTan, endTan, 40 );
          

            var currEvent = BtState.totalEvent[eventIndex];
            var currPointIndex = totalRunningEvent[currEvent];
            if( totalRunningEvent[currEvent] < 39 )
            {
                var nextPointIndex = currPointIndex+1;
                var forwardColor = Handles.color;
                float percent = totalRunningEvent[currEvent] * 1.0f / 40;
                using( new HandleColor( Color.Lerp( Color.red, Color.green, percent ) ) )
                {
                    Handles.DrawAAPolyLine( 10, allPoint[currPointIndex], allPoint[nextPointIndex] );
                }
                totalRunningEvent[currEvent]++;
            }
            else
            {
                totalRunningEvent.Remove( BtState.totalEvent[eventIndex] );
            }
            

            
            //for( int i = 1; i < allPoint.Length; i++ )
            //{
            //    var nextPoint = allPoint[i];
            //    Handles.DrawLine( nextPoint, currPoint );
            //    currPoint = nextPoint;
            //}
            

        }

        Rect GetEventRect( int i )
        {
            return new Rect( new Vector2( DrawBounds.xMin, DrawBounds.center.y + 50 + i * 40 ), new Vector2( 100, 20 ) );
        }

        public void OpenScript()
        {
            BTEditorWindow.OpenTypeScript( BtState.GetType() );
        }

        public override void Draw()
        {
            
            
            GUI.Box( DrawBounds, "", 
                isCustomState ? XStyles.GetInstance().FindStyle( "RedBox" ) : XStyles.GetInstance().FindStyle( "GreyBox" ) );
            DrawChildNodes();

            if( Selected )
            {
                GUI.Box( DrawBounds, "",
                    isCustomState ? XStyles.GetInstance().FindStyle( "GreenBox" ) : XStyles.GetInstance().FindStyle( "BlueBox" ) );
            }
            if( BtState.Owner.currState != null && BtState.Name == BtState.Owner.currState.Name )
            {
                GUI.Box( DrawBounds, "", XStyles.GetInstance().FindStyle( "OrangleBox" ) );
            }

                for( int i = 0; i < BtState.totalEvent.Count; i++ )
                {
                    Rect button = GetEventRect( i );
                    var targetEvent = BtState.totalEvent[i];
                    bool isCurrentEvent = targetEvent == BTEditorWindow.instance.currentEvent;
                    if( GUI.Button( button, targetEvent.Name, XStyles.GetInstance().button ) )
                    {
                        BTEditorWindow.instance.SetCurrentEvent( targetEvent );
                    }
                    
                    if( isCurrentEvent )
                    {
                        BTEditorWindow.instance.SetCurrentRect( button );
                    }
                    BTNode targetStateNode = null;
                    if( targetEvent.TargetState != null )
                    {
                        targetStateNode = BTEditorWindow.instance.FindBTNode( targetEvent.TargetState );
                        DrawBesizeFromRect( button, targetStateNode.DrawBounds );
                    }

                    if( totalRunningEvent.ContainsKey( targetEvent ) )
                    {
                        
                        DrawLine( i, targetStateNode.DrawBounds );
                    }

                }
                GUI.Box( DrawBounds, ThumbnailTexture, XStyles.GetInstance().FindStyle( "TextureBox" ) );
                GUI.Label( DrawBounds, Title, XStyles.GetInstance().label );
            }
            
        }

        #endregion
  }

