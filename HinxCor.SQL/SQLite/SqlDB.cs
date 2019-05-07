using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

public class SqlDB
{
    private SQLiteConnection dbConnection;
    public SQLiteConnection SQLiteConnection => dbConnection;

    public SqlDB(string dlPath)
    {
        if (File.Exists(dlPath) == false) throw new FileNotFoundException(dlPath);
        try
        {
            dbConnection = new SQLiteConnection(string.Format("data source={0}", dlPath));
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public SQLiteHelper CreateHelper()
    {
        return new SQLiteHelper(dbConnection);
    }

    public SQLiteDataReader GetTable(string tableName)
    {
        var cmd = dbConnection.CreateCommand();
        cmd.CommandText = string.Format("select * from {0}", tableName);
        return cmd.ExecuteReader();
    }

}

