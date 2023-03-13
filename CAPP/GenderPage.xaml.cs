namespace CAPP;

public partial class GenderPage : ContentPage
{

	public GenderPage()
	{
		InitializeComponent();
	}
    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BirthPage(), false);
    }

}

