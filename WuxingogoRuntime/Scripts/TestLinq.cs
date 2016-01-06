using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TestLinq : MonoBehaviour {

	public List<Student> source = new List<Student>();
	public List<Student> dest = new List<Student>();
	// Use this for initialization
	void Start () {

		var query = from student in source
			orderby student.ID ascending
			            select student;

		
		foreach( var item in query ) {
			dest.Add(item);
		}


		var dicts = new Dictionary<int, string>();
 
	    dicts.Add(9, "Jack");
	    dicts.Add(13, "Tom");
	    dicts.Add(5, "Tod");
	    dicts.Add(2, "Alics");
	 
	    foreach (var item in dicts.OrderByDescending(n=>n.Key))
	    {
	        Debug.Log(item.Value);
	    }
	}
}

[System.Serializable]
public class Student{
	public int ID;
}
