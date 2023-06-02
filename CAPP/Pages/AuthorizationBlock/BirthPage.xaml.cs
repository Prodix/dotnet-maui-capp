using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;

namespace CAPP;

public partial class BirthPage : ContentPage
{
    int mode;
    int heightValue;
    double weightValue;
    double intakeCoefficient;
    string gender;

	public BirthPage(int mode, int heightValue, double weightValue, string gender, double intakeCoefficient)
	{
		InitializeComponent();
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.intakeCoefficient = intakeCoefficient;
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
                IntakeCoefficient = intakeCoefficient,
                HeightValue = heightValue,
                Gender = gender,
                Mode = mode,
                BirthDate = dateTime.ToShortDateString(),
                Bmi = weightValue / (heightValue / 100.0 * (heightValue / 100.0))
            };

            double calorieIntake = (gender == "f") ? 447.6 + (9.3 * weightValue) + (3.1 * heightValue) - (4.3 * ((DateTime.Now - dateTime).Days / 365)) * intakeCoefficient : (88.4 + (13.4 * weightValue) + (4.8 * heightValue)) - (5.7 * ((DateTime.Now - dateTime).Days / 365) * intakeCoefficient);

            switch (userData.Mode)
            {
                case 1:
                    calorieIntake -= weightValue * 0.01 * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.5 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.3 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.2 / 4);
                    break;
                case 2:
                    calorieIntake += weightValue * 0.01 * 1540 / 3;
                    userData.CalorieIntake = (int)Math.Round(calorieIntake, 2);
                    userData.Carb = (int)(userData.CalorieIntake * 0.6 / 9);
                    userData.Fat = (int)(userData.CalorieIntake * 0.15 / 4);
                    userData.Protein = (int)(userData.CalorieIntake * 0.25 / 4);
                    break;
                case 3:
                    calorieIntake += weightValue * 0.01 * 1540 / 3;
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

            string json = JsonConvert.SerializeObject(userData);

            File.WriteAllText(Constants.UserDataPath, json);

            await Shell.Current.GoToAsync("///Activity");
        }
    }
}

