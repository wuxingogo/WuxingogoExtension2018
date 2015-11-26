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
	/// 数据库连接定义
	/// </summary>
	private SqliteConnection dbConnection;

    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// 构造函数    
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>


    [MenuItem("Wuxingogo/Wuxingogo XQuickSetDatabase ")]
    static void init()
    {
        XQuickSetDatabase window = (XQuickSetDatabase)EditorWindow.GetWindow(typeof(XQuickSetDatabase));
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
                    //构造数据库连接
                    dbConnection = new SqliteConnection("URI=file:" + appDBPath);
                    //打开数据库
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
                                    tableInt[field].Add(recordTableReader.GetInt32(recordTableReader.GetOrdinal(field)));
                                    //								CreateLabel(recordTableReader.GetName(root));
                                }
                                else if (recordTableReader.GetFieldType(root) == typeof(System.String))
                                {
                                    if (!tableString.ContainsKey(field)) tableString.Add(field, new List<string>());
                                    tableString[field].Add(recordTableReader.GetString(recordTableReader.GetOrdinal(field)));
                                    //								CreateLabel(recordTableReader.GetName(root));
                                }
                                else if (recordTableReader.GetFieldType(root) == typeof(System.Double))
                                {
                                    //								CreateLabel(recordTableReader.GetName(root));
                                    if (!tableFloat.ContainsKey(field)) tableFloat.Add(field, new List<float>());
                                    tableFloat[field].Add(recordTableReader.GetFloat(recordTableReader.GetOrdinal(field)));
                                }
                                if (!allTableField.Contains(field))
                                    allTableField.Add(field);
                                //							CreateLabel(recordTableReader.GetName(root));
                            }
                        }
                        isShowTable = true;
                        //						 while(recordTableReader.Read()){
                        //						 	Debug.Log(recordTableReader.GetInt32 (recordTableReader.GetOrdinal ("id")));
                        //						 }


                    }
                }

                if (isShowTable)
                {
                    BeginHorizontal();
                    for (int pos = 0; pos < allTableField.Count; pos++)
                    {
                        CreateLabel(allTableField[pos]);
                    }
                    EndHorizontal();

                    for (int root = 0; root < dataTable; root++)
                    {


                        BeginHorizontal();
                        for (int pos = 0; pos < allTableField.Count; pos++)
                        {
                            //						CreateLabel( allTableField[pos] );
                            //						CreateStringField(,tableString[pos]
                            if (tableInt.ContainsKey(allTableField[pos]))
                            {
                                tableInt[allTableField[pos]][root] = EditorGUILayout.IntField(tableInt[allTableField[pos]][root]);
                            }
                            else if (tableString.ContainsKey(allTableField[pos]))
                            {
                                tableString[allTableField[pos]][root] = EditorGUILayout.TextField(tableString[allTableField[pos]][root]);
                            }
                            else if (tableFloat.ContainsKey(allTableField[pos]))
                            {
                                tableFloat[allTableField[pos]][root] = EditorGUILayout.FloatField(tableFloat[allTableField[pos]][root]);
                            }
                            //						tableInt[pos] = EditorGUILayout.IntField(tableInt[pos]);
                            //						tableFloat[pos] = EditorGUILayout.FloatField(tableFloat[pos]);
                        }
                        EndHorizontal();
                    }

                }

            }




        }
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }


}

