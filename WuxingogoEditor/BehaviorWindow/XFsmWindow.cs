//
//  FsmWindow.cs
//
//  Author:
//       ${wuxingogo} <52111314ly@gmail.com>
//
//  Copyright (c) 2016 ly-user
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.


namespace FsmEditor
{
    using System;
    using UnityEditor;
    using wuxingogo.Fsm;
    using System.Collections;
    using UnityEngine;
    using Object = UnityEngine.Object;
    using System.Collections.Generic;

    [CustomEditor( typeof( XFsmComponent ) )]
	public class XFsmEditor : XBaseEditor
	{
		public override void OnXGUI()
		{
			DoButton( "Open In Fsm Editor", () => XFsmWindow.GetInstance().FSM = (XFsmComponent)target );
		}
	}


	public class XFsmWindow : XBaseWindow
	{
		
		private float ToolbarHeight = 0;
		private Vector2 scrollPos = Vector2.zero;
		[MenuItem( "Wuxingogo/Wuxingogo XBehaviorEditor" )]
		static void Init()
		{
			XBaseWindow.InitWindow<XFsmWindow>();
		}

		public XFsmComponent FSM {
			get {
				return fsm;
			}
			set {				
				if( fsm != value )
					isDirty = true;
				fsm = value;
			}
		}

		public static XFsmWindow GetInstance()
		{
			return XBaseWindow.InitWindow<XFsmWindow>();
		}

		private XFsmComponent fsm = null;
		public bool isDirty = false;

		public override bool IsAutoScroll {
			get {
				return false;
			}
		}

		public override void OnXGUI()
		{
			FSM = CreateObjectField( "", FSM ) as XFsmComponent;

			if( null != FSM ) {

				DoButton( "Save A Template", SaveTemplate );
			}

//			Profiler.BeginSample("StateMachineMaker");
//            Event e = Event.current;
//			if (e.type == EventType.Repaint)
//            {
//				Rect rect = GUILayoutUtility.GetLastRect();
//				ToolbarHeight = rect.yMax + rect.height / 2;
//				Styles.BackgroudStyle.Draw(new Rect(0, ToolbarHeight, position.width, position.height - ToolbarHeight),
//                    false, false, false, false);
//                DrawGrid();
//            }
//


			DrawRectabgle();
//            Profiler.EndSample();
		}
		private Vector2 dragPos;
        private List<Vector3[]> drawRect = new List<Vector3[]>();
		private void DrawRectabgle()
        {
//            Event e = Event.current;
//            if (e.type == EventType.MouseDrag)
//            {
//                Vector2 mousePos = e.mousePosition;
//
//                if (dragPos == Vector2.zero)
//                {
//                    dragPos = mousePos;
//                }
//
//                Vector3[] vector3s = new Vector3[]
//                {
//                    mousePos, new Vector2(dragPos.x, mousePos.y), dragPos,
//                    new Vector2(mousePos.x, dragPos.y)
//                };
//                drawRect.Add(vector3s);
//                Repaint();
//            }
//
//
//
//            if (e.type == EventType.Repaint)
//            {
//                if (drawRect.Count != 0)
//                {
//                    DrawSolidRectangleWithOutline(drawRect[0], new Color32(90, 105, 126, 255), Color.white);
//
////                    Rect rect = Vector3sToRect(drawRect[0]);
////                    foreach (S state in stateMachine.GetAllStates())
////                    {
////                        Rect pos = state.position;
////                        if (rect.Contains(pos))
////                        {
////                            if (!forcusedStates.Contains(state))
////                                ArrayUtility.Add(ref forcusedStates, state);
////                        }
////                    }
//
//                    if (drawRect.Count != 1)
//                        drawRect.RemoveAt(0);
//
//                    Repaint();
//                }
//            }

//			scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos,
//                    new Rect(0, 0, position.width * 2f, position.height * 2f), false, false);
            OnGraphGUI();
//            GUI.EndScrollView();
//            if (e.type == EventType.MouseUp)
//            {
//                drawRect = new List<Vector3[]>();
//                dragPos = Vector2.zero;
//            }
        }

