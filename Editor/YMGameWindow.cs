using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
#if YMProject
using net;
public class YMGameWindow : XBaseWindow
{
    static List<Object> quickObjects = new List<Object>();
    static int quickNum = 0;
    static bool isToolsConfig = false;
    static XmlDocument xmlDoc;
    static string filepath = Application.dataPath + @"/WuxingogoExtension/YMGame.xml";
    static bool isLoginConfig = false;
    static string userName = "";
    static string userPassword = "";
    [MenuItem( "Wuxingogo/Wuxingogo YMGameWindow " )]
    static void init()
    {
        isToolsConfig = EditorPrefs.GetBool( "isToolsConfig", false );
        isLoginConfig = EditorPrefs.GetBool( "isLoginConfig", false );
        userName = EditorPrefs.GetString( "userName", "" );
        userPassword = EditorPrefs.GetString( "userPassword", "" );
        
        GetDataFromXml();
        YMGameWindow window = ( YMGameWindow )EditorWindow.GetWindow( typeof( YMGameWindow ) );
    }

    public override void OnXGUI()
    {
        //TODO List
        isToolsConfig = EditorGUILayout.Foldout( isToolsConfig, "Quick Seach Object" );    
        if( isToolsConfig )
        {
            ShowSeachPanel();
        }
        isLoginConfig = EditorGUILayout.Foldout( isLoginConfig, "Quick Login" );
        if( isLoginConfig )
        {
            ShowLoginPanel();
        }
        if( GUI.changed )
        {
            EditorPrefs.SetBool( "isToolsConfig", isToolsConfig );
            EditorPrefs.SetBool( "isLoginConfig", isLoginConfig );
        }
    }

    void OnSelectionChange()
    {
        //TODO List

    }
    private void ExportXml()
    {
        xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement( "root" );
        for( int pos = 0; pos < quickObjects.Count; pos++ )
        {
            if( null != quickObjects[pos] )
            {
                XmlElement child = xmlDoc.CreateElement( quickObjects[pos].name );
                child.SetAttribute( "instanceID", quickObjects[pos].GetInstanceID().ToString() );
                child.SetAttribute( "path", AssetDatabase.GetAssetPath(quickObjects[pos]) );
                root.AppendChild( child );
            }
        }
        xmlDoc.AppendChild( root );

        xmlDoc.Save( filepath );
    }

    private static void CalculateArray()
    {
        if( quickObjects.Count > quickNum )
        {
            quickObjects.RemoveRange( quickNum - 1, quickObjects.Count - quickNum );
            quickObjects.TrimExcess();
        }
        else
        {
            for( int pos = quickObjects.Count; pos < quickNum; pos++ )
            {
                quickObjects.Add( null );
            }
        }
    }

    public static void GetDataFromXml()
    {
        
        if( File.Exists( filepath ) )
        {
            xmlDoc=new XmlDocument();
            xmlDoc.Load(filepath);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode( "root" ).ChildNodes;
            quickNum = nodeList.Count;
            CalculateArray();
            for( int pos = 0; pos < nodeList.Count; pos++ )
            {
                XmlElement xe = (XmlElement)nodeList[pos];
                int instanceID = int.Parse( xe.GetAttribute( "instanceID" ) );
                string path = xe.GetAttribute( "path" );
                quickObjects[pos] = AssetDatabase.LoadAssetAtPath( path, typeof( Object ) );
            }
        }
        else
        {
            
        }
    }

    public void ShowSeachPanel()
    {
        BeginHorizontal();
        quickNum = CreateIntField( "Quick Object Number", quickNum );
        if( CreateSpaceButton( "apply" ) )
        {
            CalculateArray();
            ExportXml();
        }
        if( CreateSpaceButton( "import" ) )
        {
            GetDataFromXml();
        }
        EndHorizontal();

        for( int pos = 0; pos < quickObjects.Count; pos++ )
        {
            quickObjects[pos] = CreateObjectField( quickObjects[pos] == null ? "None" : quickObjects[pos].GetType().ToString(), quickObjects[pos] );
        }
    }
    public void ShowLoginPanel()
    {
        userName = CreateStringField( "user name", userName );
        userPassword = CreateStringField( "user password", userPassword );
        BeginHorizontal();
        if( CreateSpaceButton("Login") )
        {
            EditorPrefs.SetString( "userName", userName );
            EditorPrefs.SetString( "userPassword", userPassword );
            CGPlayerLogin msg = new CGPlayerLogin( userName, userPassword, 0, "" );
            YMSocket.Instance.SendMessage( msg );
        }
        if( CreateSpaceButton( "Reset" ) )
        {
            userName = EditorPrefs.GetString( "userName", "" );
            userPassword = EditorPrefs.GetString( "userPassword", "" );
        }
        EndHorizontal();
       
    }
}
#endif