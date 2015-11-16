//XmlExtension.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 10/30/2015 2:52:23 PM 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
public class XmlExtension{
    private static XmlExtension _instance = null;

    public static XmlExtension GetInstance(){
        if( null == _instance ){
            _instance = new XmlExtension();
        }
        return _instance;
    }

    public string SavePath
    {
        get;
        set;
    }
    public string FileName
    {
        get;
        set;
    }

    public XmlExtension()
    {
        SetSavePathName("Root");
    }
    public void SetSavePathName(string fileName)
    {
        string dictoryPath = Application.dataPath + "/WuxingogoExtension/";
        FileName = fileName;
        SavePath = dictoryPath + fileName +".xml";
        
    }

    private XmlDocument xmlDoc = null;
    private XmlElement rootElement = null;
    public void TryWrite(Func<List<XmlElement>> element)
    {
        xmlDoc = new XmlDocument();
        rootElement = xmlDoc.CreateElement(FileName);

		List<XmlElement> array = element();
		foreach (var item in array) {
			rootElement.AppendChild(item);
        }
		
        xmlDoc.AppendChild(rootElement);

        xmlDoc.Save(SavePath);
    }

    

	public List<XmlElement> GenerateElements(Func<List<XmlElement>> func, XmlElement child){

		List<XmlElement> array = func();
		array.Add(child);
		return array;
    }

    public XmlElement GenerateDictionary(string name, Dictionary<string, string> dictory)
    {
        XmlElement child = xmlDoc.CreateElement(name);

        foreach (var item in dictory)
        {
            child.SetAttribute(item.Key, item.Value);
        }
        rootElement.AppendChild(child);
        return child;
    }
    /// <summary>
    /// 生成T集合的XML
    /// </summary>
    /// <typeparam name="T">集合的类型</typeparam>
    /// <param name="name">根节点的名字</param>
    /// <param name="list">集合对象</param>
    /// <param name="func">返回的集合</param>
    /// <returns></returns>
	public List<XmlElement> GenerateList1<T>(string name, List<T> list, Func<int, XmlElement, Dictionary<string, string>> func)
    {

    	List<XmlElement> array = new List<XmlElement>();

		
		for(int pos = 0; pos < list.Count; pos++){
			XmlElement element = xmlDoc.CreateElement(name + "_" + pos);
			Dictionary<string, string> dict = func(pos,element);
			foreach (var item in dict) {
				element.SetAttribute(item.Key, item.Value);
    		}
			array.Add(element);
		}
		return array;
    }

	public List<XmlElement> GenerateList<T>(string name, List<T> list, Func<int, Dictionary<string, string>> func)
    {

    	List<XmlElement> array = new List<XmlElement>();

		XmlElement element = xmlDoc.CreateElement(name);
		for(int pos = 0; pos < list.Count; pos++){
			Dictionary<string, string> dict = func(pos);

			foreach (var item in dict) {
				element.SetAttribute(item.Key, item.Value);
    		}
			array.Add(element);
		}
		return array;
    }

	public List<XmlElement> GenerateList(string name, Func<List<XmlElement>> func)
    {
        XmlElement child = xmlDoc.CreateElement(name);

		List<XmlElement> elements = func();
		foreach (var item in elements) {
			child.AppendChild(item);
		}
		return elements;
    }

	public XmlElement SetAttribute(XmlElement element, Func<Dictionary<string, string>> func){
		Dictionary<string, string> dictionary = func();
		foreach (var item in dictionary) {
			element.SetAttribute(item.Key, item.Value);
		}
		return element;
    }
	public List<XmlElement> GenerateList<T>(List<T> list){

		List<XmlElement> array = new List<XmlElement>();


		foreach (var item in list) {
			XmlElement child = xmlDoc.CreateElement(item.ToString());
			array.Add(child);
		}

		return array;
	}

    public XmlElement AppendChild(XmlElement element, List<XmlElement> childs){
		foreach (var item in childs) {
    		element.AppendChild(item);
		}
		return element;
    }

    //public List<XmlElement> TryRead(Action<int,XmlElement> action)
    //{
    //    xmlDoc = new XmlDocument();
    //    xmlDoc.Load(SavePath);

    //    rootElement = (XmlElement)xmlDoc.SelectSingleNode(FileName);

    //    for (int pos = 0; pos < rootElement.ChildNodes.Count; pos++)
    //    {
    //        action(pos, (XmlElement)rootElement.ChildNodes[pos]);
    //    }
    //}


    public Dictionary<string, string> LoadDictionary(string treeName ,string elementName)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
		XmlNodeList list = rootElement.GetElementsByTagName(treeName);
		for (int pos = 0; pos < list.Count; pos++) {
			//  TODO loop in list.Count
			XmlElement xe = (XmlElement)list[pos];
			dict.Add(elementName + "_" + pos, xe.GetAttribute(elementName));
		}
        return dict;
    }

    private void ElementAttribute(XmlElement __element, string attribute, string value)
    {
        __element.SetAttribute(attribute, value);
    }
    private void ElementChild(XmlElement __element, string name)
    {
        xmlDoc.CreateElement(name);
        XElement contacts =
        new XElement("Contacts",
            new XElement("Contact",
                new XElement("Name", "Patrick Hines"),
                new XElement("Phone", "206-555-0144"),
                new XElement("Address",
                    new XElement("Street1", "123 Main St"),
                    new XElement("City", "Mercer Island"),
                    new XElement("State", "WA"),
                    new XElement("Postal", "68042")
                )
            )
        );
    }

    public XElement generateXElement(string name, string value)
    {
        return new XElement(name, value);
    }
    public XElement generateXElement(XName name, params object[] contents){
        return new XElement(name, contents);
    }
}