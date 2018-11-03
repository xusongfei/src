using System;
using Dos.ORM;
using Dos.ORM.Sqlite;

namespace Lead.Detect.DatabaseHelper
{
    public class SqlLiteHelper
    {
        public static DbSession DB { get; protected set; }


        public static void InitDatabase(string file = @".\Config\default.db")
        {
            DB = new DbSession(new Database(new SqliteProvider($@"Data Source={file};Version=3;")));
        }
    }


    public static class DatabaseExtension
    {
        public static void Save(this ProductDataEntity entity)
        {
            SqlLiteHelper.DB?.Insert(entity);
        }
    }
}