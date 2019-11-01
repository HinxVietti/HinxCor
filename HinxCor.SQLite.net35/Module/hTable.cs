using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

public class hTable<T>
{
    private hHeader[] parms;

    public string DDL;

    public hTable(string tableName, hHeader[] parms, ref Func<SQLiteConnection> getConnFunc)
    {
        this.parms = parms;
        string ddl = string.Format(@"CREATE TABLE {0} ({1});", tableName, GetHeaderDesc(parms));
        DDL = ddl;

        using (var conn = getConnFunc())
        {
            conn.Open();
            var cmd = new SQLiteCommand(ddl, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    private string GetHeaderDesc(hHeader[] parms)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < parms.Length; i++)
        {
            sb.Append(parms[i].ToString());
            if (i + 1 < parms.Length)//有下一个值
                sb.Append(',');
        }
        return sb.ToString();
    }

}

