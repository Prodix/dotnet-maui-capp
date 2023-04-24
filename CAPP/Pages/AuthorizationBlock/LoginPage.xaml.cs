using CAPP.Pages.MainBlock;
using SQLite;

namespace CAPP;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
        new SqliteDatabase();
		InitializeComponent();
    }

    private async Task createProductDatabase()
    {
        if (!Path.Exists(Path.Combine(FileSystem.AppDataDirectory, Constants.ProductDatabaseFilename)))
        {
            var stream = await FileSystem.OpenAppPackageFileAsync(Constants.ProductDatabaseFilename);

            using (var fileStream = File.Create(Path.Combine(FileSystem.AppDataDirectory, Constants.ProductDatabaseFilename)))
            {
                stream.CopyTo(fileStream);
            }
        }
    }

	private async void OnRegisterButtonClicked(object sender, EventArgs e)
	{
        await createProductDatabase();
        await Navigation.PushAsync(new RegisterPage());
    }

    private async void OnLoginTap(object sender, TappedEventArgs e)
    {
        await createProductDatabase();
        await Navigation.PushAsync(new MainActivityPage());
    }
}

