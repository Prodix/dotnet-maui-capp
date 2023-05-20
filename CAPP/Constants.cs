using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public static class Constants
    {
        public const string UserDataFilename = "userdata.json";
        public const string ProductDatabaseFilename = "Products.db";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string UserDataPath =>
            Path.Combine(FileSystem.AppDataDirectory, UserDataFilename);

        public static string ProductDatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, ProductDatabaseFilename);
    }
}
