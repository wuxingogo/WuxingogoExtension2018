using UnityEngine;
using UnityEditor;
using System.Collections;
using wuxingogo.Node;
using wuxingogo.Editor;
using System.Collections.Generic;
using wuxingogo.Runtime;
using wuxingogo.btFsm;
namespace wuxingogo.BTNode
{
	[CustomEditor(typeof(BTFsm), true)]
	public class BTEditor : XMonoBehaviourEditor
	{
		public override void OnXGUI()
		{
			base.OnXGUI();
			DoButton("Open In EditorWindow", () =>
			{
				BTEditorWindow.InitWindow<BTEditorWindow>();
				BTEditorWindow.target = target as BTFsm;
			});
		}
		[MenuItem("Assets/Create/BTFsm")]
		public static void CreateFsm()
		{
			var gos = Selection.gameObjects;
			if (gos.Length > 0)
			{
				var fsm = gos[0].AddComponent<BTFsm>();
				var startEvent = fsm.CreateEvent("GlobalStart");
				startEvent.TargetState = new BTState(fsm);
				startEvent.TargetState.GlobalEvent = startEvent;
				startEvent.TargetState.Name = "GlobalState";
				BTGenericMenu.AddStateToFsm( fsm, startEvent.TargetState );


			}
		}
	}



	public class BTEditorWindow : DTEditorWindow<BTFsm>
	{
		public BTEditorWindow()
		{
			instance = this;
		}
		[MenuItem("Wuxingogo/BTEditor/ Open Window")]
		static void Open()
		{
			instance = InitWindow<BTEditorWindow>();
		}

		[MenuItem("Wuxingogo/BTEditor/ Save Target")]
		static void Save()
		{
			if (target != null)
			{
				var fileName = "Assets/" + target.gameObject.name + ".prefab";
				if (target.template == null)
				{
					//					PrefabUtility.CreatePrefab( fileName, target.gameObject );

					BTTemplate asset = new BTTemplate(target);

					//					AssetDatabase.AddObjectToAsset( asset,fileName );
					AssetDatabase.CreateAsset(asset, "Assets/" + target.gameObject.name + ".asset");
					for (int i = 0; i < target.totalState.Count; i++)
					{
						var currState = target.totalState[i];
						AssetDatabase.AddObjectToAsset(currState, asset);
						for (int j = 0; j < currState.totalActions.Count; j++)
						{
							var currAction = currState.totalActions[j];
							AssetDatabase.AddObjectToAsset(currAction, currState);
						}

					}


					target.template = asset;
					EditorUtility.SetDirty(target);
				}
				else {
					BTTemplate asset = target.template;
					asset.totalState = target.totalState;

					for (int i = 0; i < target.totalState.Count; i++)
					{
						var currState = target.totalState[i];
						var path = AssetDatabase.GetAssetPath(currState);
						if (path == null)
						{
							AssetDatabase.AddObjectToAsset(currState, asset);
						}
						for (int j = 0; j < currState.totalActions.Count; j++)
						{
							var currAction = currState.totalActions[j];
							AssetDatabase.AddObjectToAsset(currAction, currState);
						}
					}
					EditorUtility.SetDirty(target.template);

				}
			}
			else {
				Debug.Log("Selet one component 'BTFsm' from BTEditor.");
			}
		}

		public static BTFsm target = null;

		List<BTNode> totalNode = new List<BTNode>();

		public BTEvent currentEvent = null;

		public static BTEditorWindow instance = null;

		#region implemented abstract members of DTEditorWindow

		public void Clear()
		{
			totalNode.Clear();
		}

		public override void OnInitialization(params object[] args)
		{
			base.OnInitialization(args);
			instance.Clear();
		}

		public override DragNode[] DragNodes()
		{
			if (target == null)
				return new DragNode[0];
			if (totalNode.Count > 0)
				return totalNode.ToArray();
			int count = target.totalState.Count;
			for (int i = 0; i < count; i++)
			{
				AddNewBTNode(target.totalState[i]);
			}
			return totalNode.ToArray();

		}

