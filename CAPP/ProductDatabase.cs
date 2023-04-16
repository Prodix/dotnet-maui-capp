using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    internal class ProductDatabase
    {
        SQLiteAsyncConnection Database;

        public ProductDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.ProductDatabasePath, Constants.Flags);
        }

        public async Task<int> SaveItemAsync(ProductData item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<List<ProductData>> ListItemAsync()
        {
            return await Database.Table<ProductData>().ToListAsync();
        }
    }
}
