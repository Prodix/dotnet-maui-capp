using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class SqliteDatabase
    {

        SQLiteAsyncConnection Database;

        public SqliteDatabase() 
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        async Task Init()
        {
            await Database.CreateTableAsync<UserData>();
        }

        public async Task<int> SaveItemAsync(UserData item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<List<UserData>> ListItemAsync()
        {
           return await Database.Table<UserData>().ToListAsync();
        }
    }
}
