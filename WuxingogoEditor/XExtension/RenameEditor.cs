using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class RenameEditor : XBaseWindow {


	[MenuItem( "Tools/Rename Window" )]
	public static void InitWindow()
	{
		InitWindow<RenameEditor>();
	}

	public int headIndex = 0;
	public int interval = 1;
	public string ruleContent = "Example{0}";
	public GameObject[] renameObjects = null;
	public override void OnXGUI()
	{
		base.OnXGUI();


		headIndex = CreateIntField( "Head Index", headIndex );

		interval = CreateIntField( "Interval", interval );

		ruleContent = CreateStringField( "Rule", ruleContent );
		
		DoButton( "Excute", Excute );
		
		
	}

	void Excute()
	{
		renameObjects = Selection.gameObjects;

		Undo.RecordObjects( renameObjects, "RenameObject" );
		var transforms = new List<Transform>();
		
		for( int i = 0; i < renameObjects.Length; i++ )
		{
			var gameObject = renameObjects[ i ];

			transforms.Add( gameObject.transform );
		}

		BubbleSort( transforms );

		for( int i = 0; i < transforms.Count; i++ )
		{
			transforms[ i ].name = string.Format( ruleContent, headIndex + interval * i );
		}
	}

	public void BubbleSort(List<Transform> data)
	{
		Transform temp = null;
		for (int i = 0; i < data.Count - 1; i++)
		{
			for (int j = i + 1; j < data.Count; j++)
			{
				if (data[i].GetSiblingIndex() > data[j].GetSiblingIndex()) 
				{

					temp = data[i]; 
					data[i] = data[j]; 
					data[j] = temp; 
				}
			}
		}
	}
}
