//XmlExtensionEditor.cs
//
//Author:
//		Wuxingogo 52111314ly@gmail.com
//
//
//		Copyright (c) 11/14/2015 21:45:01 
//
//	You should have received a copy of the GNU Lesser General Public Licensealong with this program.
//	If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml.Linq;
using System.Xml;


namespace wuxingogo.Xml{
	public class XmlExtensionWindow : XBaseWindow {
		
		
		[MenuItem( "Wuxingogo/DataPaser/XmlExtensionWindow " )]
		static void Init()
		{
			XBaseWindow.InitWindow<XmlExtensionWindow>();
		}
		
		private XElement rootElement = null;
		private string fileName = string.Empty;
		private int level = 0;
		
		public override void OnXGUI()
		{
			base.OnXGUI();
			
			BeginHorizontal();
			if(CreateSpaceButton("Open")){
				fileName = EditorUtility.OpenFilePanel("Open XmlFile", XEditorSetting.ProjectPath, "");
				
				if(!fileName.Equals(string.Empty)){
					rootElement = XElement.Load(fileName);
				
				}
				
			}
			if(CreateSpaceButton("Create")){
				rootElement = new XElement("RootElement");
			}
			EndHorizontal();
			
			if( null != rootElement ){
				DrawElement(rootElement);
				if(CreateSpaceButton("Save")){
					SaveElement();
				}

			}
		}
		
		void DrawElement(XElement element){
			level++;
			CreateSpaceBox();
			BeginHorizontal();
			CreateLabel("level_"+level.ToString());
			element.Name = CreateStringField(element.Name.ToString());
			if(CreateSpaceButton("Create Attribute")){
				//TODO LIST
				//Create new attribute 
			}
			if(CreateSpaceButton("Create Child")){
				XElement child = new XElement("Default1");
				element.Add(child);
			}
			if(CreateSpaceButton("Delete")){
				element.Remove();
			}
			EndHorizontal();
			
			var attributes = element.Attributes();
			foreach( var item in attributes ) {
				CreateLabel("Attr",item.Name.ToString());
				item.Value = CreateStringField("Name",item.Value);
			}
			
			var childs = element.Elements();
			foreach( var item in childs ) {
				DrawElement(item);
			}
			
			CreateSpaceBox();
			level--;
		}
		
		void SaveElement(){
			
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.ConformanceLevel = ConformanceLevel.Auto;
			settings.IndentChars = "\t";
			settings.OmitXmlDeclaration = false;
			
			settings.NewLineOnAttributes = true;
			XmlWriter xw = XmlWriter.Create(XEditorSetting.ProjectPath + "/" + rootElement.Name + ".xml", settings);
			
			rootElement.Save(xw);
			xw.Flush();
			xw.Close();
			
		}
		
		
		
		
		
	}
}