		public void AddNewBTNode(BTState newBTState)
		{
			totalNode.Add(new BTNode(newBTState));
		}

		public override void OnXGUI()
		{
			base.OnXGUI();
			if (target == null)
				return;
			DrawGlobalEvent();


		}

		public override void OnNoneSelectedNode()
		{
			Selection.objects = new Object[] { target.gameObject };

		}

		public override BTFsm targetNode
		{
			get
			{
				return target;
			}
			set
			{
				target = value;
			}
		}

		public void SetCurrentEvent(BTEvent currentEvent)
		{
			this.currentEvent = currentEvent;
		}

		public void RemoveState(BTNode currentState)
		{
			totalNode.Remove(currentState);
			target.RemoveState(currentState.BtState);
            DestroyImmediate( currentState.BtState, true );
			EditorUtility.SetDirty(target);
            
		}

		public override DTGenericMenu<BTFsm> GetGenericMenu()
		{
			return new BTGenericMenu();
		}

		protected override void OnSelectNode(DragNode node)
		{
			if (currentEvent != null)
			{
				var currentNode = node as BTNode;
				currentEvent.TargetState = currentNode.BtState;
                currentEvent = null;
				Dirty();
			}
			else {
				base.OnSelectNode(node);
			}

		}

		public BTNode FindBTNode(BTState findState)
		{
			for (int i = 0; i < totalNode.Count; i++)
			{
				if (findState == totalNode[i].BtState)
				{
					return totalNode[i];
				}
			}
			return null;
		}

		public void Dirty()
		{
			EditorUtility.SetDirty(target);
		}

		private void DrawEvent()
		{
			for (int i = 0; i < target.totalState.Count; i++)
			{
				var totalEvent = target.totalState[i].totalEvent;
				for (int j = 0; j < totalEvent.Count; j++)
				{
					var targetEvent = totalEvent[j];
					if (targetEvent == currentEvent)
					{
						//						DrawSelfEvent( i, j );
					}
				}
			}
		}

		private void DrawGlobalEvent()
		{
			for (int i = 0; i < target.totalEvent.Count; i++)
			{
				var targetEvent = target.totalEvent[i];
				for (int j = 0; j < totalNode.Count; j++)
				{
					var targetState = totalNode[j].BtState;
					if (targetEvent.TargetState == targetState)
					{
						DrawEvent(i, j);
						break;
					}
				}
			}
		}

		protected void DrawEvent(int eventIndex, int stateIndex)
		{
			var targetEvent = target.totalEvent[eventIndex];
			var targetState = totalNode[stateIndex];

			var bounds = new Rect(targetState.DrawBounds.position + new Vector2(0, -100), new Vector2(100, 50));
			GUI.Box(bounds, target.totalEvent[eventIndex].Name, XStyles.GetInstance().window);
			var arrow = new Rect(targetState.DrawBounds.position + new Vector2(50, -50), new Vector2(10, 50));
			GUI.Box(arrow, "", XStyles.GetInstance().window);
			//GUI.Box( bounds, "" );
			//GUI.Box( arrow, "" );

		}

		protected void DrawSelfEvent(int stateIndex, int eventIndex)
		{
			var targetState = target.totalState[stateIndex];
			var targetEvent = targetState.totalEvent[eventIndex];

			//			var bounds = new Rect( targetState.DrawBounds.position + new Vector2(0, -100), new Vector2(100, 50));
		}


		public static bool HasPrefab = false;

		void OnSelectionChange()
		{
			GameObject[] gameObjs = Selection.gameObjects;
			if (gameObjs.Length > 0)
			{
				HasPrefab = PrefabUtility.GetPrefabObject(gameObjs[0]) != null;
				//Debug.Log( BTFsm.HasPrefab );
			}
		}

		#endregion

	}

}
