using System;
using wuxingogo.Node;
using UnityEngine;
using UnityEditor;
using wuxingogo.btFsm;

namespace wuxingogo.BTNode
{
	public class BTNode : DragNode
	{
		public BTState BtState = null;

		public BTNode( BTState btState )
		{
			this.BtState = btState;
		}

		#region implemented abstract members of DragNode

		public virtual Texture ThumbnailTexture {
			get {
				Texture thumb = AssetPreview.GetAssetPreview( Asset() );
				if( thumb == null ) {
					thumb = AssetPreview.GetMiniTypeThumbnail( typeof( GameObject ) );
				}
				return thumb;
			}
		}

		public override UnityEngine.Object Asset()
		{
			return BtState;
		}

		public override Rect Bounds {
			get {
				return BtState.Bounds;
			}
			set {
				BtState.Bounds = value;
			}
		}

		private Rect _Bounds = new Rect( 0, 0, 100, 100 );



		public string Title{
			get{
				return BtState.Name;
			}
		}


		public override void Draw()
		{
			GUI.Box( DrawBounds, ThumbnailTexture, XStyles.GetInstance().window );
			GUI.Label( DrawBounds, Title, XStyles.GetInstance().label );

			DrawChildNodes();

			if( Selected ) {
				EditorGUI.DrawRect( DrawBounds, new Color( 0, 0.5f, 0.5f, 0.3f ) );
			}
            if( BtState == BtState.Owner.currState )
            {
                EditorGUI.DrawRect( new Rect(DrawBounds.position, DrawBounds.size * 1.2f), new Color( 0, 0.3f, 0.5f, 0.3f ) );
            }

			for( int i = 0; i < BtState.totalEvent.Count; i++ ) {
				Rect button = new Rect( new Vector2( DrawBounds.xMin, DrawBounds.center.y + 50 + i * 20 ), new Vector2(100, 20) );
				var targetEvent = BtState.totalEvent[ i ];
				bool isCurrentEvent = targetEvent == BTEditorWindow.instance.currentEvent;
				if(GUI.Button( button, targetEvent.Name, XStyles.GetInstance().box )){
					BTEditorWindow.instance.SetCurrentEvent( targetEvent );
				}
				Rect mouseRect = new Rect( BTEditorWindow.instance.mousePosition, new Vector2( 10, 10 ) );
				if(isCurrentEvent){
					DrawBesizeFromRect( button, mouseRect );
				}
				if(targetEvent.TargetState != null)
				{
					var targetNode = BTEditorWindow.instance.FindBTNode( targetEvent.TargetState );
					DrawBesizeFromRect( button, targetNode.DrawBounds );
				}
			}

		}

		#endregion
	}
}

