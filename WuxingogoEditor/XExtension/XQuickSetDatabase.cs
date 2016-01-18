using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Object = UnityEngine.Object;

public class XQuickSetDatabase : XBaseWindow
{
	
	Object dbFile = null;
	bool isDirty = true;
	List<string> allTable = new List<string>();
	
	List<string> allTableField = new List<string>();
	bool isShowTable = false;
	
	Dictionary<string, List<string>> tableString = new Dictionary<string, List<string>>();
	Dictionary<string, List<int>> tableInt = new Dictionary<string, List<int>>();
	Dictionary<string, List<float>> tableFloat = new Dictionary<string, List<float>>();
	string currTable = "";
	
	int dataTable = 0;
	
	/// <summary>
	///  ˝æ›ø‚¡¨Ω”∂®“Â
	/// </summary>
	private SqliteConnection dbConnection;
	
	/// <summary>
	/// SQL√¸¡Ó∂®“Â
	/// </summary>
	private SqliteCommand dbCommand;
	
	/// <summary>
	///  ˝æ›∂¡»°∂®“Â
	/// </summary>
	private SqliteDataReader dataReader;
	
	/// <summary>
	/// ππ‘Ï∫Ø ˝    
	/// </summary>
	/// <param name="connectionString"> ˝æ›ø‚¡¨Ω”◊÷∑˚¥Æ</param>
	
	
	[MenuItem("Wuxingogo/Wuxingogo XQuickSetDatabase ")]
	static void init()
	{
		InitWindow<XQuickSetDatabase>();
	}
	
	const string GET_ALL_TABLE = "select name from sqlite_master where type='table' order by name;";
	public override void OnXGUI()
	{
		//TODO List
		
		if (CreateSpaceButton("Clean"))
		{
			isDirty = true;
		}
		
		if (isDirty)
		{
			dbFile = null;
			dbFile = CreateObjectField("DBFile", dbFile);
			dbConnection = null;
			dataTable = 0;
		}
		
		if (null == dbFile)
		{
			CreateMessageField("Drag a db file.", MessageType.None);
		}
		else
		{
			if (!AssetDatabase.GetAssetPath(dbFile).Contains(".db"))
			{
				dbFile = null;
				Debug.Log("DB File 's Format Error");
				isDirty = true;
			}
			else if (isDirty)
			{
				isDirty = false;
				string appDBPath = AssetDatabase.GetAssetPath(dbFile);
				try
				{
					//ππ‘Ï ˝æ›ø‚¡¨Ω”
					dbConnection = new SqliteConnection("URI=file:" + appDBPath);
					//¥Úø™ ˝æ›ø‚
					dbConnection.Open();
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
				
				allTable.Clear();
				currTable = "";
			}
			
			if (null != dbConnection)
			{
				if (CreateSpaceButton("DataBase"))
				{
					allTable.Clear();
					
					SqliteDataReader recordTableReader = ExecuteQuery(GET_ALL_TABLE);
					while (recordTableReader.Read())
					{
						allTable.Add(recordTableReader.GetString(recordTableReader.GetOrdinal("name")));
					}
					dataTable = 0;
				}
				for (int pos = 0; pos < allTable.Count; pos++)
				{
					if (CreateSpaceButton(allTable[pos]))
					{
						dataTable = 0;
						tableFloat.Clear();
						tableInt.Clear();
						tableString.Clear();
						allTableField.Clear();
						string sql = "SELECT * FROM " + allTable[pos];
						SqliteDataReader recordTableReader = ExecuteQuery(sql);
						currTable = allTable[pos];
						allTable.Clear();
						// DataTable t = recordTableReader.GetSchemaTable();
						int count = recordTableReader.VisibleFieldCount;
						for (int idx = 0; idx < count; idx++) {
							//  TODO loop in count
							string fieldTypeName = recordTableReader.GetName(idx);
							Debug.Log("fieldTypeName" + fieldTypeName);
						}
						
						
						isShowTable = true;
						while (recordTableReader.Read())
						{
							dataTable++;
							for (int root = 0; root < count; root++)
							{
								//							Debug.Log( recordTableReader.GetName(root));
								string field = recordTableReader.GetName(root);
								
								if (recordTableReader.GetFieldType(root) == typeof(System.Int64))
								{
									if (!tableInt.ContainsKey(field)) tableInt.Add(field, new List<int>());
									tableInt[field].Add(recordTableReader.GetInt32(root));
									//								CreateLabel(recordTableReader.GetName(root));
								}
								else if (recordTableReader.GetFieldType(root) == typeof(System.String))
								{
									if (!tableString.ContainsKey(field)) tableString.Add(field, new List<string>());
									string s = recordTableReader.GetString(root).ToString();
									tableString[field].Add(s);
									//								CreateLabel(recordTableReader.GetName(root));
								}
								else if (recordTableReader.GetFieldType(root) == typeof(System.Double))
								{
									//								CreateLabel(recordTableReader.GetName(root));
									if (!tableFloat.ContainsKey(field)) tableFloat.Add(field, new List<float>());
									tableFloat[field].Add(recordTableReader.GetFloat(root));
								}else{
									Debug.Log("recordTableReader.GetFieldType(root) is " + recordTableReader.GetFieldType(root).ToString());
								}
								if (!allTableField.Contains(field))
									allTableField.Add(field);
								//							CreateLabel(recordTableReader.GetName(root));
							}
						}
						recordTableReader.Close();
						
						//						 while(recordTableReader.Read()){
						//						 	Debug.Log(recordTableReader.GetInt32 (recordTableReader.GetOrdinal ("id")));
						//						 }
						
						
					}
				}
				
//				if (isShowTable)
//				{
//					BeginHorizontal();
//					for (int pos = 0; pos < allTableField.Count; pos++)
//					{
//						CreateLabel(allTableField[pos]);
//					}
//					EndHorizontal();
//					
//					for (int root = 0; root < dataTable; root++)
//					{
//						
//						
//						BeginHorizontal();
//						for (int pos = 0; pos < allTableField.Count; pos++)
//						{
//							//						CreateLabel( allTableField[pos] );
//							//						CreateStringField(,tableString[pos]
//							if(tableInt[allTableField[pos]][root] != null){
//							
//							if (tableInt.ContainsKey(allTableField[pos]))
//							{
//								tableInt[allTableField[pos]][root] = EditorGUILayout.IntField(tableInt[allTableField[pos]][root]);
//							}
//							else if (tableString.ContainsKey(allTableField[pos]))
//							{
//								tableString[allTableField[pos]][root] = EditorGUILayout.TextField(tableString[allTableField[pos]][root]);
//							}
//							else if (tableFloat.ContainsKey(allTableField[pos]))
//							{
//								tableFloat[allTableField[pos]][root] = EditorGUILayout.FloatField(tableFloat[allTableField[pos]][root]);
//							}
//							}
//							//						tableInt[pos] = EditorGUILayout.IntField(tableInt[pos]);
//							//						tableFloat[pos] = EditorGUILayout.FloatField(tableFloat[pos]);
//						}
//						EndHorizontal();
//					}
//					
//				}
//				
			}
			
			
			
			
		}
	}
	
	/// <summary>
	/// ÷¥––SQL√¸¡Ó
	/// </summary>
	/// <returns>The query.</returns>
	/// <param name="queryString">SQL√¸¡Ó◊÷∑˚¥Æ</param>
	public SqliteDataReader ExecuteQuery(string queryString)
	{
		dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText = queryString;
		dataReader = dbCommand.ExecuteReader();
		return dataReader;
	}
	
	
}
