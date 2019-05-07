using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class SQLiteHelper
{
    /// <summary> 
    /// 数据库连接定义
    /// </summary> 
    private SQLiteConnection dbConnection;
    /// <summary> 
    /// SQL命令定义 
    /// </summary> 
    private SQLiteCommand dbCommand;
    /// <summary> 
    /// 数据读取定义 
    /// </summary> 
    private SQLiteDataReader dataReader;
    /// <summary> 
    /// 构造函数   
    /// </summary> 
    /// <param name="connectionString">数据库连接字符串</param> 
    public SQLiteHelper(string connectionString)
    {
        try
        {
            //构造数据库连接 
            dbConnection = new SQLiteConnection(connectionString);
            //打开数据库 
            dbConnection.Open();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// COMM
    /// </summary>
    /// <param name="connectionString"></param>
    public SQLiteHelper(SQLiteConnection connectionString)
    {
        try
        {
            //构造数据库连接 
            dbConnection = new SQLiteConnection(connectionString);
            //打开数据库 
            dbConnection.Open();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary> 
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SQLiteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }
    /// <summary> 
    /// 关闭数据库连接 
    /// </summary>
    public void CloseConnection()
    {
        //销毁Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;
        //销毁Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;
        //销毁Connection 
        if (dbConnection != null)
        { dbConnection.Close(); }
        dbConnection = null;
    }
    /// <summary> /// 读取整张数据表 /// </summary> 
    /// <returns>The full table.</returns> 
    /// <param name="tableName">数据表名称</param>
    public SQLiteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// 向指定数据表中插入数据 
    /// </summary>
    /// <returns>The values.</returns> 
    /// <param name="tableName">数据表名称</param> 
    /// <param name="values">插入的数值</param> 
    public SQLiteDataReader InsertValues(string tableName, string[] values)
    {
        //获取数据表中字段数目 
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SQLiteException("values.Length!=fieldCount");
        }
        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// 更新指定数据表内的数据 
    /// </summary> 
    /// <returns>The values.</returns> 
    /// <param name="tableName">数据表名称</param> 
    /// <param name="colNames">字段名</param> 
    /// <param name="colValues">字段名对应的数据</param>
    /// <param name="key">关键字</param> 
    /// <param name="value">关键字对应的值</param>
    public SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string condition_key, string condition_operation, string condition_value)
    {
        //当字段名称和字段数值不对应时引发异常 
        if (colNames.Length != colValues.Length) { throw new SQLiteException("colNames.Length!=colValues.Length"); }
        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
            queryString += ", " + colNames[i] + "=" + colValues[i];
        queryString += " WHERE " + condition_key + condition_operation + condition_value;
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// 删除指定数据表内的数据
    /// </summary> 
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param> 
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param> 
    public SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }
        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// 删除指定数据表内的数据 
    /// </summary> 
    /// <returns>The values.</returns> 
    /// <param name="tableName">数据表名称</param> 
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param> 
    public SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }
        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// 创建数据表 
    /// </summary> +
    /// <returns>The table.</returns> 
    /// <param name="tableName">数据表名</param>
    /// <param name="colNames">字段名</param> 
    /// <param name="colTypes">字段名类型</param>
    public SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }
    /// <summary> 
    /// Reads the table.
    /// </summary> 
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param> 
    /// <param name="items">Items.</param> 
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param> 
    /// <param name="colValues">Col values.</param> 
    public SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}


public class SQLite
{
    /// <summary>
    /// TIME: 从 1970-01-01 00:00:00 UTC 算起的秒数。
    /// </summary>
    public const string Integer = "INTEGER";
    /// <summary>
    /// int 64
    /// </summary>
    public const string BigInt = "BIGINT";
    /// <summary>
    /// ByteArray
    /// </summary>
    public const string Blod = "BLOB";
    /// <summary>
    /// bool
    /// </summary>
    public const string Bool = "BOOLEAN";
    /// <summary>
    /// char
    /// </summary>
    public const string Char = "CHAR";
    /// <summary>
    /// date
    /// </summary>
    public const string Date = "DATE";
    /// <summary>
    /// date time
    /// </summary>
    public const string DateTime = "DATETIME";
    /// <summary>
    /// num
    /// </summary>
    public const string Decimal = "DECIMAL";
    /// <summary>
    /// double
    /// </summary>
    public const string Double = "DOUBLE";
    /// <summary>
    /// int
    /// </summary>
    public const string Int = "INT";
    /// <summary>
    /// auto type Affinity;
    /// 不做任何的转换，直接以该数据所属的数据类型进行存储。　　
    /// </summary>
    public const string None = "NONE";
    public const string Null = "NULL";
    /// <summary>
    /// num
    /// </summary>
    public const string Numeric = "NUMERIC";
    /// <summary>
    /// 从公元前 4714 年 11 月 24 日格林尼治时间的正午开始算起的天数。
    /// </summary>
    public const string Real = "REAL";
    /// <summary>
    /// string
    /// </summary>
    public const string String = "STRING";
    /// <summary>
    /// long string
    /// </summary>
    public const string Text = "TEXT";
    /// <summary>
    /// time
    /// </summary>
    public const string Time = "TIME";
    /// <summary>
    /// var char
    /// </summary>
    public const string VarChar = "VARCHAR";
}