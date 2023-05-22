using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CAPP.Pages.MainBlock;

public partial class MainActivityPage : ContentPage
{
    UserData userData;
    ProductDatabase productDatabase;
    ObservableCollection<CalendarDay> days = new ObservableCollection<CalendarDay>();
    ObservableCollection<MealData> meals = new ObservableCollection<MealData>();

    public MainActivityPage()
	{
        productDatabase = new ProductDatabase();
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;

        switch (DateTime.Now.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                start = DateTime.Now.AddDays(-6);
                end = DateTime.Now;
                break;
            case DayOfWeek.Monday:
                start = DateTime.Now;
                end = DateTime.Now.AddDays(6);
                break;
            case DayOfWeek.Tuesday:
                start = DateTime.Now.AddDays(-1);
                end = DateTime.Now.AddDays(5);
                break;
            case DayOfWeek.Wednesday:
                start = DateTime.Now.AddDays(-2);
                end = DateTime.Now.AddDays(4);
                break;
            case DayOfWeek.Thursday:
                start = DateTime.Now.AddDays(-3);
                end = DateTime.Now.AddDays(3);
                break;
            case DayOfWeek.Friday:
                start = DateTime.Now.AddDays(-4);
                end = DateTime.Now.AddDays(2);
                break;
            case DayOfWeek.Saturday:
                start = DateTime.Now.AddDays(-5);
                end = DateTime.Now.AddDays(1);
                break;
            default:
                break;
        }

        for (int i = 0; i < 7; i++)
        {
            string dayOfWeek = "";
            switch (start.AddDays(i).DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayOfWeek = "Вс";
                    break;
                case DayOfWeek.Monday:
                    dayOfWeek = "Пн";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Вт";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Ср";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Чт";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Пт";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Сб";
                    break;
                default:
                    break;
            }

            days.Add(new CalendarDay
            {
                Date = start.AddDays(i),
                Day = start.AddDays(i).Day,
                Month = start.AddDays(i).Month,
                DayOfWeek = dayOfWeek
            });

        }

        userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));
		BindingContext = this;
		InitializeComponent();

        Calendar.ItemsSource = days;

        switch (DateTime.Now.Hour)
        {
            case < 6:
                Greeting.Text = "Доброй ночи!";
                break;
            case < 12:
                Greeting.Text = "Доброе утро!";
                break;
            case < 18:
                Greeting.Text = "Добрый день!";
                break;
            case < 24:
                Greeting.Text = "Добрый вечер!";
                break;
            default:
                break;
        }

        KcalRemain.Text = userData.CalorieIntake.ToString();
        CarbRemain.Text = userData.Carb.ToString() + "г";
        FatRemain.Text = userData.Fat.ToString() + "г";
        ProteinRemain.Text = userData.Protein.ToString() + "г";

        Calendar.SelectedItem = days.Where(x => x.Date.Day == DateTime.Now.Day).First();

        Meals.ItemsSource = meals;
    }

    private void GoToCalculatorPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Calculator");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Stats");
    }


    private async void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ((CalendarDay)e.CurrentSelection[0]).Color = new SolidColorBrush(Color.FromArgb("F83D7F"));
        ((CalendarDay)e.CurrentSelection[0]).Opacity = 0.25;
        ((CalendarDay)e.CurrentSelection[0]).TextBrush = Colors.White;
        if (e.PreviousSelection.Count != 0)
        {
            ((CalendarDay)e.PreviousSelection[0]).Color = new SolidColorBrush(Colors.Transparent);
            ((CalendarDay)e.PreviousSelection[0]).Opacity = 0;
            ((CalendarDay)e.PreviousSelection[0]).TextBrush = Color.FromArgb("C4C4C4");
        }

        if ((await productDatabase.GetMealsByDateAsync(((CalendarDay)e.CurrentSelection[0]).Date.ToString("yyyy-MM-dd"))).Count == 0)
        {
            foreach (var item in days)
            {
                if ((await productDatabase.GetMealsByDateAsync(item.Date.ToString("yyyy-MM-dd"))).Count != 3)
                {
                    await productDatabase.CreateMealAsync(new MealData
                    {
                        Date = item.Date.ToString("yyyy-MM-dd"),
                        Type = "Завтрак",
                        Id = await productDatabase.GetMealsCountAsync() + 1
                    });

                    await productDatabase.CreateMealAsync(new MealData
                    {
                        Date = item.Date.ToString("yyyy-MM-dd"),
                        Type = "Обед",
                        Id = await productDatabase.GetMealsCountAsync() + 1
                    });

                    await productDatabase.CreateMealAsync(new MealData
                    {
                        Date = item.Date.ToString("yyyy-MM-dd"),
                        Type = "Ужин",
                        Id = await productDatabase.GetMealsCountAsync() + 1
                    });
                }
            }
        }

        

        meals = (await productDatabase.GetMealsByDateAsync(((CalendarDay)e.CurrentSelection[0]).Date.ToString("yyyy-MM-dd"))).ToObservableCollection();
        Meals.ItemsSource = meals;
        foreach (var item in meals)
        {
            item.Items = await productDatabase.GetMealItemsAsync(item.Id);
            foreach (var i in item.Items)
            {
                i.Name = await productDatabase.GetMealItemNameAsync(i.Recipe_id, i.Product_id);
                i.Calorie = await productDatabase.GetMealItemCalorieAsync(i.Weight, i.Recipe_id, i.Product_id);
                item.Kcal += i.Calorie;
            }
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await productDatabase.InsertMealItemAsync(new MealItem
        {
            Meal_id = 22,
            Product_id = 564,
            Weight = 100,
            Recipe_id = null,
            Id = (await productDatabase.GetMealItemsAsync(22)).Count + 1
        });
    }

    private void Calendar_Loaded(object sender, EventArgs e)
    {
        Calendar.ScrollTo(Calendar.SelectedItem, animate: false);
    }
}