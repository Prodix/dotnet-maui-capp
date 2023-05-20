using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;

namespace CAPP;

public partial class BirthPage : ContentPage
{
    int mode;
    int heightValue;
    double weightValue;
    double wishWeightValue;
    string gender;

	public BirthPage(int mode, int heightValue, double weightValue, string gender, double wishWeightValue = 0.0)
	{
		InitializeComponent();
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.wishWeightValue = wishWeightValue;
        this.gender = gender;
	}

    private async void GoNextButton_Clicked(object sender, EventArgs e)
    {
        
        DateTime dateTime = DateTime.ParseExact($"{Calendar.CurrentDay}/{Calendar.CurrentMonth}/{Calendar.CurrentYear}", "dd/M/yyyy", null);
        if ((DateTime.Now - dateTime).Days / 365.0 < 18.01)
        {
            string text = "Вам должно быть больше 18 лет";
            await Toast.Make(text, ToastDuration.Short).Show();
        }
        else
        {
            UserData userData = new UserData
            {
                WeightValue = weightValue,
                WishWeightValue = wishWeightValue,
                HeightValue = heightValue,
                Gender = gender,
                Mode = mode,
                BirthDate = dateTime.ToString("yyyy.MM.dd"),
                Bmi = weightValue / (heightValue / 100 * (heightValue / 100))
            };

            switch (userData.Mode)
            {
                case 1:
                    double calorieIntake0 = (gender == "f") ? (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) - 161 : (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) + 5;
                    calorieIntake0 -= (weightValue - weightValue * 0.01) * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake0, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.5 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.3 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.2 / 4);
                    break;
                case 2:
                    double calorieIntake1 = (gender == "f") ? (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) - 161 : (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) + 5;
                    calorieIntake1 += (weightValue - weightValue * 0.01) * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake1, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.6 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.15 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.25 / 4);
                    break;
                case 3:
                    double calorieIntake2 = (gender == "f") ? (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) - 161 : (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) + 5;
                    calorieIntake2 += (weightValue - weightValue * 0.01) * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake2, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.5 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.2 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.3 / 4);
                    break;
                case 4:
                    double calorieIntake3 = (gender == "f") ? (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) - 161 : (10 * weightValue) + (6.25 * heightValue) - (5 * ((DateTime.Now - dateTime).Days / 365)) + 5;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake3, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.4 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.3 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.3 / 4);
                    break;
                default:
                    break;
            }

            string json = JsonConvert.SerializeObject(userData);

            File.WriteAllText(Constants.UserDataPath, json);

            await Shell.Current.GoToAsync("///Activity");
        }
    }
}

