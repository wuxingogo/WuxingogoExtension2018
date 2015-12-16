using UnityEngine;
using System.Collections;

public class TestSQLHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(DataTableManager.CharacterGradeTable.FindOneByOneKey(1)["f_dodge"].OutInt);
	}

}
