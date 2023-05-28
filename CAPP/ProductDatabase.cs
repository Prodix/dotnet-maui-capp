using CommunityToolkit.Maui.Core.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async Task<string> GetProductNameAsync(int id)
        {
            return (await Database.Table<ProductData>().Where(x => x.Id == id).FirstAsync()).Name;
        }

        public async Task<ProductData> GetFirstItemAsync()
        {
            return await Database.Table<ProductData>().FirstAsync();
        }

        public async Task<List<ProductData>> FindProducts(string name)
        {
            return await Database.QueryAsync<ProductData>($"SELECT * FROM (SELECT *, length(Name) FROM ProductData WHERE Name LIKE '%{name}%' UNION SELECT *, length(Name) FROM ProductData WHERE Name LIKE '{name[0].ToString().ToUpper() + name.Substring(1)}%') ORDER BY length(Name) ASC;");
        }

        public async Task<List<RecipeData>> FindRecipes(string name)
        {
            return await Database.QueryAsync<RecipeData>($"SELECT * FROM (SELECT *, length(Name) FROM RecipeData WHERE Name LIKE '%{name}%' UNION SELECT *, length(Name) FROM RecipeData WHERE Name LIKE '{name[0].ToString().ToUpper() + name.Substring(1)}%') ORDER BY length(Name) ASC;");
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

        public async Task<int> CreateMealAsync(MealData item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<int> InsertMealItemAsync(MealItem item)
        {
            return await Database.InsertAsync(item);
        }

        public async Task<List<MealData>> GetMealsByDateAsync(string date)
        {
            return await Database.Table<MealData>().Where(x => x.Date == date).ToListAsync();
        }

        public async Task<ObservableCollection<MealItem>> GetMealItemsAsync(int mealId)
        {
            return (await Database.Table<MealItem>().Where(x => x.Meal_id == mealId).ToListAsync()).ToObservableCollection();
        }

        public async Task<string> GetMealItemNameAsync(int? recipeId = null, int? productId = null)
        {
            if (recipeId is null)
            {
                return (await Database.Table<ProductData>().Where(x => x.Id == productId).FirstAsync()).Name;
            }
            else
            {
                return (await Database.Table<RecipeData>().Where(x => x.Id == recipeId).FirstAsync()).Name;
            }
        }

        public async Task<double> GetMealItemCalorieAsync(int weight, int? recipeId = null, int? productId = null)
        {
            if (recipeId is null)
            {
                return (await Database.Table<ProductData>().Where(x => x.Id == productId).FirstAsync()).Kcal * (weight / 100.0);
            }
            else
            {
                return (await Database.Table<RecipeData>().Where(x => x.Id == recipeId).FirstAsync()).Kcal * (weight / 100.0);
            }
        }

        public async Task<double> GetMealItemCarbAsync(int weight, int? recipeId = null, int? productId = null)
        {
            if (recipeId is null)
            {
                return (await Database.Table<ProductData>().Where(x => x.Id == productId).FirstAsync()).Carb * (weight / 100.0);
            }
            else
            {
                return (await Database.Table<RecipeData>().Where(x => x.Id == recipeId).FirstAsync()).Carb * (weight / 100.0);
            }
        }

        public async Task<double> GetMealItemProteinAsync(int weight, int? recipeId = null, int? productId = null)
        {
            if (recipeId is null)
            {
                return (await Database.Table<ProductData>().Where(x => x.Id == productId).FirstAsync()).Protein * (weight / 100.0);
            }
            else
            {
                return (await Database.Table<RecipeData>().Where(x => x.Id == recipeId).FirstAsync()).Protein * (weight / 100.0);
            }
        }

        public async Task<double> GetMealItemFatAsync(int weight, int? recipeId = null, int? productId = null)
        {
            if (recipeId is null)
            {
                return (await Database.Table<ProductData>().Where(x => x.Id == productId).FirstAsync()).Fat * (weight / 100.0);
            }
            else
            {
                return (await Database.Table<RecipeData>().Where(x => x.Id == recipeId).FirstAsync()).Fat * (weight / 100.0);
            }
        }

        public async Task<int> UpdateMealCarbs(MealData item)
        {
            return await Database.UpdateAsync(item);
        }

        public async Task<int> GetMealsCountAsync()
        {
            return await Database.Table<MealData>().CountAsync();
        }

        public async Task<List<RecipeData>> GetBreakfastRecipesAsync()
        {
            return await Database.Table<RecipeData>().Where(x => x.Type == "Завтрак").ToListAsync();
        }

        public async Task<List<RecipeData>> GetLunchRecipesAsync()
        {
            return await Database.Table<RecipeData>().Where(x => x.Type == "Обед").ToListAsync();
        }

        public async Task<List<RecipeData>> GetDinnerRecipesAsync()
        {
            return await Database.Table<RecipeData>().Where(x => x.Type == "Ужин").ToListAsync();
        }

        public async Task<List<RecipeItem>> GetIngredientsAsync(RecipeData data)
        {
            return await Database.Table<RecipeItem>().Where(x => x.Recipe_id == data.Id).ToListAsync();
        }

        public async Task<List<RecipeStep>> GetStepsAsync(RecipeData data)
        {
            return await Database.Table<RecipeStep>().Where(x => x.Recipe_id == data.Id).ToListAsync();
        }
    }
}
