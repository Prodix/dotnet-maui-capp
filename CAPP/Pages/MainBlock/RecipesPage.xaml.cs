using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace CAPP.Pages.MainBlock;

public partial class RecipesPage : ContentPage
{
	ProductDatabase productDatabase = new ProductDatabase();

	List<RecipeData> breakfast = new List<RecipeData>();
	List<RecipeData> lunch = new List<RecipeData>();
	List<RecipeData> dinner = new List<RecipeData>();

    //UserData userData = new UserData();

	public RecipesPage()
	{
        //userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));

        BindingContext = this;
		InitializeComponent();

        //if (userData.Mode == 2 || userData.Mode == 3)
        //{
        //    imgText.Text = "Набор массы";
        //    img.Source = "gain_plan";
        //}

        Task.WaitAll(Task.Run(async () => breakfast = await productDatabase.GetBreakfastRecipesAsync()),
                     Task.Run(async () => lunch = await productDatabase.GetLunchRecipesAsync()),
                     Task.Run(async () => dinner = await productDatabase.GetDinnerRecipesAsync()));

        BreakfastView.ItemsSource = breakfast;
        LunchView.ItemsSource = lunch;
        DinnerView.ItemsSource = dinner;

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("///Activity");
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        RecipeData data = (RecipeData)((Border)sender).BindingContext;
        List<RecipeItem> items = new List<RecipeItem>();
        List<RecipeStep> steps = new List<RecipeStep>();

        Task.WaitAll(Task.Run(async () => {
            items = await productDatabase.GetIngredientsAsync(data);
            items.ForEach(async item => item.Name = await productDatabase.GetProductNameAsync(item.Product_id));
        }), Task.Run(async () => steps = await productDatabase.GetStepsAsync(data)));

        await Navigation.PushAsync(new RecipeInfo(data, items, steps));
    }

    //private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    //{
    //    await Navigation.PushAsync(new MealPlanPage());
    //}
}