using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class XData : ScriptableObject{
//	[System.Serializable]
//	public List<XDataModel<object>> m_list = new List<XDataModel<object>>();
	public XDataModel[] Array = null;

	public XData(){
	}
}

[System.Serializable]
public class XDataModel{
	
	public string _title = "" ;
	
	public XDataModel _Child = null;
	
	
	private string _StrValue;
	private int _IntValue;
	private float _FloatValue;
	
//	private DataType _
	
}

public enum DataType{
	Int,
	String,
	Float
}