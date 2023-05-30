using CAPP.Controls;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CAPP.Pages.MainBlock;

public partial class MainActivityPage : ContentPage
{
    UserData userData;
    ProductDatabase productDatabase;
    ObservableCollection<CalendarDay> days = new ObservableCollection<CalendarDay>();
    ObservableCollection<MealData> meals = new ObservableCollection<MealData>
    {
        new MealData { Type = "Завтрак" },
        new MealData { Type = "Обед" },
        new MealData { Type = "Ужин" }
    };
    bool isProgrammaticlyChanged = false;

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

        Meals.ItemsSource = meals;

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

    }

    private void GoToCalculatorPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Calculator");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        //Shell.Current.GoToAsync("///Stats");
    }


    private async void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (isProgrammaticlyChanged)
        {
            isProgrammaticlyChanged = false;
            return;
        }

        //if (((CalendarDay)e.CurrentSelection[0]).Date.Date > DateTime.Now.Date && e.PreviousSelection.Count != 0)
        //{
        //    Calendar.SelectedItem = (CalendarDay)e.PreviousSelection[0];
        //    isProgrammaticlyChanged = true;
        //    return;
        //}

        ((CalendarDay)e.CurrentSelection[0]).Color = new SolidColorBrush((Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]);
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
        await Task.WhenAll(test(meals[0]), test(meals[1]), test(meals[2]));

        double kcalRemain = userData.CalorieIntake - (meals[0].Kcal + meals[1].Kcal + meals[2].Kcal);
        double fatRemain = userData.Fat - (meals.Sum(x => x.Fat));
        double proteinRemain = userData.Protein - (meals.Sum(x => x.Protein));
        double carbRemain = userData.Carb - (meals.Sum(x => x.Carb));

        if ( kcalRemain < 0 )
        {
            KcalRemain.Text = "0";
        }
        else
        {
            KcalRemain.Text = ((int)kcalRemain).ToString();
        }

        if (carbRemain < 0)
        {
            CarbRemain.Text = "0г";
        }
        else
        {
            CarbRemain.Text = ((int)carbRemain).ToString() + "г";
        }

        if (fatRemain < 0)
        {
            FatRemain.Text = "0г";
        }
        else
        {
            FatRemain.Text = ((int)fatRemain).ToString() + "г";
        }

        if (proteinRemain < 0)
        {
            ProteinRemain.Text = "0г";
        }
        else
        {
            ProteinRemain.Text = ((int)proteinRemain).ToString() + "г";
        }

        MainBar.Progress = 1 - ((Convert.ToInt32(KcalRemain.Text) / (userData.CalorieIntake * 0.01)) / 100);
        FatBar.Progress = 1 - ((Convert.ToDouble(FatRemain.Text.Replace("г", "")) / (userData.Fat * 0.01)) / 100);
        ProteinBar.Progress = 1 - ((Convert.ToDouble(ProteinRemain.Text.Replace("г", "")) / (userData.Protein * 0.01)) / 100);
        CarbBar.Progress = 1 - ((Convert.ToDouble(CarbRemain.Text.Replace("г", "")) / (userData.Carb * 0.01)) / 100);
        //foreach (var item in meals)
        //{
        //    item.Items = await productDatabase.GetMealItemsAsync(item.Id);
        //    foreach (var i in item.Items)
        //    {
        //        i.Name = await productDatabase.GetMealItemNameAsync(i.Recipe_id, i.Product_id);
        //        i.Calorie = await productDatabase.GetMealItemCalorieAsync(i.Weight, i.Recipe_id, i.Product_id);
        //        item.Kcal += i.Calorie;
        //    }
        //}
    }

    //rename function
    private async Task test(MealData item)
    {
        double kcal = 0;
        item.Items = await productDatabase.GetMealItemsAsync(item.Id);

        foreach (var i in item.Items)
        {
            i.Name = await productDatabase.GetMealItemNameAsync(i.Recipe_id, i.Product_id);
            i.Calorie = await productDatabase.GetMealItemCalorieAsync(i.Weight, i.Recipe_id, i.Product_id);
            kcal += i.Calorie;
        }

        item.Kcal = Math.Round(kcal);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (((CalendarDay)Calendar.SelectedItem).Date.Date < DateTime.Now.Date || ((CalendarDay)Calendar.SelectedItem).Date.Date > DateTime.Now.Date)
        {
            await Toast.Make("Вы выбрали не текущий день!").Show();
            return;
        }

        int tapId = ((MealData)((CollectionView)((VerticalStackLayout)((Border)sender).Parent.Parent).Children[3]).BindingContext).Id;
        // доступ к колекции еды ((ObservableCollection<MealItem>)((CollectionView)((VerticalStackLayout)((Border)sender).Parent.Parent).Children[3]).ItemsSource)[0].Calorie;

        var popup = new ProductPopup();
        var weightPopup = new WeightPopup();
        ProductData product = (ProductData)await this.ShowPopupAsync(popup);
        
        if (product is null) return;
        
        object weight = await this.ShowPopupAsync(weightPopup);

        if (weight is null) return;

        foreach (var meal in meals)
        {
            if (meal.Id == tapId)
            {
                MealItem item = new MealItem
                {
                    Meal_id = meal.Id,
                    Weight = Convert.ToInt32(weight), // нужно сделать ввод веса
                    Id = (await productDatabase.GetMealItemsAsync(meal.Id)).Count + 1
                };


                if (product.Type == "Recipe")
                {
                    item.Recipe_id = product.Id;
                    item.Name = product.Name;
                    item.Calorie = Math.Round(await productDatabase.GetMealItemCalorieAsync(item.Weight, product.Id, null));
                    meal.Carb += await productDatabase.GetMealItemCarbAsync(item.Weight, product.Id);
                    meal.Protein += await productDatabase.GetMealItemProteinAsync(item.Weight, product.Id);
                    meal.Fat += await productDatabase.GetMealItemFatAsync(item.Weight, product.Id);
                }
                else
                {
                    item.Product_id = product.Id;
                    item.Name = product.Name;
                    item.Calorie = Math.Round(await productDatabase.GetMealItemCalorieAsync(item.Weight, null, product.Id));
                    meal.Carb += await productDatabase.GetMealItemCarbAsync(item.Weight, null, product.Id);
                    meal.Protein += await productDatabase.GetMealItemProteinAsync(item.Weight, null, product.Id);
                    meal.Fat += await productDatabase.GetMealItemFatAsync(item.Weight, null, product.Id);
                }

                meal.Items.Add(item);
                
                meal.Kcal += Math.Round(item.Calorie);

                await productDatabase.InsertMealItemAsync(item);
                await productDatabase.UpdateMealCarbs(meal);

            }
        }

        double kcalRemain = userData.CalorieIntake - (meals[0].Kcal + meals[1].Kcal + meals[2].Kcal);
        double fatRemain = userData.Fat - (meals.Sum(x => x.Fat));
        double proteinRemain = userData.Protein - (meals.Sum(x => x.Protein));
        double carbRemain = userData.Carb - (meals.Sum(x => x.Carb));

        if (kcalRemain < 0)
        {
            if (kcalRemain <= -100)
            {
                await Toast.Make("Вы превышаете дневной лимит калорий!", ToastDuration.Long).Show();
            }

            KcalRemain.Text = "0";
        }
        else
        {
            KcalRemain.Text = ((int)kcalRemain).ToString();
        }

        if (carbRemain < 0)
        {
            if (carbRemain <= -2)
            {
                await Toast.Make("Вы превышаете дневной лимит углеводов!", ToastDuration.Short).Show();
            }

            CarbRemain.Text = "0г";
        }
        else
        {
            CarbRemain.Text = ((int)carbRemain).ToString() + "г";
        }

        if (fatRemain < 0)
        {
            if (fatRemain <= -2)
            {
                await Toast.Make("Вы превышаете дневной лимит жиров!", ToastDuration.Short).Show();
            }

            FatRemain.Text = "0г";
        }
        else
        {
            FatRemain.Text = ((int)fatRemain).ToString() + "г";
        }

        if (proteinRemain < 0)
        {
            if (proteinRemain <= -2)
            {
                await Toast.Make("Вы превышаете дневной лимит белков!", ToastDuration.Short).Show();
            }

            ProteinRemain.Text = "0г";
        }
        else
        {
            ProteinRemain.Text = ((int)proteinRemain).ToString() + "г";
        }

        MainBar.Progress = 1 - ((Convert.ToInt32(KcalRemain.Text) / (userData.CalorieIntake * 0.01)) / 100);
        FatBar.Progress = 1 - ((Convert.ToDouble(FatRemain.Text.Replace("г", "")) / (userData.Fat * 0.01)) / 100);
        ProteinBar.Progress = 1 - ((Convert.ToDouble(ProteinRemain.Text.Replace("г", "")) / (userData.Protein * 0.01)) / 100);
        CarbBar.Progress = 1 - ((Convert.ToDouble(CarbRemain.Text.Replace("г", "")) / (userData.Carb * 0.01)) / 100);

    }

    private void Calendar_Loaded(object sender, EventArgs e)
    {
        Calendar.ScrollTo(Calendar.SelectedItem, animate: false);
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///Recipes", new Dictionary<string, object> { { "UserData", userData } });
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///Settings");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ((CalendarDay)Calendar.SelectedItem).Color = new SolidColorBrush((Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]);


        userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));

        double kcalRemain = userData.CalorieIntake - (meals[0].Kcal + meals[1].Kcal + meals[2].Kcal);
        double fatRemain = userData.Fat - (meals.Sum(x => x.Fat));
        double proteinRemain = userData.Protein - (meals.Sum(x => x.Protein));
        double carbRemain = userData.Carb - (meals.Sum(x => x.Carb));

        if (kcalRemain < 0)
        {
            KcalRemain.Text = "0";
        }
        else
        {
            KcalRemain.Text = ((int)kcalRemain).ToString();
        }

        if (carbRemain < 0)
        {
            CarbRemain.Text = "0г";
        }
        else
        {
            CarbRemain.Text = ((int)carbRemain).ToString() + "г";
        }

        if (fatRemain < 0)
        {
            FatRemain.Text = "0г";
        }
        else
        {
            FatRemain.Text = ((int)fatRemain).ToString() + "г";
        }

        if (proteinRemain < 0)
        {
            ProteinRemain.Text = "0г";
        }
        else
        {
            ProteinRemain.Text = ((int)proteinRemain).ToString() + "г";
        }

        MainBar.Progress = 1 - ((Convert.ToInt32(KcalRemain.Text) / (userData.CalorieIntake * 0.01)) / 100);
        FatBar.Progress = 1 - ((Convert.ToDouble(FatRemain.Text.Replace("г", "")) / (userData.Fat * 0.01)) / 100);
        ProteinBar.Progress = 1 - ((Convert.ToDouble(ProteinRemain.Text.Replace("г", "")) / (userData.Protein * 0.01)) / 100);
        CarbBar.Progress = 1 - ((Convert.ToDouble(CarbRemain.Text.Replace("г", "")) / (userData.Carb * 0.01)) / 100);

        HomeButton.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior()
        {
            TintColor = (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]
        });


    }
}