		private Rect Vector3sToRect(Vector3[] vactor3s)
        {
            float left = scrollPos.x + (vactor3s[2].x - vactor3s[0].x > 0 ? vactor3s[0].x : vactor3s[2].x);
            float top = scrollPos.y + (vactor3s[2].y - vactor3s[0].y > 0 ? vactor3s[0].y : vactor3s[3].y);
            float width = Mathf.Abs(vactor3s[1].x - vactor3s[0].x);
            float height = Mathf.Abs(vactor3s[2].y - vactor3s[1].y);
            return new Rect(left, top, width, height);
        }

		private void DrawSolidRectangleWithOutline(Vector3[] verts, Color faceColor, Color outlineColor)
        {
            GL.PushMatrix();

            GL.Begin(GL.TRIANGLES);
            GL.Color(faceColor);
            GL.Vertex(verts[0]);
            GL.Vertex(verts[1]);
            GL.Vertex(verts[2]);
            GL.Vertex(verts[0]);
            GL.Vertex(verts[2]);
            GL.Vertex(verts[3]);
            GL.End();

            GL.Begin(GL.LINES);
            GL.Color(outlineColor);
            for (int i = 0; i < 4; i++)
            {
                GL.Vertex(verts[i]);
                GL.Vertex(verts[(i + 1) % 4]);
            }
            GL.End();
            GL.PopMatrix();

        }

		public override object[] closeRecordArgs {
			get {
				return new object[]{ FSM };
			}
		}

		private void OnGraphGUI(){
			BeginWindows();
			for( int pos = 0; pos < nodeWindos.Count; pos++ ) {
				//  TODO loop in nodeWindos.Count
				nodeWindos[pos].Draw( pos );
			}
			EndWindows();
		}

		private void DrawGrid()
        {
            if (Event.current.type != EventType.Repaint)
                return;
            DrawGridLines(12, EditorGUIUtility.isProSkin ? new Color32(32, 32, 32, 255) : new Color32(60, 60, 60, 255));
            DrawGridLines(120, Color.black);
        }


        private void DrawGridLines(float gridSize, Color gridColor)
        {
			float xMax = position.width * 5, xMin = 0, yMax = position.height * 5, yMin = ToolbarHeight;
            Handles.color = gridColor;
            float x = xMin - xMin % gridSize;
            while (x < xMax)
            {
                Handles.DrawLine(new Vector2(x, yMin), new Vector2(x, yMax));
                x += gridSize;
            }
            float y = yMin - yMin % gridSize;
            while (y < yMax)
            {
                Handles.DrawLine(new Vector3(xMin, y), new Vector3(xMax, y));
                y += gridSize;
            }
        }

		public override void OnInitialization(params object[] args)
		{
			if( args.Length > 0 )
				FSM = args[0] as XFsmComponent;

			IList states = FSM.FsmStates<XFsmStateComponent>();
			for( int pos = 0; pos < states.Count; pos++ ) {
				//  TODO loop in FSM.FsmStates<XFsmStateComponent>()
				nodeWindos.Add( new XFsmStateWindow(states[pos] as XFsmStateComponent) );
			}
		}

		private List<XFsmStateWindow> nodeWindos = new List<XFsmStateWindow>();

		void SaveTemplate()
		{
			IList fsmStates = FSM.FsmStates<XFsmStateComponent>();
			XTemplateComponent sc = XTemplateComponent.Create();


			string path = EditorUtility.SaveFilePanel( "Create A Templete", XEditorSetting.ProjectPath, "123" + ".asset", "asset" );
			if( path == "" )
				return;
		
			path = FileUtil.GetProjectRelativePath( path ); 

			AssetDatabase.CreateAsset( sc, path );
			for( int pos = 0; pos < fsmStates.Count; pos++ ) {
				//  TODO loop in fsmStateS.Count
				XFsmStateComponent state = fsmStates[pos] as XFsmStateComponent;
				Logger.Log( "pos is : " + pos );

				state.name = state.GetType().ToString();
				AssetDatabase.AddObjectToAsset( state, path );

			}
			// Refresh/Reimport
			AssetDatabase.Refresh();
			AssetDatabase.ImportAsset( path );
			AssetDatabase.SaveAssets();
		}
	}
}

