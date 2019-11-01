using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading;

public class SQLiteDao : IDAO
{
    private Func<SQLiteConnection> GetConnFunc;

    /// <summary>
    /// 请先校验文件是否存在;
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="passwd"></param>
    public SQLiteDao(string fileName, string passwd = "")
    {
        this.GetConnFunc = () =>
        {
            var conn = new SQLiteConnection(string.Format("url=file:{0}", fileName));
            if (!string.IsNullOrEmpty(passwd))
                conn.SetPassword(passwd);
            return conn;
        };

        //conn = string.Format("url=file:{0}{1}", dbFile, string.IsNullOrEmpty(passwd) ? "" 
        //: string.Format(";Password={0}", passwd));
        //if (!File.Exists(dbFile))
        //{
        //    //此处应该Throw
        //    //Create db file.
        //    using (var fs = File.Create(dbFile))
        //    {
        //        Thread.Sleep(20);
        //    }
        //    if (!string.IsNullOrEmpty(passwd))
        //    {
        //        try
        //        {
        //            SQLiteConnection con = new SQLiteConnection(conn);
        //            con.Open();
        //            con.ChangePassword(passwd);
        //            con.Close();
        //        }
        //        catch (SQLiteException e)
        //        {
        //            throw e;
        //        }
        //    }
        //}
    }


    public hTable<T> CreateTable<T>(string tableName, params hHeader[] extraparms)
    {
        return new hTable<T>(tableName, extraparms, ref GetConnFunc);
    }

    public int Delete<T>(hTable<T> table, hFilter<T> row)
    {
        throw new NotImplementedException();
    }

    public int Drop<T>(hTable<T> table)
    {
        throw new NotImplementedException();
    }

    public void Insert<T>(hTable<T> table, hRow<T> row)
    {
        throw new NotImplementedException();
    }

    public hResult<T> Serach<T>(hTable<T> table, hRow<T> row)
    {
        throw new NotImplementedException();
    }

    public hResult<T> Update<T>(hTable<T> table, hRow<T> row)
    {
        throw new NotImplementedException();
    }
}

