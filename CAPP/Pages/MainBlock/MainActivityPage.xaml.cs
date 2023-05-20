using Newtonsoft.Json;

namespace CAPP.Pages.MainBlock;

public partial class MainActivityPage : ContentPage
{
    UserData userData;

    public MainActivityPage()
	{
        userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));

		BindingContext = this;
		InitializeComponent();
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
    }

    private void GoToCalculatorPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Calculator");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Stats");
    }

}