using CAPP.Pages.MainBlock;
using CAPP.Resources.Styles;
using Newtonsoft.Json;

namespace CAPP;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
        if (File.Exists(Constants.UserDataPath))
        {
            Shell.Current.GoToAsync("///Activity");
            UserData userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(Constants.UserDataPath));
            switch (userData.Theme)
            {
                case "Зелёная":
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(new GreenTheme());
                    Application.Current.Resources.MergedDictionaries.Add(new Styles());
                    break;
                case "Синяя":
                    break;
                case "Розовая":
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(new PinkTheme());
                    Application.Current.Resources.MergedDictionaries.Add(new Styles());
                    break;
                default:
                    break;
            }
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

