namespace CAPP;

public partial class GenderPage : ContentPage
{
    int mode;
    int heightValue;
    double weightValue;
    string username;
    string email;
    string password;
    double wishWeightValue;
	public GenderPage(int mode, int heightValue, double weightValue, string username, string email, string password, double wishWeightValue = 0.0)
	{
		InitializeComponent();
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.username = username;
        this.email = email;
        this.password = password;
        this.wishWeightValue = wishWeightValue;
	}

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        if (CheckOne.IsChecked)
            await Navigation.PushAsync(new BirthPage(mode, heightValue, weightValue, username, email, password, "m", wishWeightValue));
        else 
            await Navigation.PushAsync(new BirthPage(mode, heightValue, weightValue, username, email, password, "f", wishWeightValue));
    }

    private void ButtonOn()
    {
        GoNextButton.IsEnabled = true;
        GoNextButton.Background = SolidColorBrush.White;
        GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
    }

    private void ButtonOff()
    {
        GoNextButton.IsEnabled = false;
        GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474")).Color;
        GoNextButton.TextColor = SolidColorBrush.White.Color;
    }

    private void OnTapOne(object sender, TappedEventArgs e)
    {
        if (CheckOne.IsChecked)
        {
            CheckOne.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckOne.IsChecked = true;
            CheckTwo.IsChecked = false;
            ButtonOn();
        }
    }

    private void OnTapTwo(object sender, TappedEventArgs e)
    {
        if (CheckTwo.IsChecked)
        {
            CheckTwo.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckTwo.IsChecked = true;
            CheckOne.IsChecked = false;
            ButtonOn();
        }
    }
}

