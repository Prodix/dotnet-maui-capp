namespace CAPP;

public partial class HeightPage : ContentPage
{

    public HeightPage()
    {
		InitializeComponent();
	}
    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GoalPage(), false);
    }

    
}

