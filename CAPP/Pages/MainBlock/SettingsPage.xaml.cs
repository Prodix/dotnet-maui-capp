using CAPP.Controls;
using CAPP.Resources.Styles;
using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CAPP.Pages.MainBlock;

public partial class SettingsPage : ContentPage, INotifyPropertyChanged
{
	UserData userData = new UserData();

    private double _bmi = 0;
    private string _bmistate = "";
    private string _age = "";
    private double _weight = 0;
    private int _height = 0;

    public double BMI
    {
        get { return _bmi; }
        set
        {
            _bmi = Math.Round(value, 2);
            BmiState = _bmi.ToString();
            OnPropertyChanged("BMI");
        }
    }

    public double Weight
    {
        get { return _weight; }
        set
        {
            _weight = value;
            BMI = value / (_height / 100.0 * (_height / 100.0));
            OnPropertyChanged("Weight");
        }
    }

    public int Height
    {
        get { return _height; }
        set
        {
            _height = value;
            BMI = _weight / (value / 100.0 * (value / 100.0));
            OnPropertyChanged("Height");
        }
    }

    public string Age
    {
        get { return _age; }
        set
        {
            _age = Convert.ToInt32((DateTime.Now - DateTime.Parse(value)).TotalDays / 365).ToString();
            OnPropertyChanged("Age");
        }
    }

    public string BmiState
    {
        get { return _bmistate; }
        set
        {
            switch (Convert.ToDouble(value))
            {
                case > 40:
                    _bmistate = "Ожирение 3 степени";
                    BMIState.TextColor = Color.FromRgba("#DC0101");
                    break;
                case > 35:
                    _bmistate = "Ожирение 2 степени";
                    BMIState.TextColor = Color.FromRgba("#DC4301");
                    break;
                case > 30:
                    _bmistate = "Ожирение 1 степени";
                    BMIState.TextColor = Color.FromRgba("#DC8401");
                    break;
                case > 25:
                    _bmistate = "Предожирение";
                    BMIState.TextColor = Color.FromRgba("#DCB901");
                    break;
                case > 18.5:
                    _bmistate = "Норма";
                    BMIState.TextColor = Color.FromRgba("#20DC01");
                    break;
                case > 16:
                    _bmistate = "Недостаток";
                    BMIState.TextColor = Color.FromRgba("#86FF73");
                    break;
                default:
                    _bmistate = "Дефицит";
                    BMIState.TextColor = Color.FromRgba("#B3FF84");
                    break;
            }
            OnPropertyChanged("BmiState");
        }
    }

    public SettingsPage()
	{
		InitializeComponent();


		userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));
        BMI = userData.Bmi;
        Age = userData.BirthDate;
        Height = userData.HeightValue;
        Weight = userData.WeightValue;
        
        bmi.SetBinding(Label.TextProperty, "BMI", stringFormat: "ИМТ (кг/м²): {0}");
        bmi.BindingContext = this;

        AgeLabel.SetBinding(Label.TextProperty, "Age");
        AgeLabel.BindingContext = this;

        HeightLabel.SetBinding(Label.TextProperty, "Height", stringFormat: "{0} см");
        HeightLabel.BindingContext = this;

        WeightLabel.SetBinding(Label.TextProperty, "Weight", stringFormat: "{0} кг");
        WeightLabel.BindingContext = this;

        BMIState.SetBinding(Label.TextProperty, "BmiState");
        BMIState.BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        SettingsPopup settingsPopup = new SettingsPopup(Weight, Height);
        UserData result = (UserData)await this.ShowPopupAsync(settingsPopup);
        if (result != null)
        {
            Height = result.HeightValue;
            Weight = result.WeightValue;

            userData.WeightValue = result.WeightValue;
            userData.HeightValue = result.HeightValue;

            
            double calorieIntake = (userData.Gender == "f") ? 447.6 + (9.3 * userData.WeightValue) + (3.1 * userData.HeightValue) - (4.3 * Convert.ToInt32(Age)) * userData.IntakeCoefficient : 88.4 + (13.4 * userData.WeightValue) + (4.8 * userData.HeightValue) - (5.7 * Convert.ToInt32(Age)) * userData.IntakeCoefficient;

            switch (userData.Mode)
            {
                case 1:
                    calorieIntake -= userData.WeightValue * 0.02 * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.5 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.3 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.2 / 4);
                    break;
                case 2:
                    calorieIntake += userData.WeightValue * 0.02 * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.6 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.15 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.25 / 4);
                    break;
                case 3:
                    calorieIntake += userData.WeightValue * 0.02 * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.5 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.2 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.3 / 4);
                    break;
                case 4:
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.4 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.3 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.3 / 4);
                    break;
                default:
                    break;
            }

            File.WriteAllText(Constants.UserDataPath, JsonConvert.SerializeObject(userData));
        }
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///Recipes");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///Activity");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Выберите тему", null, null, "Зелёная", "Синяя", "Розовая");

        switch (action)
        {
            case "Зелёная":
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new GreenTheme());
                Application.Current.Resources.MergedDictionaries.Add(new Styles());
                userData.Theme = "Зелёная";
                File.WriteAllText(Constants.UserDataPath, JsonConvert.SerializeObject(userData));
                break;
            case "Розовая":
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new PinkTheme());
                Application.Current.Resources.MergedDictionaries.Add(new Styles());
                userData.Theme = "Розовая";
                File.WriteAllText(Constants.UserDataPath, JsonConvert.SerializeObject(userData));
                break;
            case "Синяя":
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new BlueTheme());
                Application.Current.Resources.MergedDictionaries.Add(new Styles());
                userData.Theme = "Синяя";
                File.WriteAllText(Constants.UserDataPath, JsonConvert.SerializeObject(userData));
                break;
            default:
                break;
        }

        ProfileButton.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior()
        {
            TintColor = (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]
        });
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ProfileButton.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior()
        {
            TintColor = (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]
        });
    }
}