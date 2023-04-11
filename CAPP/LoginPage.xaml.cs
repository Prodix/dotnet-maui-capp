namespace CAPP;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
    }

	private async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void OnLoginTap(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}

