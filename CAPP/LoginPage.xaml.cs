namespace CAPP;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
    }
	private async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new HeightPage(), false);
    }

}

