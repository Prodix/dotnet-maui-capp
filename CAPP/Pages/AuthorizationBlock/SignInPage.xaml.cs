using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace CAPP;

public partial class SignInPage : ContentPage
{
    string email;
    string password;
    bool checkState;
    HttpClient httpClient = new();

	public SignInPage()
	{
		InitializeComponent();
	}

    private async void OnClickOnLoginWithoutRegister(object sender, TappedEventArgs e)
    {
        if (!InputCheckBox.IsChecked)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            await Toast.Make("Вы должны принять политику конфединциальности", ToastDuration.Short).Show(cancellationTokenSource.Token);
        }
        else
        {
            await Navigation.PushAsync(new HeightPage(IsWithoutRegister: true));
        }
    }

    private void EmailChanged(object sender, TextChangedEventArgs e)
    {
        email = EmailEntry.Text;
        OnValidData();
    }

    private void PasswordChanged(object sender, TextChangedEventArgs e)
    {
        password = PasswordEntry.Text;
        OnValidData();
    }

    private void CheckChanged(object sender, EventArgs e)
    {
        checkState = InputCheckBox.IsChecked;
        OnValidData();
    }

    private bool OnValidData()
    {
        if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) && checkState)
        {
            GoNextButton.IsEnabled = true;
            return true;
        }
        else
        {
            GoNextButton.IsEnabled = false;
            return false;
        }
    }

    private bool checkInternet()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            return true;
        }
        else
        {
            Toast.Make("Пожалуйста включите интернет", ToastDuration.Short).Show();
            return false;
        }
    }

    private async void OnLogin(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        if (checkInternet())
        {
            var request = await httpClient.PostAsJsonAsync(Constants.serverLink, new AccountDataForAuthorization { email = email, password = password });
            var respone = JsonConvert.DeserializeObject<ResponseJsonMessage>(await request.Content.ReadAsStringAsync());

            if (!request.IsSuccessStatusCode)
            {
                await Toast.Make(respone.message, ToastDuration.Short).Show(cancellationTokenSource.Token);
            }
            else
            {
                await Toast.Make(respone.message, ToastDuration.Short).Show(cancellationTokenSource.Token);
            }
        }
        else
        {
            await Toast.Make("Вы должны принять политику конфиденциальности", ToastDuration.Short).Show(cancellationTokenSource.Token);

        }
    }
}