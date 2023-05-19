using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class ProductDatabase
    {
        SQLiteAsyncConnection Database;

        public ProductDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.ProductDatabasePath, Constants.Flags);
        }

        async Task Init()
        {
            await Database.CreateTableAsync<ProductData>();
        }

        public async Task<int> SaveItemAsync(ProductData item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<List<ProductData>> ListItemAsync()
        {
            await Init();
            return await Database.Table<ProductData>().ToListAsync();
        }

        public async Task<ProductData> GetFirstItemAsync()
        {
            await Init();
            return await Database.Table<ProductData>().FirstAsync();
        }

        public async Task<List<ProductData>> FindItem(string name)
        {
            await Init();
            //return await Database.Table<ProductData>().Where(x => x.Name.StartsWith(name)).ToListAsync();
            return await Database.QueryAsync<ProductData>($"SELECT * FROM (SELECT *, length(Name) FROM ProductData WHERE Name LIKE '%{name}%' UNION SELECT *, length(Name) FROM ProductData WHERE Name LIKE '{name[0].ToString().ToUpper() + name.Substring(1)}%') ORDER BY length(Name) ASC;");

        }
    }
}
