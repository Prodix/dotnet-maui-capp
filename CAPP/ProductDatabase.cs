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

        public async Task<int> InsertProductAsync(ProductData item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<List<ProductData>> ListProductsAsync()
        {
            return await Database.Table<ProductData>().ToListAsync();
        }

        public async Task<ProductData> GetFirstItemAsync()
        {
            return await Database.Table<ProductData>().FirstAsync();
        }

        public async Task<List<ProductData>> FindProducts(string name)
        {
            return await Database.QueryAsync<ProductData>($"SELECT * FROM (SELECT *, length(Name) FROM ProductData WHERE Name LIKE '%{name}%' UNION SELECT *, length(Name) FROM ProductData WHERE Name LIKE '{name[0].ToString().ToUpper() + name.Substring(1)}%') ORDER BY length(Name) ASC;");
        }

        public async Task<int> InsertRecipeAsync(RecipeData item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<List<RecipeData>> ListRecipeAsync()
        {
            return await Database.Table<RecipeData>().ToListAsync();
        }

        public async Task<int> GetRecipeCountAsync()
        {
            return await Database.Table<RecipeData>().CountAsync();
        }

        public async Task<int> InsertRecipeItemAsync(RecipeItem item)
        {
            return await Database.InsertAsync(item);
        }
    }
}
