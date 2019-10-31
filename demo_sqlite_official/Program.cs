using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace demo_SQLite_official
{
    class Program
    {
        static string cs = "URI=file:test.db";

        [STAThread]
        static void Main(string[] args)
        {
            //TestA();
            //TestB();
            //TestC();
            //TestD();
            //TestE();
            //TestF();
            //TestG();
            //TestH();
            //TestI();
            //TestJ();
            //TestK();
            //TestL();
            //TestN();
            //TestM();
            //TestO();
            //TestP();
            //TestQ();
            //TestR();
            //TestS();

            //TestT();
            //TestU();
            TestV();
            //TestW();
            //TestX();
            //TestY();
            //TestZ();

            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        static byte[] passwd = new byte[] { 125, 207, 231, 077, 070, 117, 207, 37, 65, 124 };

        /// <summary>
        /// 数据库加密
        /// </summary>
        private static void TestV()
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                con.ChangePassword(passwd);
                con.Close();
            }
            Console.WriteLine("Encrypted");
        }


        /// <summary>
        /// 测试Update 数据; view 只差不改
        /// </summary>
        static void TestU()
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                //string query = "update Cars " +
                //    "set Price = Price + 1000 " +
                //    "where Price < 100000;";
                string query = "update ExpCars " +
                    "set Price = Price + 1000 ";
                using (var cmd = new SQLiteCommand(query, con))
                {
                    var count = cmd.ExecuteNonQuery();
                    Console.WriteLine(string.Format("有{0}辆车, 涨价了1000", count));
                }

                con.Close();
            }
        }


        /// <summary>
        /// 测试查询视图
        /// </summary>
        static void TestT()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string query = "Select * from ExpCars";
                using (var cmd = new SQLiteCommand(query, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetInt32(0) + " "
                              + reader.GetString(1) + " " + reader.GetInt32(2));
                        }
                    }

                }
                con.Close();
            }
        }

        #region 官方例子

        /// <summary>
        /// RockBack
        /// </summary>
        static void TestS()
        {
            string cs = "URI=file:test.db";

            SQLiteConnection con = null;
            SQLiteTransaction tr = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(cs);

                con.Open();

                tr = con.BeginTransaction();
                cmd = con.CreateCommand();

                cmd.Transaction = tr;
                cmd.CommandText = "DROP TABLE IF EXISTS Friends";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE Friends(Id INTEGER PRIMARY KEY, 
                                Name TEXT)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Tom')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Rebecca')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jim')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Robert')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Julian')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jane')";
                cmd.ExecuteNonQuery();

                tr.Commit();

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                if (tr != null)
                {
                    try
                    {
                        tr.Rollback();
                    }
                    catch (SQLiteException ex2)
                    {
                        Console.WriteLine("Transaction rollback failed.");
                        Console.WriteLine("Error: {0}", ex2.ToString());
                    }
                    finally
                    {
                        tr.Dispose();
                    }
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (tr != null)
                {
                    tr.Dispose();
                }

                if (con != null)
                {
                    try
                    {
                        con.Close();

                    }
                    catch (SQLiteException ex)
                    {

                        Console.WriteLine("Closing connection failed.");
                        Console.WriteLine("Error: {0}", ex.ToString());

                    }
                    finally
                    {
                        con.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// 使用transition SQL
        /// </summary>
        static void TestR()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {

                        cmd.Transaction = tr;
                        cmd.CommandText = "DROP TABLE IF EXISTS Friends";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"CREATE TABLE Friends(Id INTEGER PRIMARY KEY, 
                                        Name TEXT)";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Tom')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Rebecca')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jim')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Robert')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Julian')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jane')";
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                }

                con.Close();
            }
        }

        /// <summary>
        /// 不使用transition SQL
        /// </summary>
        static void TestQ()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Friends";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Friends(Id INTEGER PRIMARY KEY, 
                                    Name TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Tom')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Rebecca')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Jim')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Robert')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends(Name) VALUES ('Julian')";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// 获取Table的列表
        /// </summary>
        static void TestP()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = @"SELECT name FROM SQLite_master
                WHERE type='table' ORDER BY name";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(rdr.GetString(0));
                        }
                    }
                }

                con.Close();
            }

        }


        /// <summary>
        /// 获取row信息  Table schema
        /// </summary>
        static void TestO()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Cars LIMIT 3";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable schemaTable = rdr.GetSchemaTable();

                        foreach (DataRow row in schemaTable.Rows)
                        {
                            foreach (DataColumn col in schemaTable.Columns)
                                Console.WriteLine(col.ColumnName + " = " + row[col]);
                            Console.WriteLine();
                        }
                    }
                }

                con.Close();
            }
        }

        /// <summary>
        /// 获取Query影响的数据量
        /// </summary>
        static void TestM()
        {
            string cs = "Data Source=:memory:";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {

                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "CREATE TABLE Friends(Id INT, Name TEXT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends VALUES(1, 'Tom')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends VALUES(2, 'Jane')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends VALUES(3, 'Rebekka')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends VALUES(4, 'Lucy')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Friends VALUES(5, 'Robert')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM Friends WHERE Id IN (3, 4, 5)";
                    int n = cmd.ExecuteNonQuery();

                    Console.WriteLine("The statement has affected {0} rows", n);
                }

                con.Close();
            }
        }

        /// <summary>
        /// 头部读取方案
        /// </summary>
        static void TestN()
        {

            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Cars LIMIT 5";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        Console.WriteLine(String.Format("{0, -3} {1, -8} {2, 8}",
                            rdr.GetName(0), rdr.GetName(1), rdr.GetName(2)));

                        while (rdr.Read())
                        {
                            Console.WriteLine(String.Format("{0, -3} {1, -8} {2, 8}",
                                rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2)));
                        }
                    }
                }

                con.Close();
            }
        }


        /// <summary>
        /// Getting SQLite metadata with C#
        /// </summary>
        static void TestL()
        {
            string cs = "URI=file:test.db";

            string nrows = null;

            try
            {
                Console.Write("Enter rows to fetch: ");
                nrows = Console.ReadLine();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.ToString());
            }

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Cars LIMIT @Id";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Id", int.Parse(nrows));

                    int cols = 0;
                    int rows = 0;

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {

                        cols = rdr.FieldCount;
                        rows = 0;

                        while (rdr.Read())
                        {
                            rows++;
                        }

                        Console.WriteLine("The query fetched {0} rows", rows);
                        Console.WriteLine("Each row has {0} cols", cols);
                    }
                }

                con.Close();
            }
        }


        /// <summary>
        /// 读取 binary数据
        /// </summary>
        static void TestK()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = "SELECT Data FROM Images WHERE Id=1";
                byte[] data = (byte[])cmd.ExecuteScalar();

                try
                {
                    if (data != null)
                    {
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.Filter = "|*.jpg;*.png";
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(saveFile.FileName, data);
                        }
                        else
                        {
                            Console.WriteLine("CancelSave File ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Binary data not read");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                con.Close();
            }
        }

        /// <summary>
        /// insert binary data
        /// </summary>
        static void TestJ()
        {

            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {

                con.Open();

                byte[] data = null;

                try
                {
                    OpenFileDialog openImage = new OpenFileDialog();
                    openImage.Filter = "|*.jpg;*.png";
                    openImage.ShowDialog();
                    data = File.ReadAllBytes(openImage.FileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                SQLiteCommand cmd = new SQLiteCommand(con);

                cmd.CommandText = "INSERT INTO Images(Data) VALUES (@img)";
                cmd.Prepare();

                cmd.Parameters.Add("@img", DbType.Binary, data.Length);
                cmd.Parameters["@img"].Value = data;
                cmd.ExecuteNonQuery();

                con.Close();

            }
        }

        /// <summary>
        /// 从XML读取数据
        /// </summary>
        static void TestI()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                DataSet ds = new DataSet();

                ds.ReadXml("cars.xml");
                DataTable dt = ds.Tables["Cars"];

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.Write(row[col] + " ");
                    }

                    Console.WriteLine();
                }

                con.Close();
            }
        }


        static void TestA()
        {
            string dbName = "demo.db";
            string connStr = string.Format("Data Source={0}", dbName);
            if (File.Exists(dbName) == false)
                File.Create(dbName);

            SQLiteConnection con = null;
            SQLiteCommand cmd = null;

            try
            {
                con = new SQLiteConnection(connStr);
                con.Open();

                string stm = "SELECT SQLITE_VERSION()";
                cmd = new SQLiteCommand(stm, con);

                string version = Convert.ToString(cmd.ExecuteScalar());

                Console.WriteLine("SQLite version : {0}", version);

            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (con != null)
                {
                    try
                    {
                        con.Close();

                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine("Closing connection failed.");
                        Console.WriteLine("Error: {0}", ex.ToString());

                    }
                    finally
                    {
                        con.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Drop 表格和 
        /// </summary>
        static void TestB()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Cars";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Cars(Id INTEGER PRIMARY KEY, 
                    Name TEXT, Price INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(1,'Audi',52642)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(2,'Mercedes',57127)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(3,'Skoda',9000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(4,'Volvo',29000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(5,'Bentley',350000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(6,'Citroen',21000)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(7,'Hummer',41400)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Cars VALUES(8,'Volkswagen',21600)";
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        /// <summary>
        /// 插入资料
        /// </summary>
        static void TestC()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO Cars(Name, Price) VALUES(@Name, @Price)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Name", "BMW");
                    cmd.Parameters.AddWithValue("@Price", 36600);
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        /// <summary>
        /// 查询资料 Test
        /// </summary>
        static void TestD()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                //string stm = "SELECT * FROM Cars LIMIT 5";
                string stm = "SELECT * FROM Cars where Price > 40000";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(rdr.GetInt32(0) + " "
                                + rdr.GetString(1) + " " + rdr.GetInt32(2));
                        }
                    }
                }

                con.Close();
            }


        }

        /// <summary>
        /// 结果取值方式
        /// </summary>
        static void TestE()
        {
            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Cars LIMIT 5";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.Write("{0}\t", rdr["Id"]);
                            Console.Write("{0}\t\t ", rdr["Name"]);
                            Console.Write("{0} \n", rdr["Price"]);
                        }
                    }
                }

                con.Close();
            }
        }

        /// <summary>
        /// 多次选择, 多次结果
        /// </summary>
        static void TestF()
        {
            string cs = "Data Source=:memory:";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT 25; SELECT 44; SELECT 33";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        do
                        {
                            rdr.Read();
                            Console.WriteLine("{0}", rdr.GetInt32(0));

                        } while (rdr.NextResult());

                    }
                }

                con.Close();
            }
        }

        /// <summary>
        /// 使用DataTable添加数据
        /// </summary>
        static void TestG()
        {

            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Friends2";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "CREATE TABLE Friends2(Id INTEGER PRIMARY KEY, Name TEXT);";
                    cmd.ExecuteNonQuery();
                }


                DataTable table = new DataTable("Friends2");

                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = Type.GetType("System.Int32");
                column.ColumnName = "Id";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Name";
                table.Columns.Add(column);

                row = table.NewRow();
                row["Id"] = 1;
                row["Name"] = "Jane";
                table.Rows.Add(row);

                row = table.NewRow();
                row["Id"] = 2;
                row["Name"] = "Lucy";
                table.Rows.Add(row);

                row = table.NewRow();
                row["Id"] = 3;
                row["Name"] = "Thomas";
                table.Rows.Add(row);

                string SQL = "SELECT * FROM Friends2";

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(SQL, con))
                {
                    using (new SQLiteCommandBuilder(da))
                    {
                        da.Fill(table);
                        da.Update(table);
                    }
                }

                con.Close();
            }
        }

        /// <summary>
        /// xml写入查询结果
        /// </summary>
        static void TestH()
        {

            string cs = "URI=file:test.db";

            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Cars where Price < 40000";

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(stm, con))
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds, "Cars");
                    DataTable dt = ds.Tables["Cars"];

                    dt.WriteXml("cars.xml");

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            Console.Write(row[col] + " ");
                        }

                        Console.WriteLine();
                    }
                }

                con.Close();
            }

        }

        #endregion

    }
}
