using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

//Disclaimer: Code originates from SAE lecture: Datenbanken
public class SQLite : MonoBehaviour
{
    SqliteConnection dbConnection = null;
    // Start is called before the first frame update
    void Start()
    {
        //Creating database as file in %Appdata% @: C:\Users\Franz Surface6Pro\AppData\LocalLow\DefaultCompany\UnitySQL
        string connection = "URI=file:" + Application.persistentDataPath + "/MyDataBase.sqlite";
        
        //Creating connection to DB
        dbConnection = new SqliteConnection(connection);
        
        //Opening connection
        dbConnection.Open();
        Debug.Log($"Connection string: {connection} | State: {dbConnection.State}");

        //Create table
        string createTable = "CREATE TABLE IF NOT EXISTS highscores (id INTEGER PRIMARY KEY, name TEXT, score INTEGER)";
        ExecuteCommand(createTable);

        //Add data to table
        string insertData1 = "INSERT INTO highscores (name, score) VALUES (\"peter\", 5340)";
        string insertData2 = "INSERT INTO highscores (name, score) VALUES (\"hans\", 2305)";
        ExecuteCommand(insertData1);
        ExecuteCommand(insertData2);

        //Print data
        IDbCommand readCmd = dbConnection.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM highscores";
        readCmd.CommandText = query;
        reader = readCmd.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log($"ID: {reader["id"]} name: {reader["name"]} score: {reader["score"]}");
        }
        dbConnection.Close();
    }

    private void ExecuteCommand(string str)
    {
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = str;
        cmd.ExecuteNonQuery();
        Debug.Log($"Executed Command: {cmd.CommandText}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
