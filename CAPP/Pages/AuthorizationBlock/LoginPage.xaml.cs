using CAPP.Pages.MainBlock;

namespace CAPP;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
        if (File.Exists(Constants.UserDataPath))
        {
            Shell.Current.GoToAsync("///Activity");
        }
		InitializeComponent();
    }

    private async Task createProductDatabase()
    {
        if (!Path.Exists(Constants.ProductDatabasePath))
        {
            var stream = await FileSystem.OpenAppPackageFileAsync(Constants.ProductDatabaseFilename);

            using (var fileStream = File.Create(Constants.ProductDatabasePath))
            {
                stream.CopyTo(fileStream);
            }
        }
    }

	private async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
        await createProductDatabase();
        await Navigation.PushAsync(new HeightPage());
    }
}

