using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnCourseApp.LocalDatabase
{
    public class SqlConnectionBase
    {
        public ISQLiteAsyncConnection CreateConnection()
        {
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "mocsqlite.db3"),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}
