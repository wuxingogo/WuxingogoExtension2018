using UnityEngine;
using System.Collections;
using wuxingogo.Runtime;
using Xingyu.Tools;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;


public class TestBinary : XMonoBehaviour{

	public float speed = 1;
	public float targetX = 0;
	
	public Test t = null;
	// Use this for initialization
	void Start () {
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER","yes");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * speed), 0, 0);
	}
	[XAttribute("")]
	public void OutputFile(){
		FileInfo file = new FileInfo("Assets/TestStream.text");  
		StreamWriter sw = file.CreateText();
		Stream sm = sw.BaseStream;
		StreamUtils.Write(sm, ObjectToByteArray(t));
		sm.Close();
		sw.Close();
	}
	
	[XAttribute("")]
	public void InputFile(){
		FileInfo file = new FileInfo("Assets/TestStream.text");  
		FileStream fs = file.OpenRead();
		
		
		ByteArrayToObject(StreamToBytes(fs));
		
		fs.Close();
	}
	
	
	// Convert an object to a byte array
	private byte[] ObjectToByteArray(object obj)
	{
		if(obj == null)
			return null;
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		bf.Serialize(ms, obj);
		return ms.ToArray();
	}
	
	// Convert a byte array to an Object
	private object ByteArrayToObject(byte[] arrBytes)
	{
		MemoryStream memStream = new MemoryStream();
		BinaryFormatter binForm = new BinaryFormatter();
		memStream.Write(arrBytes, 0, arrBytes.Length);
		memStream.Seek(0, SeekOrigin.Begin);
		object obj = (object) binForm.Deserialize(memStream);
		return obj;
	}

	/// 将 Stream 转成 byte[]  
	
	private byte[] StreamToBytes(Stream stream)   
	{   
		byte[] bytes = new byte[stream.Length];   
		stream.Read(bytes, 0, bytes.Length);   
		// 设置当前流的位置为流的开始   
		stream.Seek(0, SeekOrigin.Begin);   
		return bytes;   
	}  
	
}

[Serializable]
public class Test : object{
	
	public int pos = 3;
	public string name = "Name";
}
