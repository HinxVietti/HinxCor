using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo_sqlite_encrypt
{
    class Program
    {
        static string dbname = "testdb";
        static string passwd = "passwd";

        static void Main(string[] args)
        {
            var sdao = new SQLiteDao(dbname, passwd);

            //var table = sdao.CreateTable<int>("user",
            //       new hHeader(fieldname: "uid", fieldType: FieldType.INTEGER, autoincrement: true, primarykey: true, unique: true, notnull: true),
            //       new hHeader(fieldname: "userName", fieldType: FieldType.STRING),
            //       new hHeader(fieldname: "header", fieldType: FieldType.BLOB)
            //       );

            //Console.WriteLine(table.DDL);

            Console.WriteLine();
            Console.WriteLine("finished");
            Console.ReadKey();
        }
    }
}
