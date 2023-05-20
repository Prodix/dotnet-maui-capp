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
                Greeting.Text = "������ ����!";
                break;
            case < 12:
                Greeting.Text = "������ ����!";
                break;
            case < 18:
                Greeting.Text = "������ ����!";
                break;
            case < 24:
                Greeting.Text = "������ �����!";
                break;
            default:
                break;
        }

        KcalRemain.Text = userData.CalorieIntake.ToString();
        CarbRemain.Text = userData.Carb.ToString() + "�";
        FatRemain.Text = userData.Fat.ToString() + "�";
        ProteinRemain.Text = userData.Protein.ToString() + "�";
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